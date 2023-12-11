using System;
using UnityEngine;

[Serializable]
public class Month
{
    [SerializeField] private string monthName;
    [SerializeField] private int daysPerMonth;

    public Month(string monthName, int daysPerMonth)
    {
        this.monthName = monthName;
        this.daysPerMonth = daysPerMonth;
    }

    public string GetName() { return this.monthName; }
    public int GetDays() { return this.daysPerMonth; }

}
