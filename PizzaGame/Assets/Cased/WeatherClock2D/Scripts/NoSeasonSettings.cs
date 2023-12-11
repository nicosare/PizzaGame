using System;

[Serializable]
public class NoSeasonsSettings
{
    public NoSeason noSeasons;

    public NoSeasonsSettings(NoSeason noSeasons)
    {
        this.noSeasons = noSeasons;
    }

    public NoSeason GetNoSeason() { return this.noSeasons; }

}
