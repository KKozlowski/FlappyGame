using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Flapper
{
    public static class LeaderboardService
    {
        public const string contentPrefKey = "Leaderboard";

        private static LeaderboardContent content = null;

        public static IReadOnlyList<LeaderboardEntry> Scores => content.Scores;

        static LeaderboardService()
        {
            ReadLeaderboard();
        }

        private static void ReadLeaderboard()
        {
            var contentString = PlayerPrefs.GetString(contentPrefKey, "");
            if (!string.IsNullOrEmpty(contentString))
            {
                content = JsonUtility.FromJson<LeaderboardContent>(contentString);
            }
            else
            {
                content = new LeaderboardContent();
            }
        }

        private static void WriteLeaderboard()
        {
            var contentString = JsonUtility.ToJson(content);
            PlayerPrefs.SetString(contentPrefKey, contentString);
        }

        public static bool TryAdd(LeaderboardEntry entry)
        {
            if (content.TryAdd(entry))
            {
                WriteLeaderboard();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}

