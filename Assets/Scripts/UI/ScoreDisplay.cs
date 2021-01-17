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

        void Awake()
        {
            display.text = "";
            SignalMachine.AddListener<NewScoreSignal>(OnScore);
        }

        private void OnScore(NewScoreSignal obj)
        {
            display.text = obj.Score.ToString();
        }

        private void OnDestroy()
        {
            SignalMachine.RemoveListener<NewScoreSignal>(OnScore);
        }
    }
}

