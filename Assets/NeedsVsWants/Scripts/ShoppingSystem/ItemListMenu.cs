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
        TMP_Text _Text;
        [SerializeField]
        VerticalLayoutGroup _SaleLeftList;
        [SerializeField]
        VerticalLayoutGroup _SaleRightList;

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
                    _ContentLayoutGroup.gameObject.SetActive(false);
                    _ContentLayoutGroup.gameObject.SetActive(true);

                    StartCoroutine(UpdateItemList());
                }
            };

            ObjectPoolManager.instance.Instantiate("Item Button");    

            PlayerStatManager.instance.onEditItems += onItemListChange;
            PlayerStatManager.instance.onRemoveItems += onItemListChange;
        }

        IEnumerator UpdateItemList()
        {
            Item[] saleItems = Player.PlayerStatManager.instance.ShopItems.Where(items => items.isDiscounted).ToArray();
            Item[] shopItems = Player.PlayerStatManager.instance.ShopItems.Where(items => !items.isDiscounted).ToArray();

            ItemButton[] shopItemButtons = ObjectPoolManager.instance.GetObjects("Item Button", shopItems.Length).
                Select(g => g.GetComponent<ItemButton>()).ToArray();
            ItemButton[] saleItemButtons = ObjectPoolManager.instance.GetObjects("Item Button", saleItems.Length).
                Select(g => g.GetComponent<ItemButton>()).ToArray();

            _LoadingPopUp.EnablePopUp();

            _Text.transform.parent.gameObject.SetActive(saleItems.Length > 0);

            for(int i = 0; i < shopItemButtons.Length; i++)
            {
                shopItemButtons[i].AssignItem(shopItems[i], menuGroup as AppMenuGroup, _ItemViewerMenu, () => _ButtonClickAsset.PlayOneShot(audioSource));
                shopItemButtons[i].transform.SetParent(i % 2 == 0 ? _ShopLeftList.transform : _ShopRightList.transform, false);
            }
            
            for(int i = 0; i < saleItemButtons.Length; i++)
            {
                saleItemButtons[i].AssignItem(saleItems[i], menuGroup as AppMenuGroup, _ItemViewerMenu, () => _ButtonClickAsset.PlayOneShot(audioSource));
                saleItemButtons[i].transform.SetParent(i % 2 == 0 ? _SaleLeftList.transform : _SaleRightList.transform, false);
            }

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