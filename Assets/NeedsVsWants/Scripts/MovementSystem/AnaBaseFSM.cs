using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NeedsVsWants.MovementSystem
{
    public class AnaBaseFSM : StateMachineBehaviour
    {
        protected GameObject Ana;
       
        [SerializeField] protected float speed = 5.0f; //ana movespeed
        [SerializeField] protected float rotspeed = 100.0f; //ana rotation
        [SerializeField] protected float accuracy = 1.0f; //checks distance whether to go to the next waypoint or not

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            Ana = animator.gameObject;
           
        }

    }

}
