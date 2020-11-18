using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Track : StateMachineBehaviour
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        MonsterBehavior behavior = animator.gameObject.GetComponent<MonsterBehavior>();
        behavior.setMovement("player");
    }
}
