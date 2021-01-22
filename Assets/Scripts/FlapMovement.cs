using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Flapper
{
    using Signals;

    public enum MovementMode
    {
        None = 0,
        Idle = 1,
        Control = 2,
        Death = 3
    }

    public class FlapMovement : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D body;
        [SerializeField] private Vector3 flapVelocity = new Vector3(0, 5, 0);
        [SerializeField] private Transform flapPivot;
        private float timeToUnflap = 0;

        private const float FlapAnimationTime = 0.3f;

        public MovementMode CurrentMode { get; private set; } = MovementMode.None;

        private void Start()
        {
            SetMode(MovementMode.Idle);

            SignalMachine.AddListener<GameStartedSignal>(OnGameStarted);
            SignalMachine.AddListener<SimpleTapSignal>(OnTap);
            SignalMachine.AddListener<DeathSignal>(OnDeath);
        }

        private void Update()
        {
            timeToUnflap -= Time.deltaTime;
            if (timeToUnflap < 0)
                Unflap();
        }

        private void OnDestroy()
        {
            SignalMachine.RemoveListener<GameStartedSignal>(OnGameStarted);
            SignalMachine.RemoveListener<SimpleTapSignal>(OnTap);
            SignalMachine.RemoveListener<DeathSignal>(OnDeath);
        }

        private void OnGameStarted(GameStartedSignal arg)
        {
            SetMode(MovementMode.Control);
            Flap();
        }

        private void OnTap(SimpleTapSignal arg)
        {
            if (CurrentMode == MovementMode.Control)
                Flap();
        }

        private void OnDeath(DeathSignal arg)
        {
            SetMode(MovementMode.Death);
        }

        public void SetMode(MovementMode mode)
        {
            if (mode == CurrentMode)
                return;

            switch (mode)
            {
                case MovementMode.Control:
                    body.isKinematic = false;
                    break;

                case MovementMode.Idle:
                    body.isKinematic = true;
                    break;

                case MovementMode.Death:
                    body.isKinematic = false;
                    break;
            }

            CurrentMode = mode;
        }

        public void Flap()
        {
            body.velocity = flapVelocity;
            flapPivot.rotation = Quaternion.Euler(0, 0, 180);
            timeToUnflap = FlapAnimationTime;
        }

        private void Unflap()
        {
            flapPivot.rotation = Quaternion.identity;
        }
    }
}