﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/*  Info
 *  + 処理速度に問題があれば(Add|Remove)ActionsでSortActionsを呼ぶのをやめ，
 *    actionConfigsを変更せずに直接orderedActionsを設定するように変更する可能性がある
 */

[DisallowMultipleComponent]
public class ActionManager : MonoBehaviour {

    /*  各Actionの設定のリスト
     *  + InspectorやAddActionsメソッドによって設定される
     */
    public List<ActionConfig> actionConfigs;

#if UNITY_EDITOR
    [Space(15)]
    public string configFile;
#endif

    /*  順序化されたActionConfig
     *  + ActionConfigのorderをDictionaryのKeyとした辞書
     *    同じorderのものはリストにまとめられる
     *  + actionConfigsを変更した際に更新される
     */
    SortedDictionary<int, List<ActionConfig>> orderedActions
        = new SortedDictionary<int, List<ActionConfig>>();

    /*  現在実行中のActionConfigのリスト
     *  + ActionConfigはActionを実行する際にdoingActionsに追加される
     *  + doingActions内のActionConfigはFixedUpdateのはじめにAction.IsDoneが
     *    呼ばれ，trueだった場合はリストから削除される
     */
    List<ActionConfig> doingActions = new List<ActionConfig>();

    /*  現在ブロックされるActionの型のリスト
     *  + blockActionTypesはdoingActionsに基づいて設定される
     *  + doingActionsとともにActionの実行の際と，FixedUpdateのはじめに更新される
     */
    List<System.Type> blockActionTypes = new List<System.Type>();

    /*
     *  + actionConfigsの初期化処理
     *  + orderedActionsの更新
     */
    void Start () {
        foreach (ActionConfig action in actionConfigs) {
            action.Init(this);
        }
        SortActions();
    }

    /*  orderedActionsを更新するメソッド
     *  + actionConfigsを参照し，orderをKeyとして辞書化してorderedActionsに入れる
     *  + 同じorderのActionConfigはリストとしてひとまとめにされる
     */
    void SortActions () {
        orderedActions.Clear();
        foreach (ActionConfig action in actionConfigs) {
            int order = action.order;
            if (orderedActions.ContainsKey(order)) {
                orderedActions[order].Add(action);
            } else {
                orderedActions.Add(order, new List<ActionConfig> {action});
            }
        }
    }

    /*  ActionConfigを追加するためのメソッド
     *  + 追加されるActionConfigを初期化
     *  + actionConfigsにActionConfigを追加
     *  + 追加後はorderedActionsを更新する
     */
    public void AddActions (ActionConfig[] actions) {
        foreach (ActionConfig action in actions) {
            action.Init(this);
        }
        actionConfigs.AddRange(actions);
        SortActions();
    }

    /*  ActionConfigを削除するためのメソッド
     *  + 引数は削除されるActionConfigを指定する
     *  + actionConfigsから指定されたActionConfigを削除する
     *  + 削除後はorderedActionsを更新する
     */
    public void RemoveActions (ActionConfig[] actions) {
        actionConfigs.RemoveAll(action => actions.Contains(action));
        SortActions();
    }

    /*  渡されたActionConfigのリストから実行不可のものを取り除くメソッド
     *  + 以下のものを取り除く
     *    - Actionがdisableなもの
     *    - blockActionsに指定されているもの
     *    - 条件（conditions）を満たしていないもの
     */
    List<ActionConfig> RemoveNeedless (List<ActionConfig> actions) {
        List<ActionConfig> availableActions = new List<ActionConfig>();
        foreach (ActionConfig action in actions) {
            System.Type actionType = action.action.GetType();
            if (action.action.enabled
                && !blockActionTypes.Any(type => actionType.IsClassOf(type))
                && action.IsAvailable()
            ) {
                availableActions.Add(action);
            }
        }
        return availableActions;
    }

    //  リストからweightを重みとして確率的にActionConfigを選択するメソッド
    ActionConfig SelectRandom (List<ActionConfig> actions) {
        int totalWeight = actions.Sum(action => action.weight);
        float rnd = Random.value * totalWeight;
        foreach (ActionConfig action in actions) {
            rnd -= action.weight;
            if (rnd <= 0.0f) {
                return action;
            }
        }
        return actions[0];
    }

    /*  ActionConfigのActionを実行するためのメソッド
     *  + Actionを実行
     *  + doingActionsにActionConfigを追加
     *  + blockActionTypesにActionConfigのblockActionTypesを追加
     */
    void DoAction (ActionConfig action) {
        action.Act();
        if (!doingActions.Contains(action)) {
            doingActions.Add(action);
            blockActionTypes.AddRange(action.blockActionTypes);
        }
    }

    /*  doingActionsとblockActionTypesの更新を行うためのメソッド
     *  + 各doingActionsがActionを終えているか調べて，
     *    終了しているならdoingActionsから削除する
     *  + 各doingActionsによってblockActionTypesを更新する
     */
    void UpdateBlock () {
        blockActionTypes.Clear();
        for (int i = doingActions.Count - 1; i >= 0; i--) {
            if (doingActions[i].action.IsDone()) {
                doingActions.RemoveAt(i);
            } else {
                blockActionTypes.AddRange(doingActions[i].blockActionTypes);
            }
        }
    }

    /*
     *  1. doingActionsとblockActionsの更新
     *  2. orderedActionsについて，order順に以下を実行
     *     1) ActionConfigのリストから実行不可なものを除去
     *     2) 残ったActionConfigが複数あれば確率的に選択
     *     3) ActionConfigのActionを実行（ActionConfigがなければ何もしない）
     */
    void FixedUpdate () {
        UpdateBlock();
        foreach (List<ActionConfig> actions in orderedActions.Values) {
            List<ActionConfig> availableActions = RemoveNeedless(actions);
            switch (availableActions.Count) {
                case 0:
                    break;
                case 1:
                    DoAction(availableActions[0]);
                    break;
                default:
                    DoAction(SelectRandom(availableActions));
                    break;
            }
        }
    }
}

public static class TypeExtension {
    //  deriveTypeがbaseTypeの継承かそれ自体であることを判定する関数
    public static bool IsClassOf (this System.Type deriveType, System.Type baseType) {
        return deriveType == baseType || deriveType.IsSubclassOf(baseType);
    }
}

[System.Serializable]
public class ActionConfig {
    public string actionName;
    public int order = 1;
    public ConditionConfig[] conditions = new ConditionConfig[0];
    public int weight = 1;
    public string[] blockActions = new string[0];

    [System.NonSerialized]
    public Action action;
    [System.NonSerialized]
    public System.Type[] blockActionTypes;

    Dictionary<string, object> args = new Dictionary<string, object>();

    public ActionConfig () {}

    public ActionConfig (
            string actionName,
            int order = 0,
            ConditionConfig[] conditions = null,
            int weight = 1,
            string[] blockActions = null
        ) {
        this.actionName = actionName;
        this.order = order;
        if (conditions != null) {
            this.conditions = conditions;
        }
        this.weight = weight;
        if (blockActions != null) {
            this.blockActions = blockActions;
        }
    }

    public void Init (ActionManager manager) {
        action = manager.GetComponents<Action>().First(
            elm => elm.actionName == actionName);

        foreach (ConditionConfig condition in conditions) {
            condition.Init(manager);
        }
        int len = blockActions.Length;
        blockActionTypes = new System.Type[len];
        for (int i = 0; i < len; i++) {
            blockActionTypes[i] = System.Type.GetType(blockActions[i]);
        }
    }

    public bool IsAvailable () {
        args.Clear();
        foreach (ConditionConfig condition in conditions) {
            ConditionState state = condition.Check();
            if (!state.isSatisfied) {
                return false;
            }
            foreach (KeyValuePair<string, object> arg in state.args) {
                args[arg.Key] = arg.Value;
            }
        }
        return true;
    }

    public void Act () {
        action.Act(args);
    }
}

[System.Serializable]
public class ConditionConfig {
    public string conditionName;
    public string[] args = new string[0];
    public bool not = false;

    Condition condition;

    public ConditionConfig () {}

    public ConditionConfig (
            string conditionName,
            string[] args = null,
            bool not = false
        ) {
        this.conditionName = conditionName;
        this.not = not;
        if (args != null) {
            this.args = args;
        }
    }

    public void Init (ActionManager manager) {
        condition = manager.GetComponents<Condition>().First(
            elm => elm.conditionName == conditionName);
    }

    public ConditionState Check () {
        ConditionState state = condition.Check(args);
        state.isSatisfied ^= not;
        return state;
    }
}
