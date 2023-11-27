using UnityEngine;

namespace WorldsDev
{
    [CreateAssetMenu]
    public class GameEffects : ScriptableObject
    {
        public GameObject OnDoDamageEnemy;
        public GameObject OnDoDamageSummon;


        public GameObject OnDeathEnemy;
        public GameObject OnDeathSummon;

        public GameObject OnDamagedRecivedEnemy;
        public GameObject OnDamageRecivedSummon;
    }
}