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
        [SerializeField] private GameObject content;

        private void Awake()
        {
            content.SetActive(false);
            SignalMachine.AddListener<NewBombCountSignal>(OnBombCount);
            SignalMachine.AddListener<NewScoreSignal>(OnScore);
        }

        private void OnScore(NewScoreSignal obj)
        {
            content.SetActive(true);
        }

        private void OnDestroy()
        {
            SignalMachine.RemoveListener<NewBombCountSignal>(OnBombCount);
            SignalMachine.RemoveListener<NewScoreSignal>(OnScore);
        }

        private void OnBombCount(NewBombCountSignal obj)
        {
            display.text = obj.Count.ToString();
        }
    }
}

