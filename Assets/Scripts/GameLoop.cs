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

        private void Start()
        {
            SignalMachine.AddListener<SimpleTapSignal>(OnTap);
            SignalMachine.AddListener<DeathSignal>(OnDeath);
        }

        private void OnDestroy()
        {
            SignalMachine.RemoveListener<SimpleTapSignal>(OnTap);
            SignalMachine.RemoveListener<DeathSignal>(OnDeath);
        }

        void OnTap(SimpleTapSignal tap)
        {
            if (State == LoopState.PreGame)
            {
                State = LoopState.InGame;
                SignalMachine.Call(new GameStartedSignal());
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
