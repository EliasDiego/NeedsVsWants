using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using TMPro;

namespace NeedsVsWants.ShoppingSystem
{
    public class ItemSlot : MonoBehaviour
    {
        [SerializeField]
        Image _PreviewImage;
        [SerializeField]
        Toggle _Toggle;
        [SerializeField]
        TMP_Text _NameText;
        [SerializeField]
        TMP_Text _PriceText;
        [SerializeField]
        TMP_Text _FromPriceText;
        [SerializeField]
        TMP_Text _QuantityText;

        int _Quantity;

        Item _Item;

        System.Action _OnQuantityChange;
        System.Action _OnToggleChange;

        public int quantity => _Quantity;

        public Item item => _Item;

        public bool isToggled { get => _Toggle.isOn; set => _Toggle.isOn = value; }

        public void AssignItem(Item item, int quantity, System.Action onQuantityChange, System.Action onToggleChange)
        {
            // Assign to public Properties
            _Item = item;
            _Quantity = quantity;
            _OnQuantityChange = onQuantityChange as System.Action;
            _OnToggleChange = onToggleChange as System.Action;

            UpdateItemDetails();

            // When Item is not a Game Object Item
            if(item.GetType() != typeof(GameObjectItem))
            {
                // Activate Quantity Buttons and text
                _QuantityText.transform.parent.gameObject.SetActive(true);
                _QuantityText.text = quantity.ToString();
            }

            else
                _QuantityText.transform.parent.gameObject.SetActive(false);
        }
        
        public void UpdateItemDetails()
        {
            _PreviewImage.sprite = item.previewImage;
            _NameText.text = item.name;
            _PriceText.text = StringFormat.ToPriceFormat(item.isDiscounted ? item.discountPrice : item.price);
            _FromPriceText.gameObject.SetActive(item.isDiscounted);
            _FromPriceText.text = "From <color=red>" + StringFormat.ToPriceFormat(item.price) + "</color>";
        }
        
        public void Increment()
        {
            if(_Quantity + 1 < 100)
            {
                _Quantity++;

                _QuantityText.text = quantity.ToString();

                if(isToggled)
                    _OnQuantityChange?.Invoke();
            }
        }

        public void Decrement()
        {
            if(_Quantity - 1 > 0)
            {
                _Quantity--;

                _QuantityText.text = quantity.ToString();
                
                if(isToggled)
                    _OnQuantityChange?.Invoke();
            }
        }

        public void OnToggle()
        {
            _OnToggleChange?.Invoke();
        }
    }
}