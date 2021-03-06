﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Flapper
{
    using Signals;
    public class PipePair : MonoBehaviour
    {
        [SerializeField] private Transform scorePoint;
        [SerializeField] private Transform startPoint;
        private ObstaclesController controller;

        public bool Scored { get; private set; } = false;
        public Vector3 StartPosition => startPoint.position;
        public Vector3 ScoringPosition => scorePoint.position;

        public void Setup(ObstaclesController controller, Vector3 position)
        {
            Scored = false;
            this.controller = controller;
            transform.position = position;
        }

        public void Move(Vector3 delta)
        {
            transform.position += delta;
            if (!Scored && scorePoint.position.x < controller.ScoringPoint.x)
            {
                Scored = true;
                SignalMachine.Call(new PointScoredSignal(this));
            }
        }
    }
}