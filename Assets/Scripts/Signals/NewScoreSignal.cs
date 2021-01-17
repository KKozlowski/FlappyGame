using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Flapper.Signals
{
    public class NewScoreSignal
    {
        public int Score { get; }
        public NewScoreSignal(int score)
        {
            Score = score;
        }
    }
}