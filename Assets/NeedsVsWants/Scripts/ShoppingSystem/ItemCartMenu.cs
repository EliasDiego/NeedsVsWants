using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using NeedsVsWants.MenuSystem;

namespace NeedsVsWants.ShoppingSystem
{
    public class ItemCartMenu : Menu
    {
        Dictionary<Item, int> _ItemCart = new Dictionary<Item, int>();

        protected override void OnEnableMenu() 
        { 
            transform.SetActiveChildren(true);
        }

        protected override void OnDisableMenu() 
        { 
            transform.SetActiveChildren(false);
        }

        protected override void OnReturn()
        {
            
        }

        protected override void OnSwitchFrom()
        {
            
        }

        public void AddToCart(Item item)
        {
            if(_ItemCart.ContainsKey(item))
                _ItemCart[item]++;

            else
                _ItemCart.Add(item, 1);
        }   
    }
}