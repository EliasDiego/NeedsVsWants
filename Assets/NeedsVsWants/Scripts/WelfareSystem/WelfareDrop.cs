using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;

using NeedsVsWants.Player;

namespace NeedsVsWants.WelfareSystem
{
    public class WelfareDrop : MonoBehaviour
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

        WelfareDropManager.WelfareType _WelfareType;
            

        float _Value;

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
            welfareValue.value += _Value;

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

        public void SetDrop(WelfareDropManager.WelfareType welfareType, Sprite sprite, float value, Vector3 spawnPoint)
        {
            _WelfareType = welfareType;

            _SpriteRenderer.sprite = sprite;

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

            switch(_WelfareType)
            {
                case WelfareDropManager.WelfareType.Social:
                PlayerStatManager.instance.currentHappinessWelfare = AddValue(PlayerStatManager.instance.currentSocialWelfare);
                break;
                
                case WelfareDropManager.WelfareType.Happiness:
                PlayerStatManager.instance.currentHappinessWelfare = AddValue(PlayerStatManager.instance.currentHappinessWelfare);
                break;
                
                case WelfareDropManager.WelfareType.Health:
                PlayerStatManager.instance.currentHappinessWelfare = AddValue(PlayerStatManager.instance.currentHealthWelfare);
                break;
                
                case WelfareDropManager.WelfareType.Hunger:
                PlayerStatManager.instance.currentHappinessWelfare = AddValue(PlayerStatManager.instance.currentHungerWelfare);
                break;
            }
        }
    }
}