using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace WorldsDev
{
    public class EnemyAI : MonoBehaviour, IDamageable
    {
        private int _curHealth;

        private MovementController _moveControl;


        private Transform _myTransform;

        protected void Awake()
        {
            Setup();
        }

        protected void Setup()
        {
            if (_moveControl) return;
            _myTransform = transform;
            _moveControl = gameObject.GetComponent<MovementController>();
            if (_moveControl == null) _moveControl = gameObject.AddComponent<MovementController>();

            _moveControl.OnDestinationReached.AddListener(OnDestinationReached);
        }

        protected void OnEnable()
        {
            GameControl.OnGameStarStateEvent.AddListener(OnGameOver);
        }

        protected void OnDisable()
        {
            GameControl.OnGameStarStateEvent.RemoveListener(OnGameOver);
        }

        public void SetupData(SummonData data, Face faces)
        {
            Setup();
            _curHealth = data.Health;
            _moveControl.faces = faces;
            _moveControl.SecondSetup();
            _moveControl.ChangeSpeed(data.Speed);

            _moveControl.waypoints = new Vector3[]
            {
                new Vector3(transform.position.x, transform.position.y, 2)
            };

            _moveControl.currentState = SlimeAnimationState.Walk;
        }


        public bool Alive()
        {
            return _curHealth > 0;
        }

        public void OnHit(int dmg)
        {
            _curHealth -= dmg;
            if (!Alive()) OnDeath();
        }

        public void OnDeath()
        {
            EnemyControl.OnDeathEvent.Invoke(this);
        }

        private void OnDestinationReached(int index)
        {
            if (index == 0)
            {
                if (_myTransform.position.z > 0)
                    EnemyControl.Instance.OnDestinationReached(index);
            }
        }

        private void OnGameOver(GameState state)
        {
            switch (state)
            {
                case GameState.Idle:
                    break;
                case GameState.StartedPlaying:
                    break;
                case GameState.Pause:
                    break;
                case GameState.Win:
                    break;
                case GameState.Lost:
                    _moveControl.CancelGoNextDestination();
                    _moveControl.currentState = SlimeAnimationState.Idle;
                    break;
            }
       
        }
    }
}