using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalendarTesting : MonoBehaviour
{
    [SerializeField]
    int _Century;
    [SerializeField]
    int _Year;
    [SerializeField]
    int _Month;
    [SerializeField]
    int _Day;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    int GetWeekOfMonth(int year, int month, int day)
    {
        System.DateTime dateTime = new System.DateTime(year, month, 1);

        return Mathf.CeilToInt(((int)dateTime.DayOfWeek + day) / 7f);
    }
}
