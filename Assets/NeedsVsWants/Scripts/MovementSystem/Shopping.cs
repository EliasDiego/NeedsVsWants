using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NeedsVsWants.MovementSystem
{
    public class Shopping : AnaBaseFSM
    {
        GameObject[] shoppoint; //gets an error when not called as an array
        //Transform TeleportGoal;

        void Awake()
        {
            shoppoint = GameObject.FindGameObjectsWithTag("shoppoint");
        }

        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateEnter(animator, stateInfo, layerIndex);

        }

        override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            //TeleportGoal.position = busypoint[0].transform.position;
            Ana.transform.position = shoppoint[0].transform.position;
            Ana.transform.rotation = shoppoint[0].transform.rotation;
        }

        override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {

        }
    }
}