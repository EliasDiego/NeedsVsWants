using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

using NeedsVsWants.Player;
using NeedsVsWants.Patterns;
using NeedsVsWants.WelfareSystem;

namespace NeedsVsWants.DropSystem
{
    public class DropManager : SimpleSingleton<DropManager>
    {
        [SerializeField]
        LayerMask _HitLayer;
        [SerializeField]
        EventSystem _EventSystem;
        [SerializeField]
        InputActionReference _LeftClick;
        [SerializeField]
        InputActionReference _Point;

        [Space]
        [SerializeField]
        Transform _AnneTransform;
        [SerializeField]
        Sprite _HappinessDropSprite;
        [SerializeField]
        Sprite _HungerDropSprite;
        [SerializeField]
        Sprite _HealthDropSprite;
        [SerializeField]
        Sprite _SocialDropSprite;
        [SerializeField]
        Sprite _MoneyDropSprite;

        void Start() 
        {
            ObjectPoolManager.instance.Instantiate("Welfare Drop");
        }

        void OnEnable() 
        {
            _LeftClick.action.started += OnLeftClick;
        }

        void OnDisable() 
        {
            _LeftClick.action.started -= OnLeftClick;
        }

        void OnLeftClick(InputAction.CallbackContext context)
        {
            Vector2 mousePosition = _Point.action.ReadValue<Vector2>();

            Ray ray = Camera.main.ScreenPointToRay(mousePosition);

            if(!_EventSystem.IsPointerOverGameObject())
            {
                foreach(RaycastHit hit in Physics.RaycastAll(ray, float.MaxValue, _HitLayer))
                {
                    if(hit.collider.gameObject.TryGetComponent<Drop>(out Drop drop) && !drop.isClicked)
                    {
                        drop.OnClick();
                        
                        break;
                    }
                }
            }
        }

        Sprite GetSprite(DropType dropType)
        {
            Sprite sprite = null;

            switch(dropType)
            {
                case DropType.Social:
                sprite = _SocialDropSprite;
                break;
                
                case DropType.Happiness:
                sprite = _HappinessDropSprite;
                break;
                
                case DropType.Health:
                sprite = _HealthDropSprite;
                break;
                
                case DropType.Hunger:
                sprite = _HungerDropSprite;
                break;
                
                case DropType.Money:
                sprite = _MoneyDropSprite;
                break;
            }

            return sprite;
        }

        void Spawn(DropType DropType, double valuePerDrop, int dropCount, Vector3 spawnPoint)
        {
            for(int i = 0; i < dropCount; i++)
                ObjectPoolManager.instance.GetObject("Welfare Drop").GetComponent<Drop>().
                    SetDrop(DropType, GetSprite(DropType), valuePerDrop, spawnPoint);
        }

        public void SpawnDropsOnAnne(double money, int dropCount)
        {
            Spawn(DropType.Money, money / (double)dropCount, dropCount, _AnneTransform.transform.position);
        }

        public void SpawnDrops(WelfareOperator welfareOperator, int dropCount, Vector3 spawnPoint)
        {
            float valueDifference;

            if(welfareOperator.healthValue > 0)
            {
                valueDifference = welfareOperator.GetHealth(PlayerStatManager.instance.currentHealthWelfare).value - 
                    PlayerStatManager.instance.currentHealthWelfare.value;

                Spawn(DropType.Health, valueDifference / dropCount, dropCount, spawnPoint);
            }

            if(welfareOperator.hungerValue > 0)
            {
                valueDifference = welfareOperator.GetHunger(PlayerStatManager.instance.currentHungerWelfare).value - 
                    PlayerStatManager.instance.currentHungerWelfare.value;

                Spawn(DropType.Hunger, valueDifference / dropCount, dropCount, spawnPoint);
            }
            
            if(welfareOperator.happinessValue > 0)
            {
                valueDifference = welfareOperator.GetHappiness(PlayerStatManager.instance.currentHappinessWelfare).value - 
                    PlayerStatManager.instance.currentHappinessWelfare.value;

                Spawn(DropType.Happiness, valueDifference / dropCount, dropCount, spawnPoint);
            }
            
            if(welfareOperator.socialValue > 0)
            {
                valueDifference = welfareOperator.GetSocial(PlayerStatManager.instance.currentSocialWelfare).value - 
                    PlayerStatManager.instance.currentSocialWelfare.value;

                Spawn(DropType.Social, valueDifference / dropCount, dropCount, spawnPoint);
            }
        }
        
        public void SpawnDropsOnAnne(WelfareOperator welfareOperator, int dropCount)
        {
            SpawnDrops(welfareOperator, dropCount, _AnneTransform.position);
        }
    }
}