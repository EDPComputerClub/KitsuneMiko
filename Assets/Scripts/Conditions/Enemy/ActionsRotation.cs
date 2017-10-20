using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*  使用方法
 *  + numにローテーションで回すAction数を設定
 *  + それぞれのActionConfigに対して
 *    - このConditionを設定
 *    - argsにローテーション順序の何番目かを設定（0からnum-1）
 *    - orderをかぶらないように設定（argsと同じが分かりやすい）
 *    - blockActionsにActionを追加
 */

[AddComponentMenu("Conditions/Enemy/Actions Rotation")]
public class ActionsRotation : Condition {
    public int num;
    int count = 0;

    public override ConditionState Check (string[] args) {
        ConditionState state = new ConditionState();
        int order = int.Parse(args[0]);
        if (order == count) {
            state.isSatisfied = true;
            count = (count + 1) % num;
        }
        return state;
    }
}
