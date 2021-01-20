using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Flapper
{
    using Signals;

    public enum LoopState
    {
        PreGame,
        InGame,
        PostGame
    }

    public class GameLoop : MonoBehaviour
    {
        public LoopState State { get; private set; } = LoopState.PreGame;
        public int Score { get; private set; } = 0;

        private void Start()
        {
            SignalMachine.AddListener<SimpleTapSignal>(OnTap);
            SignalMachine.AddListener<DeathSignal>(OnDeath);
            SignalMachine.AddListener<PointScoredSignal>(OnPoint);
            SignalMachine.AddListener<RestartSignal>(OnRestartCalled);
        }

        private void OnRestartCalled(RestartSignal obj)
        {
            Restart();
        }

        private void OnDestroy()
        {
            SignalMachine.RemoveListener<SimpleTapSignal>(OnTap);
            SignalMachine.RemoveListener<DeathSignal>(OnDeath);
            SignalMachine.RemoveListener<PointScoredSignal>(OnPoint);
            SignalMachine.RemoveListener<RestartSignal>(OnRestartCalled);
        }

        private void Restart()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(1);
        }

        private void OnPoint(PointScoredSignal obj)
        {
            ++Score;
            SignalMachine.Call(new NewScoreSignal(Score));
        }

        private void OnTap(SimpleTapSignal tap)
        {
            if (State == LoopState.PreGame)
            {
                State = LoopState.InGame;
                SignalMachine.Call(new GameStartedSignal());
                SignalMachine.Call(new NewScoreSignal(0));
            }
        }

        private void OnDeath(DeathSignal arg)
        {
            if (State == LoopState.InGame)
            {
                State = LoopState.PostGame;
                if(LeaderboardService.TryAdd(new LeaderboardEntry(Score)))
                {
                    SignalMachine.Call(new NewLeaderboardScoreSignal());
                    Debug.Log("On leaderboard");
                }
            }
        }
    }
}
