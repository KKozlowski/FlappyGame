﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Flapper
{
    public class WorldRoot : MonoBehaviour
    {
        [SerializeField] private float speed;
        public float Speed => speed;
    }
}