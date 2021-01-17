using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Flapper
{
    using System;
    using Signals;
    public class ObstaclesController : MonoBehaviour
    {
        [SerializeField] private WorldRoot root;
        [SerializeField] private PipePair obstaclePrefab;
        [SerializeField] private Transform scorePoint;
        [SerializeField] private float minY, maxY, distanceBetweenPipes;
        [SerializeField] private Transform spawnPoint, despawnPoint;

        private HashSet<PipePair> pipePool = new HashSet<PipePair>();
        private List<PipePair> usedPipes = new List<PipePair>();

        public Vector3 ScoringPoint => scorePoint.position;

        private bool ongoing = false;

        private void Start()
        {
            SignalMachine.AddListener<GameStartedSignal>(OnGameStart);
            SignalMachine.AddListener<DeathSignal>(OnGameEnd);

            for (int i = 0; i < 20; ++i)
            {
                var obst = Instantiate(obstaclePrefab, transform);
                obst.gameObject.SetActive(false);
                pipePool.Add(obst);
            }
        }

        private void OnDestroy()
        {
            SignalMachine.RemoveListener<GameStartedSignal>(OnGameStart);
            SignalMachine.AddListener<DeathSignal>(OnGameEnd);
        }

        private void OnGameStart(GameStartedSignal obj)
        {
            ongoing = true;
            SpawnPipe();
        }

        private void OnGameEnd(DeathSignal obj)
        {
            ongoing = false;
        }

        private void SpawnPipe()
        {
            Vector3 spawnPosition = spawnPoint.position;
            if (usedPipes.Count != 0)
                spawnPosition = usedPipes[usedPipes.Count - 1].transform.position + Vector3.right * distanceBetweenPipes;

            var pipe = pipePool.First();
            pipe.Setup(this, spawnPosition);
            pipe.gameObject.SetActive(true);

            pipePool.Remove(pipe);
            usedPipes.Add(pipe);
        }

        private void DespawnPipe(PipePair pipe)
        {
            pipePool.Add(pipe);
            usedPipes.Remove(pipe);
            pipe.gameObject.SetActive(false);
        }

        private void FixedUpdate()
        {
            if (!ongoing)
                return;

            MovePipes();
            DespawnOldPipes();
            if (usedPipes.Count == 0 || spawnPoint.position.x - usedPipes[usedPipes.Count - 1].transform.position.x > distanceBetweenPipes)
                SpawnPipe();
        }

        private void MovePipes()
        {
            foreach (var p in usedPipes)
            {
                p.Move(new Vector3(-Time.fixedDeltaTime * root.Speed, 0, 0));
            }
        }

        private void DespawnOldPipes()
        {
            var toDespawn = new List<PipePair>();
            foreach (var p in usedPipes)
            {
                if (p.transform.position.x < despawnPoint.position.x)
                    toDespawn.Add(p);
            }
            foreach (var p in toDespawn)
            {
                DespawnPipe(p);
            }
        }
    }
}