using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Flapper.UI
{
    using System;
    using Signals;
    public class GameOverScreen : MonoBehaviour
    {
        [SerializeField] private GameObject popup;

        private void Start()
        {
            SignalMachine.AddListener<DeathSignal>(OnDeath);
        }

        private void OnDeath(DeathSignal obj)
        {
            StartCoroutine(ShowWithDelay(1f));
        }

        private void OnDestroy()
        {
            SignalMachine.RemoveListener<DeathSignal>(OnDeath);
        }

        IEnumerator ShowWithDelay(float delay)
        {
            yield return new WaitForSeconds(delay);
            popup.SetActive(true);
        }

        public void Restart()
        {
            SignalMachine.Call(new RestartSignal());
        }
    }
}