using System.Collections.Generic;

[System.Serializable]

public record Leaderboards
{
    public PlayersLeaderboard[] leaderboard;
}

[System.Serializable]

    public record PlayersLeaderboard
    {
        public int placeInTop;
        public string login;
        public int rating;
    }
