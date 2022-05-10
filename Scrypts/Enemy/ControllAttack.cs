using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllAttack : StateMachineBehaviour
{
    public float attackDelay;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //выставляет нужную скорость анимации
        if (attackDelay < stateInfo.length)
            animator.SetFloat("AttackSpeed", stateInfo.length / attackDelay);
    }
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("isAttack", false);
    }
}
