using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Flapper
{
    using System;
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
        }

        private void OnDestroy()
        {
            SignalMachine.RemoveListener<SimpleTapSignal>(OnTap);
            SignalMachine.RemoveListener<DeathSignal>(OnDeath);
            SignalMachine.RemoveListener<PointScoredSignal>(OnPoint);
        }

        private void OnPoint(PointScoredSignal obj)
        {
            ++Score;
            SignalMachine.Call(new NewScoreSignal(Score));
        }

        void OnTap(SimpleTapSignal tap)
        {
            if (State == LoopState.PreGame)
            {
                State = LoopState.InGame;
                SignalMachine.Call(new GameStartedSignal());
                SignalMachine.Call(new NewScoreSignal(0));
            }
        }

        void OnDeath(DeathSignal arg)
        {
            if (State == LoopState.InGame)
            {
                State = LoopState.PostGame;
            }
        }
    }
}
