using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using NeedsVsWants.Player;
using NeedsVsWants.MoneySystem;
using NeedsVsWants.WelfareSystem;
using NeedsVsWants.CalendarSystem;
using NeedsVsWants.MessagingSystem;

namespace NeedsVsWants.MoneySystem
{
    [CreateAssetMenu(menuName = "NeedsVsWants/Messages/Promotion Event")]
    public class PromotionEvent : MessageEvent
    {
        Date _PromotionDate;

        void OnWelfareChange(WelfareValue welfare)
        {
            if((welfare.value / welfare.maxValue) < 0.3f)
                _PromotionDate = (Date)PlayerStatManager.instance.currentDate;
        }

        public override void Initialize()
        {
            // _PromotionDate = (Date)PlayerStatManager.instance.currentDate;

            PlayerStatManager.instance.onHappinessChange += OnWelfareChange;
            PlayerStatManager.instance.onSocialChange += OnWelfareChange;

            _PromotionDate = Resources.Load<PlayerStatStartReference>("Player Stat Start Reference").startDate;
        }

        public override bool IsWithinDate(DateTime dateTime)
        {
            return dateTime.IsOnSameDay(_PromotionDate, false) && _PromotionDate.year < dateTime.Year;
        }
    }
}