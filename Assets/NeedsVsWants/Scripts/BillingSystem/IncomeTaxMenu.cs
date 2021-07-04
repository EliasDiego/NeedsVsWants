using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using NeedsVsWants.CalendarSystem;

namespace NeedsVsWants.BillingSystem
{
    public class IncomeTaxMenu : BillMenu
    {
        [SerializeField]
        IncomeEvent _JobIncome;
        [SerializeField]
        SSSMenu _SSSMenu;
        [SerializeField]
        PhilHealthMenu _PhilHealthMenu;
        [SerializeField]
        PagibigFundMenu _PagibigFundMenu;

        protected override string billEventName => "Income Tax";

        double TrainTaxRate(double income)
        {
            double newIncome = 0;

            if(250000 >= income)
                newIncome = income;

            else if(250000 < income && income <= 400000)
                newIncome = (income - 250000f) * 0.2f;

            else if(400000 < income && income <= 800000)
                newIncome = (income - 400000) * 0.25f + 30000;

            else if(800000 < income && income <= 2000000)
                newIncome = (income - 800000) * 0.3f + 130000;

            else if(2000000 < income && income <= 8000000)
                newIncome = (income - 2000000) * 0.32f + 490000;

            else
                newIncome = (income - 8000000) * 0.35f + 2410000;

            return newIncome;
        }

        public override bool IsWithinDate(DateTime dateTime)
        {
            return DateTime.DaysInMonth(dateTime.Year, dateTime.Month) == dateTime.Day;
        }

        public override double CalculateBill(DateTime dateTime)
        {
            double deductions = _SSSMenu.CalculateBill(dateTime) + _PhilHealthMenu.CalculateBill(dateTime) + _PagibigFundMenu.CalculateBill(dateTime);
            double annualTaxableIncome = (_JobIncome.incomeRate * 12) + _JobIncome.GetThirteenthMonthPay() - ((deductions * 12) + _JobIncome.GetThirteenthMonthPay());

            return TrainTaxRate(annualTaxableIncome) / 12;
        }

        protected override void OnReturn()
        {
            
        }

        protected override void OnSwitchFrom()
        {
            
        }
    }
}