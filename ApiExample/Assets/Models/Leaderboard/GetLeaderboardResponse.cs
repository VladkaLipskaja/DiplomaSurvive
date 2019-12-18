using System;
using UnityEngine.Serialization;

[Serializable]
public class GetLeaderboardResponse
{
    public int place;

   public LeaderboardPlayer[] players;

    [Serializable]
    public class LeaderboardPlayer
    {
        public int place;

        public string name;

        public int score;
    }
}