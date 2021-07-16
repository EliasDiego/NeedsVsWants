using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

using NeedsVsWants.Patterns;

namespace NeedsVsWants.WelfareSystem
{
    public class WelfareDropManager : MonoBehaviour
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

        public enum WelfareType
        {
            Social, Happiness, Hunger, Health
        }

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
                    if(hit.collider.gameObject.TryGetComponent<WelfareDrop>(out WelfareDrop drop) && !drop.isClicked)
                    {
                        drop.OnClick();
                        
                        break;
                    }
                }
            }
        }

        Sprite GetSprite(WelfareType welfareType)
        {
            Sprite sprite = null;

            switch(welfareType)
            {
                case WelfareDropManager.WelfareType.Social:
                sprite = _SocialDropSprite;
                break;
                
                case WelfareDropManager.WelfareType.Happiness:
                sprite = _HappinessDropSprite;
                break;
                
                case WelfareDropManager.WelfareType.Health:
                sprite = _HealthDropSprite;
                break;
                
                case WelfareDropManager.WelfareType.Hunger:
                sprite = _HungerDropSprite;
                break;
            }

            return sprite;
        }

        public void SpawnDrops(WelfareType welfareType, float valuePerDrop, int dropCount, Vector3 spawnPoint)
        {
            for(int i = 0; i < dropCount; i++)
                ObjectPoolManager.instance.GetObject("Welfare Drop").GetComponent<WelfareDrop>().
                    SetDrop(welfareType, GetSprite(welfareType), valuePerDrop, spawnPoint);
        }
        
        public void SpawnDropsOnAnne(WelfareType welfareType, float valuePerDrop, int dropCount)
        {
            SpawnDrops(welfareType, valuePerDrop, dropCount, _AnneTransform.position);
        }
    }
}