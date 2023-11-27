using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WorldsDev
{
    public class SummonSlot : MonoBehaviour, ISelectable
    {
        protected void Awake()
        {
            Setup();
        }

        protected void Setup()
        {
        }


        public void OnSelected()
        {
            var pos = transform.position;
            pos.y = LevelControl.Instance.SpawnHeight;
            if (PlayerControl.Instance)
                PlayerControl.Instance.SpawnCurrentAt(pos);
        }
    }
}