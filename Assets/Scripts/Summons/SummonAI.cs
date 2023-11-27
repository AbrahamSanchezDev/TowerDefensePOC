using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WorldsDev
{
    public class SummonAI : MonoBehaviour, IDamageable
    {
        public SummonData Data;

        private int _curHealth;

        protected void Awake()
        {
            Setup();
        }

        protected void Setup()
        {
            gameObject.layer = PrefabsRef.SummonLayer;
            var col = gameObject.AddComponent<BoxCollider>();
            col.center = Vector3.up / 2;
        }

        public void SecondSetup(SummonData data)
        {
            Data = data;
            _curHealth = data.Health;
        }

        public bool Alive()
        {
            return _curHealth > 0;
        }

        public void OnHit(int dmg)
        {
            //Debug.Log("HIT ON " + gameObject.name + " " + dmg);
            var visuals = PrefabsRef.Prefabs.GameEffects;
            Instantiate(visuals.OnDamageRecivedSummon, transform.position, Quaternion.identity);
            _curHealth -= dmg;

            if (!Alive()) OnDeath();
        }

        public void OnDeath()
        {
            var visuals = PrefabsRef.Prefabs.GameEffects;
            Instantiate(visuals.OnDeathSummon, transform.position, Quaternion.identity);
        }
    }
}