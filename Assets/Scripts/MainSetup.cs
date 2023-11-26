using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WorldsDev
{
    public class MainSetup : MonoBehaviour
    {
        public float StartGameOn = 2;

        protected void Awake()
        {
            GameControl.Level++;
            Setup();
        }

        protected void Setup()
        {
            GameControl.ChangeGameState(GameState.Idle);

            AddComponent<LevelControl>();

            CreateUi();

            AddComponent<PlayerControl>();
            AddComponent<EnemyControl>();


            Invoke(nameof(StartGame), StartGameOn);
        }

        private void StartGame()
        {
            GameControl.Instance.OnGameStart();
        }

        private T AddComponent<T>() where T : MonoBehaviour
        {
            return gameObject.AddComponent<T>();
        }

        private void CreateUi()
        {
            var prefabs = PrefabsRef.Prefabs;
            var canvas = Instantiate(prefabs.GameCanvasUi);
            canvas.Setup();
        }
    }
}