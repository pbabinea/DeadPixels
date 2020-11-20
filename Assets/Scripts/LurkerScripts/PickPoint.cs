using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickPoint : StateMachineBehaviour
{
    public MonsterBehavior behavior;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex){
        behavior = animator.gameObject.GetComponent<MonsterBehavior>();
        behavior.setMovement("waypoint");
        behavior.nextPoint();
    }
}
