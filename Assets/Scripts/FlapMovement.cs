using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Flapper
{
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

        public MovementMode CurrentMode { get; private set; } = MovementMode.None;

        private void Start()
        {
            SetMode(MovementMode.Idle);
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
        }
    }
}