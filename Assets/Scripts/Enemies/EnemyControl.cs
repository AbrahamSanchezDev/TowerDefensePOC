using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

namespace WorldsDev
{
    public class EnemyControl : MonoBehaviour
    {
        private int _minSpawns = 10;
        private float _modPerLevel = 0.3f;

        private int _curSpawns, _curMaxSpawn, _curDeaths;

        public static UnityEvent<EnemyAI> OnDeathEvent = new UnityEvent<EnemyAI>();

        public static EnemyControl Instance;

        protected void Awake()
        {
            Instance = this;
            Setup();
        }

        protected void Setup()
        {
        }

        protected void OnEnable()
        {
            GameControl.OnGameStarStateEvent.AddListener(OnGameStart);
            OnDeathEvent.AddListener(OnDeath);
        }

        protected void OnDisable()
        {
            GameControl.OnGameStarStateEvent.RemoveListener(OnGameStart);
            OnDeathEvent.RemoveListener(OnDeath);
        }

        private int TotalToSpawn()
        {
            return _minSpawns + (int) (_modPerLevel * GameControl.Level);
        }

        private void OnGameStart(GameState state)
        {
            switch (state)
            {
                case GameState.Idle:
                    break;
                case GameState.StartedPlaying:
                    _curMaxSpawn = TotalToSpawn();
                    _curSpawns = 0;
                    _curDeaths = 0;
                    StartCoroutine(nameof(SpawnEnemies));
                    break;
                case GameState.Pause:
                    break;
                case GameState.Win:
                    break;
                case GameState.Lost:
                    break;
            }
        }

        private IEnumerator SpawnEnemies()
        {
            while (_curSpawns < _curMaxSpawn)
            {
                var spawnAmount = Random.Range(0, 2 + GameControl.Level);
                for (var i = 0; i < spawnAmount; i++)
                {
                    if (GameControl.CurGameState == GameState.StartedPlaying)
                    {
                        SpawnRandomEnemy();
                        //Stop if already spawned max amount
                        if (_curSpawns >= _curMaxSpawn) break;
                    }
                   
                    //Wait for spawning next enemy
                    yield return new WaitForSeconds(0.2f);
                }

                //Wait for the next wave of spawns
                yield return new WaitForSeconds(Random.Range(1f, 3f));
            }

            yield return null;
        }

        private void SpawnRandomEnemy()
        {
            _curSpawns++;
            var db = PrefabsRef.Prefabs.EnemyDB;

            var spawnIndex = Random.Range(0, db.SummonObjs.Count - 1);

            var mob = db.SummonObjs[spawnIndex];
            var spawnPos = LevelControl.Instance.GetLinePosition(Random.Range(0, LevelControl.Instance.MaxLines));
            spawnPos.y = LevelControl.Instance.SpawnHeight;
            spawnPos.z = LevelControl.Instance.SpawnLocation;
            var go = Instantiate(mob.Prefab, spawnPos, Quaternion.identity);
            go.SetActive(false);
            var ai = go.AddComponent<EnemyAI>();
            ai.SetupData(mob.Data, mob.Face);
            //Do extra setups on game object
            mob.DoSetup(go, mob.Data);

            go.SetActive(true);
        }

        private void OnDeath(EnemyAI theAi)
        {
            _curDeaths++;
            if (_curDeaths >= _curMaxSpawn)  GameControl.Instance.OnGameWin();
        }

        public void OnDestinationReached(int index)
        {
            if (index == 0) GameControl.Instance.OnGameOver();
        }
    }
}