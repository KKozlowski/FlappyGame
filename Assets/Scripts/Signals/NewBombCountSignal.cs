using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Flapper.Signals
{
    public class NewBombCountSignal
    {
        public int Count { get; }
        public NewBombCountSignal(int count)
        {
            Count = count;
        }
    }
}