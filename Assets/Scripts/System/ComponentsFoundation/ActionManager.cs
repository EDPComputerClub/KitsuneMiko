using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class ActionManager : MonoBehaviour {
    // 登録するActionのセッティングデータを格納する配列
    public List<ActionConfig> actionConfigs;

    protected List<ActionConfig> doingActions = new List<ActionConfig>();
    protected List<System.Type> blockActionTypes = new List<System.Type>();
    protected SortedDictionary<int, List<ActionConfig>> orderedActions
        = new SortedDictionary<int, List<ActionConfig>>();

    // ActionConfigとConditionConfigの初期化
    protected virtual void Start () {
        foreach (ActionConfig action in actionConfigs) {
            action.Init(this);
        }
        SortActions();
    }

    // orderedActionsListの中身をaction.orderに従って並べ替える
    protected virtual void SortActions () {
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

    public virtual void AddActions (ActionConfig[] actions) {
        foreach (ActionConfig action in actions) {
            action.Init(this);
        }
        actionConfigs.AddRange(actions);
        SortActions();
    }

    public virtual void RemoveActions (ActionConfig[] actions) {
        actionConfigs.RemoveAll(action => actions.Contains(action));
        SortActions();
    }

    // 実行可能なactionsのみ取り出す
    protected virtual List<ActionConfig> RemoveNeedless (List<ActionConfig> actions) {
        // 新しいGeneric Collection Listを宣言
        List<ActionConfig> availableActions = new List<ActionConfig>();
        //与えられたActionConfigsの中からひとつActionConfigを取り出し、
        foreach (ActionConfig action in actions) {
            // ActionConfigの中のActionの型を取得してactionTypeに代入
            System.Type actionType = action.action.GetType();

            // blockActionTypesの中に、actionTypeと同じ型またはその派生型であり、ActionConfig.actionが実行可能であれば、
            // availableActionsListの中にそのactionを追加する
            if (!blockActionTypes.Any(type => actionType == type || actionType.IsSubclassOf(type))
                && action.IsAvailable()
            ) {
                availableActions.Add(action);
            }
        }
        return availableActions;
    }

    // 重み付き確率により実行するActionをランダムに決める
    protected virtual ActionConfig SelectRandom (List<ActionConfig> actions) {
        // selectedActionにactionsの最後のアイテムを代入
        ActionConfig selectedAction = actions[actions.Count - 1];

        int totalWeight = 0;
        foreach (ActionConfig action in actions) {
            totalWeight += action.weight;
        }

        float rnd = Random.value * totalWeight;
        foreach (ActionConfig action in actions) {
            rnd -= action.weight;
            if (rnd <= 0) {
                selectedAction = action;
            }
        }
        return selectedAction;
    }

    // actionを実行する
    protected virtual void DoAction (ActionConfig action) {
        action.Act();
        if (!doingActions.Contains(action)) {
            doingActions.Add(action);
            blockActionTypes.AddRange(action.blockActionTypes);
        }
    }

    // blockActionTypesからIsDone() = true : 処理が終わっているActionを消す
    protected virtual void UpdateBlock () {
        // blockActionTypesの初期化
        blockActionTypes.Clear();
        // doingActionsの中身からIsDone()の返り値としてtrueが返ってきたものをRemove
        // doingActionsの中身からIsDone()の返り値としてfalseが返ってきたものをblockActionTypesに追加
        for (int i = doingActions.Count - 1; i >= 0; i--) {
            if (doingActions[i].action.IsDone()) {
                doingActions.RemoveAt(i);
            } else {
                blockActionTypes.AddRange(doingActions[i].blockActionTypes);
            }
        }
    }

    protected virtual void FixedUpdate () {
        UpdateBlock();
        foreach (List<ActionConfig> actions in orderedActions.Values) {
            List<ActionConfig> availableActions = RemoveNeedless(actions);
            // 実行可能なActionの数で条件分けを行う
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

[System.Serializable]
// 登録するActionのデータを格納するクラス
public class ActionConfig {
    [System.Serializable]
    // Actionを実行するために必要な条件のデータを格納するクラス
    public class ConditionConfig {
        
        // 条件の名前
        public string conditionName;
        // 条件の反転を行うかどうか
        public bool not = false;

        [System.NonSerialized]
        public Condition condition;

        public ConditionConfig (bool not, string conditionName) {
            this.not = not;
            this.conditionName = conditionName;
        }

        public virtual void Init (ActionManager manager) {
            // ActionConfig.conditionNameと名前が一致するConditionコンポーネントをActionConfig.conditionに代入
            condition = manager.GetComponents<Condition>().First(
                elm => elm.conditionName == conditionName);
        }
    }

    // Actionの名前
    public string actionName;
    // 実行するActionの優先順位. 昇順.
    public int order = 1;
    // Actionを実行するのに必要な条件を格納する配列
    public ConditionConfig[] conditions;
    // 重み付き確率を行うための重みの設定
    public int weight = 1;
    // Actionを行うにあたっての実行をブロックする他のActionsの型名:string
    public string[] blockActions;

    [System.NonSerialized]
    public Action action;
    [System.NonSerialized]
    public System.Type[] blockActionTypes;

    protected Dictionary<string, object> args;
    // 以下は出来ない.
    //public Dictionary<string, object> args = new Dictionary<string, object>();

    public ActionConfig (
            string actionName, int order, int weight,
            Dictionary<string, bool> conditions, string[] blockActions
        ) {
        this.actionName = actionName;
        this.order = order;
        this.weight = weight;
        this.blockActions = blockActions;
        int idx = 0;
        int len = conditions.Count;
        this.conditions = new ConditionConfig[len];
        foreach (KeyValuePair<string, bool> condition in conditions) {
            this.conditions[idx] = new ConditionConfig(condition.Value, condition.Key);
            idx += 1;
        }
    }

    public virtual void Init (ActionManager manager) {
        // ActionConfig.actionNameと名前が一致するActionコンポーネントをActionConfig.actionに代入
        action = manager.GetComponents<Action>().First(elm => elm.actionName == actionName);
        this.args = new Dictionary<string, object>();
        // ConditionのInitialize
        foreach (ConditionConfig condition in conditions) {
            condition.Init(manager);
        }

        // blockActions(ブロックするActionのリスト):string[]の要素数を取得
        int len = blockActions.Length;
        blockActionTypes = new System.Type[len];
        for (int i = 0; i < len; i++) {
            // string型からType型を取得してblockActionTypesに代入
            blockActionTypes[i] = System.Type.GetType(blockActions[i]);
        }
    }

    public virtual bool IsAvailable () {
        args.Clear();
        // データ格納用structureの宣言
        ConditionState state;
        // conditionsに入っているconditionをひとつずつ取り出す
        foreach (ConditionConfig condition in conditions) {
            // conditionの中に入っているCheckメソッドを呼び出してConditionState.stateに代入する
            state = condition.condition.Check();
            // condition.notでConditionのReverseを行う
            if (condition.not ? state.isSatisfied : !state.isSatisfied) {
                // ひとつでもcondition.isSatisfiedを満たしていなければreturn false
                return false;
            }
            // ActionConfig.args に ConditionState.args の代入を行う
            args = args.Union(state.args)
                        .ToDictionary(elm => elm.Key, elm => elm.Value);
        }
        return true;
    }

    public virtual void Act () {
        action.Act(args);
    }
}
