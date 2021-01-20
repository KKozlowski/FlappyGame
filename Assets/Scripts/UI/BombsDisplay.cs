using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Flapper.UI
{
    using Signals;
    public class BombsDisplay : MonoBehaviour
    {
        [SerializeField] private Text display;

        private void Awake()
        {
            SignalMachine.AddListener<NewBombCountSignal>(OnBombCount);
        }

        private void OnDestroy()
        {
            SignalMachine.RemoveListener<NewBombCountSignal>(OnBombCount);
        }

        private void OnBombCount(NewBombCountSignal obj)
        {
            display.text = obj.Count.ToString();
        }
    }
}

