using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Flapper.UI
{
    public class LeaderboardScreen : MonoBehaviour
    {
        [SerializeField] private GameObject popup;
        [SerializeField] private Transform rowsParent;
        [SerializeField] private LeaderboardEntry rowPrefab;
        [SerializeField] private int maxRows = 5;

        private List<LeaderboardEntry> spawnedRows = new List<LeaderboardEntry>();

        public void Show()
        {
            Clear();
            DisplayLeaderboard();
            popup.SetActive(true);
        }

        public void Hide()
        {
            popup.SetActive(false);
        }

        private void Clear()
        {
            foreach(var r in spawnedRows)
            {
                Destroy(r.gameObject);
            }

            spawnedRows.Clear();
        }

        private void DisplayLeaderboard()
        {
            for (int i = 0; i < LeaderboardService.Scores.Count && i < maxRows; ++i)
            {
                var row = SpawnRow();
                row.Show(i + 1, LeaderboardService.Scores[i].Score);
            }
        }

        private LeaderboardEntry SpawnRow()
        {
            var row = Instantiate(rowPrefab, rowsParent);
            spawnedRows.Add(row);
            return row;
        }
    }
}