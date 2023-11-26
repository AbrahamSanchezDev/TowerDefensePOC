using UnityEngine;

namespace WorldsDev
{
    [System.Serializable]
    public class SummonObj
    {
        public GameObject Prefab;
        public Sprite Icon;
        public int Price;
        public SummonData Data;
        public Face Face;
    }
}