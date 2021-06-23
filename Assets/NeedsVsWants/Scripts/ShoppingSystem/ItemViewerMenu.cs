using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using NeedsVsWants.MenuSystem;

using TMPro;

namespace NeedsVsWants.ShoppingSystem
{
    public class ItemViewerMenu : Menu
    {
        [SerializeField]
        VerticalLayoutGroup _ContentLayoutGroup;
        [SerializeField]
        Image _PreviewImage;
        [SerializeField]
        TMP_Text _Name;
        [SerializeField]
        TMP_Text _Price;
        [SerializeField]
        TMP_Text _Description;
        [SerializeField]
        ItemCartMenu _ItemCartMenu;

        public Item item { get; set; }
        
        async protected override void OnEnableMenu() 
        { 
            transform.SetActiveChildren(true);

            _PreviewImage.sprite = item.previewImage;
            _Price.text = StringFormat.ToPriceFormat(item.price);
            _Name.text = item.name;
            _Description.text = item.description;
            
            await System.Threading.Tasks.Task.Delay(20);

            _ContentLayoutGroup.enabled = false;
            _ContentLayoutGroup.enabled = true;
        }

        protected override void OnDisableMenu() 
        { 
            transform.SetActiveChildren(false);
        }

        protected override void OnReturn() { }

        protected override void OnSwitchFrom() { }

        public void AddToCart()
        {
            _ItemCartMenu.AddToCart(item);
        }

        public void BuyNow()
        {
            _ItemCartMenu.AddAndSelectItemToCart(item);
        }
    }
}