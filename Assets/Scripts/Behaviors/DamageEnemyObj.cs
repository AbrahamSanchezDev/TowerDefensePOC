using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WorldsDev
{
    public class DamageEnemyObj : MonoBehaviour
    {
        private int _damageAmount;

        private float _speed;

        private LayerMask _hitMask;
        private float _hitDistance;

        private IDamageable _damageable;


        protected void Start()
        {
            StartCoroutine(nameof(DoDamage));
        }

        public void Setup(int dmg, float speed, LayerMask mask, float distance)
        {
            _damageAmount = dmg;
            _speed = speed;
            _hitMask = mask;
            _hitDistance = distance;
            _damageable = gameObject.GetComponent<IDamageable>();
        }

        private IEnumerator DoDamage()
        {
            var delay = new WaitForSeconds(_speed);

            var shootFrom = transform.position;
            shootFrom.y += 0.5f;
            var shootDirection = transform.forward;
            var ray = new Ray(shootFrom, shootDirection);
            var visuals = PrefabsRef.Prefabs.GameEffects;
            while (_damageable.Alive())
            {
                if (GameControl.CurGameState == GameState.StartedPlaying)
                {
                    Instantiate(visuals.OnDoDamageSummon, shootFrom, Quaternion.identity);
                    if (Physics.Raycast(ray, out var hit, _hitDistance, _hitMask))
                    {
                        //Debug.Log("Damaged " + hit.transform.name);
                        var selectable = hit.transform.gameObject.GetComponent<IDamageable>();
                        selectable?.OnHit(_damageAmount);
                    }
                }
                yield return delay;
            }
        }
    }
}