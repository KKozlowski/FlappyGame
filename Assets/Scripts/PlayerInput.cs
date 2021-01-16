using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Flapper
{
    using Signals;
    public class PlayerInput : MonoBehaviour
    {
        [SerializeField] private FlapMovement movement;

        private void Update()
        {
            if (Input.GetMouseButton(0))
            {
                SignalMachine.Call(new SimpleTapSignal());
            }
        }
    }
}