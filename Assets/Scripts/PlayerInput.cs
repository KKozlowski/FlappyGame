using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Flapper
{
    using Signals;
    public class PlayerInput : MonoBehaviour
    {
        [SerializeField] private FlapMovement movement;
        private float timeSinceLastTap = 0;

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                SignalMachine.Call(new SimpleTapSignal());
                if (timeSinceLastTap < 0.2f)
                {
                    SignalMachine.Call(new DoubleTapSignal());
                }
                timeSinceLastTap = 0;
            } else
            {
                timeSinceLastTap += Time.deltaTime;
            }
        }
    }
}