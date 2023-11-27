using System.Collections.Generic;
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

        public List<SetupBehavior> SetupGo = new List<SetupBehavior>();

        public void DoSetup(GameObject go, SummonData data)
        {
            for (int i = 0; i < SetupGo.Count; i++)
            {
                SetupGo[i].SetupGo(go,data);
            }
        }
    }
}