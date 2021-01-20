using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Flapper.UI
{
    using System;
    using Signals;
    public class ScoreDisplay : MonoBehaviour
    {
        [SerializeField] private Text display;
        [SerializeField] private GameObject content;

        private void Awake()
        {
            content.SetActive(false);
            SignalMachine.AddListener<NewScoreSignal>(OnScore);
        }

        private void OnScore(NewScoreSignal obj)
        {
            content.SetActive(true);
            display.text = obj.Score.ToString();
        }

        private void OnDestroy()
        {
            SignalMachine.RemoveListener<NewScoreSignal>(OnScore);
        }
    }
}

