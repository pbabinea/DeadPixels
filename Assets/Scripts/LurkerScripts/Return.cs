using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Return : StateMachineBehaviour
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        MonsterBehavior behavior = animator.gameObject.GetComponent<MonsterBehavior>();
        behavior.teleport(behavior.spawn);
        Debug.Log("teleport");
    }
}
