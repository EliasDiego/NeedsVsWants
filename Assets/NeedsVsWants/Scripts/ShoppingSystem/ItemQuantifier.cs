using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using TMPro;

namespace NeedsVsWants.ShoppingSystem
{
    public class ItemQuantifier : MonoBehaviour
    {
        [SerializeField]
        Image _PreviewImage;
        [SerializeField]
        TMP_Text _Name;
        [SerializeField]
        TMP_Text _Price;
        [SerializeField]
        TMP_Text _Quantity;

        public void AssignItem(Item item, int quantity)
        {
            _PreviewImage.sprite = item.previewImage;
            _Name.text = item.name;
            _Price.text = item.price.ToString();
            _Quantity.text = quantity.ToString();
        }
        
        public void Increment()
        {

        }

        public void Decrement()
        {

        }
    }
}