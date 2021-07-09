using System.Linq;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using NeedsVsWants.Player;
using NeedsVsWants.MenuSystem;
using NeedsVsWants.PhoneSystem;

using TMPro;

namespace NeedsVsWants.ShoppingSystem
{
    public class ItemViewerMenu : Menu
    {
        [SerializeField]
        VerticalLayoutGroup _ContentLayoutGroup;
        [SerializeField]
        ItemCartMenu _ItemCartMenu;

        [Header("Pop Up")]
        [SerializeField]
        LoadingPopUp _LoadingPopUp;
        [SerializeField]
        PopUp _ItemNotifPopUp;
        [SerializeField]
        TMP_Text _PopUpText;
        [SerializeField]
        Button _CloseButton;
        [SerializeField][Multiline]
        string _OnDiscount;
        [SerializeField][Multiline]
        string _OnNotDiscount;
        [SerializeField][Multiline]
        string _OnRemove;

        [Header("Item Details")]
        [SerializeField]
        Image _PreviewImage;
        [SerializeField]
        TMP_Text _Name;
        [SerializeField]
        TMP_Text _Price;
        [SerializeField]
        TMP_Text _FromPrice;
        [SerializeField]
        TMP_Text _Description;

        public Item item { get; set; }

        protected override void Start()
        {
            base.Start();
            
            PlayerStatManager.instance.onRemoveItems += items => 
            {
                if(isActive && items.Contains(item))
                    EnablePopUpForRemovedItem();
            };

            PlayerStatManager.instance.onEditItems += items =>
            {
                if(isActive && items.Contains(item))
                {
                    _PopUpText.text = item.isDiscounted ? _OnDiscount : _OnNotDiscount;
                    
                    _CloseButton.onClick.RemoveAllListeners();
                    _CloseButton.onClick.AddListener(async() => 
                    {
                        _ItemNotifPopUp.DisablePopUp();
                        _ItemNotifPopUp.transform.SetActiveChildren(false);
                        
                        _LoadingPopUp.EnablePopUp();

                        _Price.text = StringFormat.ToPriceFormat(item.isDiscounted ? item.discountPrice : item.price);
                        _FromPrice.gameObject.SetActive(item.isDiscounted);
                        _FromPrice.text = "From <color=red>" + StringFormat.ToPriceFormat(item.price) + "</color>";

                        _ContentLayoutGroup.enabled = false;

                        await System.Threading.Tasks.Task.Delay(100);
                        
                        _ContentLayoutGroup.enabled = true;
                        
                        _LoadingPopUp.DisablePopUp();
                    });

                    _ItemNotifPopUp.EnablePopUp();
                }
            };
        }

        void EnablePopUpForRemovedItem()
        {
            _PopUpText.text = _OnRemove;

            _CloseButton.onClick.RemoveAllListeners();
            _CloseButton.onClick.AddListener(() => 
            {
                _ItemNotifPopUp.DisablePopUp();
                _ItemNotifPopUp.transform.SetActiveChildren(false);

                menuGroup.Return();
            });
            
            _ItemNotifPopUp.EnablePopUp();
        }
        
        async protected override void OnEnableMenu() 
        { 
            transform.SetActiveChildren(true);

            _PreviewImage.sprite = item.previewImage;
            _Price.text = StringFormat.ToPriceFormat(item.isDiscounted ? item.discountPrice : item.price);
            _FromPrice.gameObject.SetActive(item.isDiscounted);
            _FromPrice.text = "From <color=red>" + StringFormat.ToPriceFormat(item.price) + "</color>";
            _Name.text = item.name;
            _Description.text = item.description;

            _LoadingPopUp.EnablePopUp();
            
            _ContentLayoutGroup.enabled = false;

            await System.Threading.Tasks.Task.Delay(200);

            _ContentLayoutGroup.enabled = true;
            
            await System.Threading.Tasks.Task.Delay(200);
            
            _LoadingPopUp.DisablePopUp();

            if(!PlayerStatManager.instance.ShopItems.Contains(item))
                EnablePopUpForRemovedItem();
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