using System;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class Months
{
    [SerializeField] private int numberOfMonths;
    [SerializeField] private List<Month> months;

    public Months(List<Month> months, int numberOfMonths)
    {
        this.numberOfMonths = numberOfMonths;
        this.months = new List<Month>(new Month[numberOfMonths]);
    }

    public List<Month> GetMonths() { return months; }
    public int GetNumberOfMonths() { return numberOfMonths; }
}

