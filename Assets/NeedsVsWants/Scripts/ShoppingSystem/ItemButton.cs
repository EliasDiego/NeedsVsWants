using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using NeedsVsWants.PhoneSystem;

using TMPro;

namespace NeedsVsWants.ShoppingSystem
{
    public class ItemButton : Button
    {
        Image _Image;

        TMP_Text _Name;
        TMP_Text _Price;
        TMP_Text _FromPrice;

        protected override void Awake() 
        {
            _Image = transform.GetChild(0).GetComponent<Image>();
            
            _Name = transform.GetChild(1).GetComponent<TMP_Text>();
            _Price = transform.GetChild(2).GetComponent<TMP_Text>();
            _FromPrice = transform.GetChild(3).GetComponent<TMP_Text>();
        }

        public void AssignItem(Item item, AppMenuGroup appMenuGroup, ItemViewerMenu itemViewerMenu, System.Action onClickEvent)
        {
            _Image.sprite = item.previewImage;

            _Name.text = item.name;
            _Price.text = StringFormat.ToPriceFormat(item.isDiscounted ? item.discountPrice : item.price);
            _FromPrice.gameObject.SetActive(item.isDiscounted);
            _FromPrice.text = "From <color=red>" + StringFormat.ToPriceFormat(item.price) + "</color>";
            
            onClick.RemoveAllListeners();

            onClick.AddListener(() =>
            {
                itemViewerMenu.item = item;

                appMenuGroup.SwitchTo(itemViewerMenu);

                onClickEvent?.Invoke();
            });
        }
    }
}