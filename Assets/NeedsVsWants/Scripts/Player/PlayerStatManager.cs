using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

using NeedsVsWants.Patterns;
using NeedsVsWants.WelfareSystem;
using NeedsVsWants.CalendarSystem;
using NeedsVsWants.ShoppingSystem;
using NeedsVsWants.MessagingSystem;

namespace NeedsVsWants.Player
{
    public class PlayerStatManager : SimpleSingleton<PlayerStatManager>
    {
        [SerializeField]
        int _EndMenuBuildSceneIndex;

        PlayerStat _PlayerStat;

        public DateTime currentDate 
        { 
            get => _PlayerStat.currentDateTime; 
            set
            {
                _PlayerStat.currentDateTime = value;

                onDateChange?.Invoke(_PlayerStat.currentDateTime);
            }
        }

        public CalendarEvent[] calendarEvents => _PlayerStat.calendarEventList.ToArray();

        public double currentMoney
        {
            get => _PlayerStat.currentMoney;

            set
            {
                _PlayerStat.currentMoney = value;

                onMoneyChange?.Invoke(value);
            }
        }

        // public int currentYear 
        // { 
        //     get => _PlayerStat.currentDateTime.Year;
        //     set
        //     {
        //         DateTime newDate = _PlayerStat.currentDateTime;

        //         _PlayerStat.currentDateTime = new DateTime(value, newDate.Month, newDate.Day);

        //         onDateChange?.Invoke(_PlayerStat.currentDateTime);
        //     }
        // }
        // public int currentMonth 
        // {
        //     get => _PlayerStat.currentDateTime.Month;
        //     set
        //     {
        //         DateTime newDate = _PlayerStat.currentDateTime;

        //         _PlayerStat.currentDateTime = new DateTime(newDate.Year, value, newDate.Day);

        //         onDateChange?.Invoke(_PlayerStat.currentDateTime);
        //     }
        // }
        // public int currentDay
        // {
        //     get => _PlayerStat.currentDateTime.Day;
        //     set
        //     {
        //         DateTime newDate = _PlayerStat.currentDateTime;

        //         _PlayerStat.currentDateTime = new DateTime(newDate.Year, newDate.Month, value);

        //         onDateChange?.Invoke(_PlayerStat.currentDateTime);
        //     }
        // }

        public WelfareValue currentHealthWelfare
        {
            get => _PlayerStat.healthWelfare; 
            set
            {
                _PlayerStat.healthWelfare = value;

                onHealthChange?.Invoke(_PlayerStat.healthWelfare);
            }
        }
        
        public WelfareValue currentSocialWelfare
        {
            get => _PlayerStat.socialWelfare; 
            set
            {
                _PlayerStat.socialWelfare = value;

                onSocialChange?.Invoke(_PlayerStat.socialWelfare);
            }
        }
       
        public WelfareValue currentHungerWelfare
        {
            get => _PlayerStat.hungerWelfare; 
            set
            {
                _PlayerStat.hungerWelfare = value;

                onHungerChange?.Invoke(_PlayerStat.hungerWelfare);
            }
        }
        
        public WelfareValue currentHappinessWelfare
        {
            get => _PlayerStat.happinessWelfare; 
            set
            {
                _PlayerStat.happinessWelfare = value;

                onHappinessChange?.Invoke(_PlayerStat.happinessWelfare);
            }
        }

        public Chat[] chats => _PlayerStat.chatList.ToArray();

        public Item[] ShopItems => _PlayerStat.ShopItemList.ToArray();

        public event Action<double> onMoneyChange;

        public event Action<DateTime> onDateChange;

        public event Action<WelfareValue> onHealthChange;
        public event Action<WelfareValue> onHungerChange;
        public event Action<WelfareValue> onHappinessChange;
        public event Action<WelfareValue> onSocialChange;

        public event Action<Chat> onAddChat;

        public event Action<Item[]> onAddItems;
        public event Action<Item[]> onEditItems;
        public event Action<Item[]> onRemoveItems;

        protected override void Awake()
        {
            base.Awake();

            _PlayerStat = PlayerStat.instance;
        }

        void Start() 
        {
            // onMoneyChange?.Invoke(currentMoney);

            // onDateChange?.Invoke(currentDate);

            // onHealthChange?.Invoke(currentHealthWelfare);
            // onHappinessChange?.Invoke(currentHappinessWelfare);
            // onHungerChange?.Invoke(currentHungerWelfare);
            // onSocialChange?.Invoke(currentSocialWelfare);  

            onHealthChange += welfareValue =>
            {
                if(welfareValue.value <= 0)
                    LoadEndMenuScene();
            };
            
            onHungerChange += welfareValue =>
            {
                if(welfareValue.value <= 0)
                    LoadEndMenuScene();
            };
            
            onHappinessChange += welfareValue =>
            {
                if(welfareValue.value <= 0)
                    LoadEndMenuScene();
            };
            
            onSocialChange += welfareValue =>
            {
                if(welfareValue.value <= 0)
                    LoadEndMenuScene();
            };
        }

        public void LoadEndMenuScene()
        {
            SceneManager.LoadScene(_EndMenuBuildSceneIndex, LoadSceneMode.Single);
        }

        public void AddConversationToChat(Conversation conversation)
        {
            Chat chat = new Chat();

            chat.conversation = conversation;

            _PlayerStat.chatList.Add(chat);
                
            onAddChat?.Invoke(chat);
        }

        public void CalculateWelfare(WelfareOperator welfareOperator)
        {
            currentHappinessWelfare = welfareOperator.GetHappiness(currentHappinessWelfare);
            currentHealthWelfare = welfareOperator.GetHappiness(currentHealthWelfare);
            currentHungerWelfare = welfareOperator.GetHappiness(currentHungerWelfare);
            currentSocialWelfare = welfareOperator.GetHappiness(currentSocialWelfare);
        }

        #region Shop Item
        public void AddShopItem(params Item[] items)
        {
            _PlayerStat.ShopItemList.AddRange(items);
            
            onAddItems?.Invoke(items);
        }

        public void RemoveShopItem(params Item[] items)
        {
            _PlayerStat.ShopItemList.RemoveAll(item => items.Contains(item));

            onRemoveItems?.Invoke(items);
        }

        public void EditItem(Func<Item, bool> predicate)
        {
            if(predicate != null)
                onEditItems?.Invoke(_PlayerStat.ShopItemList.Where(item => predicate.Invoke(item)).ToArray());
        }
        #endregion
    }
}