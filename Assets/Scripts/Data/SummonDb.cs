using System.Collections.Generic;
using UnityEngine;

namespace WorldsDev
{
    [CreateAssetMenu(fileName = "SummonDb", menuName = "Data/SummonDb")]
    public class SummonDb : ScriptableObject
    {
        public List<SummonObj> SummonObjs = new List<SummonObj>();
    }
}