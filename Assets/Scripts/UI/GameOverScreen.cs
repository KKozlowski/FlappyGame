using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Flapper.UI
{
    using System;
    using Signals;
    public class GameOverScreen : MonoBehaviour
    {
        [SerializeField] private GameObject popup;
        [SerializeField] private Text scoreDisplay;
        [SerializeField] private GameObject highScoreIndicator;

        [SerializeField] private LeaderboardScreen leaderboard;

        private int scoreToDisplay = 0;

        private void Start()
        {
            SignalMachine.AddListener<DeathSignal>(OnDeath);
            SignalMachine.AddListener<NewScoreSignal>(OnScoreChange);
            SignalMachine.AddListener<NewLeaderboardScoreSignal>(OnHighScore);
        }

        private void OnHighScore(NewLeaderboardScoreSignal obj)
        {
            highScoreIndicator.SetActive(true);
        }

        private void OnScoreChange(NewScoreSignal obj)
        {
            scoreToDisplay = obj.Score;
        }

        private void OnDeath(DeathSignal obj)
        {
            StartCoroutine(ShowWithDelay(1f));
        }

        private void OnDestroy()
        {
            SignalMachine.RemoveListener<DeathSignal>(OnDeath);
            SignalMachine.RemoveListener<NewScoreSignal>(OnScoreChange);
            SignalMachine.RemoveListener<NewLeaderboardScoreSignal>(OnHighScore);
        }

        private IEnumerator ShowWithDelay(float delay)
        {
            yield return new WaitForSeconds(delay);
            scoreDisplay.text = scoreToDisplay.ToString();
            popup.SetActive(true);
        }

        public void Restart()
        {
            SignalMachine.Call(new RestartSignal());
        }

        public void OpenLeaderboard()
        {
            leaderboard.Show();
        }
    }
}