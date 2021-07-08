using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NeedsVsWants.MovementSystem
{
    public class Busy : AnaBaseFSM
    {
        GameObject[] busypoint; //gets an error when not called as an array, don't change
        Transform TeleportGoal;

        void Awake()
        {
            busypoint = GameObject.FindGameObjectsWithTag("busypoint");
        }

        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateEnter(animator, stateInfo, layerIndex);
            
        }

        override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            TeleportGoal.position = busypoint[0].transform.position;
            Ana.transform.position = TeleportGoal.position;
        }

        override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {

        }
    }
}