using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;

using NeedsVsWants.Patterns;

namespace NeedsVsWants.Player
{
    public class PlayerStatManager : SimpleSingleton<PlayerStatManager>
    {
        PlayerStat _PlayerStat;

        public float currentMoney
        {
            get => _PlayerStat.currentMoney;

            set
            {
                _PlayerStat.currentMoney = value;

                onMoneyChange?.Invoke(value);
            }
        }

        public DateTime currentDateTime
        {
            get => _PlayerStat.currentDateTime;

            set
            {
                _PlayerStat.currentDateTime = value;

                onDateChange?.Invoke(value);
            }
        }

        public event Action<float> onMoneyChange;
        public event Action<DateTime> onDateChange;

        protected override void Awake()
        {
            base.Awake();

            _PlayerStat = PlayerStat.instance;
        }
    }
}