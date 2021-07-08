using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NeedsVsWants.MenuSystem;

namespace NeedsVsWants.MovementSystem
{
    
    
    public class AnaAI : MonoBehaviour
    {
        Animator anim;
       // public GameObject menu;
        [SerializeField] MenuGroup Activity;
        [SerializeField] MenuGroup Activity01;
        [SerializeField] MenuGroup Activity02;
        [SerializeField] MenuGroup Activity03;
        /*public GameObject GetMenu()
        {
            return menu;
        }*/
        
        void Start()
        {
            //GameObject theMenu = GameObject.Find("MenuGroup");
            //MenuSystem.MenuGroup menuGroup = theMenu.GetComponent<MenuSystem.MenuGroup>();
            anim = GetComponent<Animator>();
            
        }

        void CheckBill()
        {
            anim.SetBool("New Bool", Activity.isActive);
        }

        void CheckInvestments()
        {
            anim.SetBool("Invest Bool", Activity01.isActive);
        }

        void CheckMessages()
        {
            anim.SetBool("Msg Bool", Activity02.isActive);
        }

        void CheckShopping()
        {
            anim.SetBool("Shop Bool", Activity03.isActive);
        }
        void Update()
        {

            CheckBill();
            CheckInvestments();
            CheckMessages();
            CheckShopping();
        }


    }
}