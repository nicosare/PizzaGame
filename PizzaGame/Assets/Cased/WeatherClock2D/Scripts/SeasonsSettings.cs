using System;

[Serializable]
public class SeasonsSettings
{
    public Season spring;
    public Season summer;
    public Season autumn;
    public Season winter;

    public Season GetSpring() { return spring; }
    public Season GetSummer() { return summer; }
    public Season GetAutumn() { return autumn; }
    public Season GetWinter() { return winter; }

    public SeasonsSettings(Season spring, Season summer, Season autumn, Season winter)
    {
        this.spring = spring;
        this.summer = summer;
        this.autumn = autumn;
        this.winter = winter;
    }

}