using UnityEngine;

namespace Flapper
{
    [System.Serializable]
    public class LeaderboardEntry
    {
        [SerializeField] private int score;
        public int Score => score;

        public LeaderboardEntry(int score)
        {
            this.score = score;
        }
    }
}

