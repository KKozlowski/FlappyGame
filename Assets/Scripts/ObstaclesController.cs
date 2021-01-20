using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Flapper
{
    using Signals;
    public class ObstaclesController : MonoBehaviour
    {
        [SerializeField] private WorldRoot root;
        [SerializeField] private PipePair obstaclePrefab;
        [SerializeField] private Transform scorePoint;
        [SerializeField] private float minY, maxY, distanceBetweenPipes;
        [SerializeField] private Transform spawnPoint, despawnPoint;
        [SerializeField] private SlideSprite groundAnimation;

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
            if (groundAnimation)
                groundAnimation.speed = 0;
        }

        private void SpawnPipe()
        {
            Vector3 spawnPosition = spawnPoint.position;
            if (usedPipes.Count != 0)
            {
                var last = GetLast();
                spawnPosition = last.transform.position + Vector3.right * distanceBetweenPipes;
                spawnPosition.y += UnityEngine.Random.Range(0.4f, 1.4f) * (UnityEngine.Random.value > 0.5f ? 1 : -1);
                spawnPosition.y = Mathf.Clamp(spawnPosition.y, minY, maxY);
            }

            var pipe = pipePool.First();
            pipe.Setup(this, spawnPosition);
            pipe.gameObject.SetActive(true);

            pipePool.Remove(pipe);
            usedPipes.Add(pipe);
        }

        private PipePair GetLast()
        {
            return usedPipes[usedPipes.Count - 1];
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
            DestroyPipesBetween(float.NegativeInfinity, despawnPoint.position.x);
        }

        public void DestroyPipesBetween(float minX, float maxX)
        {
            var toDespawn = new List<PipePair>();
            foreach (var p in usedPipes)
            {
                if (p.ScoringPosition.x > minX && p.StartPosition.x < maxX)
                    toDespawn.Add(p);
            }
            foreach (var p in toDespawn)
            {
                DespawnPipe(p);
            }
        }
    }
}