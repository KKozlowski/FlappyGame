using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Flapper
{
    using Signals;
    public class BombSystem : MonoBehaviour
    {
        public int Count { get; private set; }

        private int pointsCounted = 0;
        public const int PointsToBomb = 10;
        public const int MaxBombs = 3;

        private void Awake()
        {
            SignalMachine.AddListener<PointScoredSignal>(OnPointScored);
            SignalMachine.AddListener<DoubleTapSignal>(OnDoubleTap);
        }

        private void OnDestroy()
        {
            SignalMachine.RemoveListener<PointScoredSignal>(OnPointScored);
            SignalMachine.RemoveListener<DoubleTapSignal>(OnDoubleTap);
        }

        private void Start()
        {
            SignalMachine.Call(new NewBombCountSignal(Count));
        }

        private void OnDoubleTap(DoubleTapSignal obj)
        {
            Explode();
        }

        private void OnPointScored(PointScoredSignal obj)
        {
            ++pointsCounted;
            if (pointsCounted >= PointsToBomb)
            {
                pointsCounted -= PointsToBomb;
                ++Count;
                if (Count > MaxBombs)
                    Count = MaxBombs;
                else
                    SignalMachine.Call(new NewBombCountSignal(Count));
            }
        }

        private void Explode()
        {
            if (Count == 0)
                return;

            --Count;
            SignalMachine.Call(new NewBombCountSignal(Count));
        }
    }
}