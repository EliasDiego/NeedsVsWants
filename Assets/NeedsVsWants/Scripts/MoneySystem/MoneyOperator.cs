using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NeedsVsWants.MoneySystem
{
    [System.Serializable]
    public class MoneyOperator
    {
        [SerializeField]
        AmountType _AmountType;
        [SerializeField]
        double _Value;

        public double CalculateMoney(double currentMoney)
        {
            double newMoney = 0;

            switch(_AmountType)
            {
                case AmountType.Value:
                    newMoney += _Value;
                    break;
                
                case AmountType.Percentage:
                    newMoney = currentMoney + currentMoney * (_Value / 100);
                    break;

                case AmountType.NewValue:
                    newMoney = _Value;
                    break;
            }

            return newMoney;
        }
    }
}