using System.Linq;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using NeedsVsWants.Audio;
using NeedsVsWants.Player;
using NeedsVsWants.Patterns;
using NeedsVsWants.MenuSystem;
using NeedsVsWants.PhoneSystem;

using TMPro;

namespace NeedsVsWants.ShoppingSystem
{
    public class ItemListMenu : Menu
    {
        [SerializeField]
        VerticalLayoutGroup _ContentLayoutGroup;
        [SerializeField]
        ItemViewerMenu _ItemViewerMenu;
        [SerializeField]
        LoadingPopUp _LoadingPopUp;
        [SerializeField]
        AudioAsset _ButtonClickAsset;

        [Header("Sale")]
        [SerializeField]
        TMP_Text _SaleText;
        [SerializeField]
        TMP_Text _ShopText;
        [SerializeField]
        VerticalLayoutGroup _SaleLeftList;
        [SerializeField]
        VerticalLayoutGroup _SaleRightList;
        [SerializeField]
        AudioAsset _SaleSFXAsset;

        [Header("Shop")]
        [SerializeField]
        VerticalLayoutGroup _ShopLeftList;
        [SerializeField]
        VerticalLayoutGroup _ShopRightList;

        Item[] _Item;

        Coroutine _UpdateListCoroutine;

        protected override void Start()
        {
            base.Start();
            
            System.Action<Item[]> onItemListChange = items =>
            {
                if(isActive)
                {
                    _ShopLeftList.transform.SetActiveChildren(false);
                    _ShopRightList.transform.SetActiveChildren(false);

                    _SaleLeftList.transform.SetActiveChildren(false);
                    _SaleRightList.transform.SetActiveChildren(false);

                    StartCoroutine(UpdateItemList());
                }
            };

            ObjectPoolManager.instance.Instantiate("Item Button");    

            PlayerStatManager.instance.onAddItems += items => _SaleSFXAsset.PlayOneShot(audioSource);
            PlayerStatManager.instance.onEditItems += onItemListChange;
            PlayerStatManager.instance.onRemoveItems += onItemListChange;
        }

        IEnumerator UpdateItemList()
        {
            Item[] saleItems = Player.PlayerStatManager.instance.ShopItems.Where(items => items.isDiscounted).ToArray();
            Item[] shopItems = Player.PlayerStatManager.instance.ShopItems.Where(items => !items.isDiscounted).ToArray();

            ItemButton itemButton;

            _LoadingPopUp.EnablePopUp();

            _SaleText.transform.parent.gameObject.SetActive(saleItems.Length > 0);

            if(saleItems.Length > 0)
            {
                _SaleText.transform.parent.gameObject.SetActive(true);

                for(int i = 0; i < saleItems.Length; i++)
                {
                    itemButton = ObjectPoolManager.instance.GetObject("Item Button").GetComponent<ItemButton>();

                    itemButton.AssignItem(saleItems[i], menuGroup as AppMenuGroup, _ItemViewerMenu, () => _ButtonClickAsset.PlayOneShot(audioSource));
                    itemButton.transform.SetParent(i % 2 == 0 ? _SaleLeftList.transform :_SaleRightList.transform, false);
                }
            }

            else
                _SaleText.transform.parent.gameObject.SetActive(false);

            if(shopItems.Length > 0)
            {
                _ShopText.transform.parent.gameObject.SetActive(true);
                
                for(int i = 0; i < shopItems.Length; i++)
                {
                    itemButton = ObjectPoolManager.instance.GetObject("Item Button").GetComponent<ItemButton>();

                    itemButton.AssignItem(shopItems[i], menuGroup as AppMenuGroup, _ItemViewerMenu, () => _ButtonClickAsset.PlayOneShot(audioSource));
                    itemButton.transform.SetParent(i % 2 == 0 ? _ShopLeftList.transform :_ShopRightList.transform, false);
                }
            }

            else
                _ShopText.transform.parent.gameObject.SetActive(false);
            

            _ContentLayoutGroup.enabled = false;

            yield return new WaitForSecondsRealtime(.5f);

            _ContentLayoutGroup.enabled = true;

            yield return new WaitForEndOfFrame();

            _LoadingPopUp.DisablePopUp();
        }

        protected override void OnEnableMenu() 
        { 
            transform.SetActiveChildren(true);

            _UpdateListCoroutine = StartCoroutine(UpdateItemList());
        }

        protected override void OnDisableMenu() 
        { 
            transform.SetActiveChildren(false);

            if(_UpdateListCoroutine != null)
                StopCoroutine(_UpdateListCoroutine);
        }

        protected override void OnReturn() { }

        protected override void OnSwitchFrom() { }
    }
}