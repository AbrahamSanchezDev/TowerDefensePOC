using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WorldsDev
{
    [CreateAssetMenu(fileName = "PrefabsRef", menuName = "Tools/PrefabsRef")]
    public class PrefabsRef : ScriptableObject
    {
        private static PrefabsRef _prefabs;

        public static PrefabsRef Prefabs
        {
            get
            {
                if (_prefabs != null)
                {
                    return _prefabs;
                }

                _prefabs = Resources.Load<PrefabsRef>("Data/PrefabsRef");
                return _prefabs;
            }
        }
        [Header("Summons DBs")]
        public SummonDb SummonDB;
        public SummonDb EnemyDB;
        [Header("User UI")]
        public SummonButtonUi SummonButtonUi;
        public GameObject GameCanvasGo;
    }
}