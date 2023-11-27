using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace WorldsDev
{
    public enum GameState
    {
        Idle,
        StartedPlaying,
        Pause,
        Win,
        Lost
    }

    public class GameControl : MonoBehaviour
    {
        public static int Level;

        public static GameControl Instance;

        public static GameState CurGameState;

        public static UnityEvent<GameState> OnGameStarStateEvent = new UnityEvent<GameState>();


        public static LayerMask ClickMask;


        protected void Awake()
        {
            if (Instance)
            {
                DestroyImmediate(gameObject);
                return;
            }

            Instance = this;
            Setup();
        }

        protected void Setup()
        {
            ClickMask = PrefabsRef.Prefabs.ClickMask;
        }

        public void OnGameStart()
        {
            ChangeGameState(GameState.StartedPlaying);
        }

        public void OnGameWin()
        {
            if (CurGameState == GameState.StartedPlaying) ChangeGameState(GameState.Win);
        }

        public void OnGameOver()
        {
            if (CurGameState == GameState.StartedPlaying) ChangeGameState(GameState.Lost);
        }

        public static void ChangeGameState(GameState state)
        {
            CurGameState = state;
            //Debug.Log("GAME " + state);
            OnGameStarStateEvent.Invoke(state);
        }
    }
}