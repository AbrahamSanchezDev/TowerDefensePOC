using UnityEngine;

namespace WorldsDev
{
    public class SummonSlot : MonoBehaviour, ISelectable
    {
        public void OnSelected()
        {
            var pos = transform.position;
            pos.y = LevelControl.Instance.SpawnHeight;
            if (PlayerControl.Instance)
                PlayerControl.Instance.SpawnCurrentAt(pos);
        }
    }
}