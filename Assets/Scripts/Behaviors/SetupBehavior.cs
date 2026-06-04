using UnityEngine;

namespace WorldsDev
{
    public class SetupBehavior : ScriptableObject
    {

        public virtual void SetupGo(GameObject go, SummonData data)
        {

        }

        public virtual void SetupAudio(AudioClip sfx, GameObject go)
        {
        }

    }
}