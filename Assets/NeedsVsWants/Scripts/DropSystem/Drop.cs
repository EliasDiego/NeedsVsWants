using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;

using NeedsVsWants.Player;
using NeedsVsWants.WelfareSystem;

namespace NeedsVsWants.DropSystem
{
    public class Drop : MonoBehaviour
    {
        [SerializeField]
        float _SlowdownDeltaValue;
        [SerializeField]
        float _FloatSpeed;
        [SerializeField]
        float _AlphaShiftDeltaValue;
        [SerializeField]
        UnityEvent _OnClick;

        SpriteRenderer _SpriteRenderer;
        Rigidbody _Rigidbody;

        DropType _DropType;
            
        double _Value;

        bool _IsClicked = false;

        public bool isClicked => _IsClicked;

        void Awake() 
        {
            _SpriteRenderer = GetComponent<SpriteRenderer>();
            _Rigidbody= GetComponent<Rigidbody>();
        }

        void Update() 
        {
            transform.rotation = Quaternion.LookRotation(transform.position - Camera.main.transform.position, Camera.main.transform.up);
        }

        void FixedUpdate() 
        {
            Vector2 velocity2d = new Vector2(_Rigidbody.velocity.x, _Rigidbody.velocity.z);

            velocity2d = Vector2.MoveTowards(velocity2d, Vector2.zero, _SlowdownDeltaValue * Time.fixedDeltaTime);

            _Rigidbody.velocity = new Vector3(velocity2d.x, _Rigidbody.velocity.y, velocity2d.y);
        }

        WelfareValue AddValue(WelfareValue welfareValue)
        {
            welfareValue.value += (float)_Value;

            return welfareValue;
        }

        IEnumerator AlphaShift()
        {
            while(_SpriteRenderer.color != Color.clear)
            {
                _SpriteRenderer.color = Color.Lerp(_SpriteRenderer.color, Color.clear, _AlphaShiftDeltaValue * Time.deltaTime);

                yield return new WaitForEndOfFrame();
            }
            
            gameObject.SetActive(false);
        }

        public void SetDrop(DropType dropType, Sprite sprite, double value, Vector3 spawnPoint)
        {
            _DropType = dropType;

            _SpriteRenderer.sprite = sprite;
            _SpriteRenderer.color = Color.white;

            _Value = value;

            _IsClicked = false;
            
            _Rigidbody.useGravity = true;
            _Rigidbody.position = spawnPoint;

            _Rigidbody.AddForce(Random.insideUnitSphere * 5, ForceMode.Impulse);
        }

        public void OnClick()
        {
            _IsClicked = true;

            _Rigidbody.useGravity = false;

            _Rigidbody.velocity = Vector3.up * _FloatSpeed;

            _OnClick?.Invoke();

            StartCoroutine(AlphaShift());

            switch(_DropType)
            {
                case DropType.Social:
                PlayerStatManager.instance.currentSocialWelfare = AddValue(PlayerStatManager.instance.currentSocialWelfare);
                break;
                
                case DropType.Happiness:
                PlayerStatManager.instance.currentHappinessWelfare = AddValue(PlayerStatManager.instance.currentHappinessWelfare);
                break;
                
                case DropType.Health:
                PlayerStatManager.instance.currentHealthWelfare = AddValue(PlayerStatManager.instance.currentHealthWelfare);
                break;
                
                case DropType.Hunger:
                PlayerStatManager.instance.currentHungerWelfare = AddValue(PlayerStatManager.instance.currentHungerWelfare);
                break;
                
                
                case DropType.Money:
                PlayerStatManager.instance.currentMoney += _Value;
                break;
            }
        }
    }
}