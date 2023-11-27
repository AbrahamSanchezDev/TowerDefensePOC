using UnityEngine;

namespace WorldsDev
{
    [System.Serializable]
    public class SummonData
    {
        [System.NonSerialized]
        public byte Id;
        //public string Name;
        [Range(1,10)]
        public byte Health = 1;
        [Range(1, 10)]
        public byte Damage = 1;
        [Range(0.1f, 10f)]
        public float Speed;

        [Range(1, 100)]
        public int MinSpawnLevel = 1;
        [Range(1, 100)]
        public int MaxSpawnLevel = 100;

        public bool SpawnAtThisLevel(int level)
        {
            return level >= MinSpawnLevel && level <= MaxSpawnLevel;
        }
    }
}