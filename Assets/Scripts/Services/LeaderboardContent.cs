using System.Collections.Generic;
using UnityEngine;

namespace Flapper
{
    [System.Serializable]
    public class LeaderboardContent
    {
        [SerializeField] private List<LeaderboardEntry> scores = new List<LeaderboardEntry>();
        public IReadOnlyList<LeaderboardEntry> Scores => scores;

        public const int MaxSize = 5;

        public LeaderboardContent()
        {

        }

        public bool TryAdd(LeaderboardEntry entry)
        {
            if (scores.Count < MaxSize || entry.Score > LastOrDefault().Score)
            {
                Add(entry);
                return true;
            }
            return false;
        }

        public void Add(LeaderboardEntry entry)
        {
            if (scores.Count == 0)
            {
                scores.Add(entry);
                return;
            }

            bool added = false;
            for (int i = 0; i < scores.Count; ++i)
            {
                if (entry.Score > scores[i].Score)
                {
                    scores.Insert(i, entry);
                    added = true;
                    break;
                }
            }

            if (!added && scores.Count < MaxSize)
                scores.Add(entry);

            if (scores.Count > MaxSize)
            {
                scores.RemoveRange(MaxSize, scores.Count - MaxSize);
            }
        }

        public LeaderboardEntry LastOrDefault()
        {
            if (scores.Count == 0)
                return null;
            return scores[scores.Count - 1];
        }
    }
}

