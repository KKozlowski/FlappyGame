using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Flapper.Signals
{
    public class PointScoredSignal
    {
        public Object Source { get; }

        public PointScoredSignal(Object source)
        {
            Source = source;
        }
    }
}