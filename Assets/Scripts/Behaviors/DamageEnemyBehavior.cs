using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WorldsDev
{
    [CreateAssetMenu(menuName = "Behaviors/DamageEnemyBehavior")]
    public class DamageEnemyBehavior : SetupBehavior
    {
        public LayerMask HitMask;
        [Range(1,100)]
        public float Distance = 100;
        public override void SetupGo(GameObject go, SummonData data)
        {
            base.SetupGo(go, data);

            var dmg = go.AddComponent<DamageEnemyObj>();

            dmg.Setup(data.Damage, data.Speed, HitMask, Distance);
        }
    }
}