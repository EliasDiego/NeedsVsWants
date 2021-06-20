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
        TMP_Text _QuantityText;

        int _Quantity;

        Item _Item;

        public int quantity => _Quantity;

        public Item item => _Item;

        public bool isToggled => _Toggle.isOn;

        public void AssignItem(Item item, int quantity)
        {
            // Assign Item Properties
            _PreviewImage.sprite = item.previewImage;
            _NameText.text = item.name;
            _PriceText.text = StringFormat.ToPriceFormat(item.price);
            _Toggle.isOn = false;

            // Assign to public Properties
            _Item = item;
            _Quantity = quantity;

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
        
        public void Increment()
        {
            if(_Quantity + 1 < 100)
            {
                _Quantity++;

                _QuantityText.text = quantity.ToString();
            }
        }

        public void Decrement()
        {
            if(_Quantity - 1 > 0)
            {
                _Quantity--;

                _QuantityText.text = quantity.ToString();
            }
        }
    }
}