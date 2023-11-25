using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WorldsDev
{
    public class MainSetup : MonoBehaviour
    {
        protected void Awake()
        {
            Setup();
        }

        protected void Setup()
        {
            AddComponent<LevelControl>();
        }

        private T AddComponent<T>() where T : MonoBehaviour
        {
            return gameObject.AddComponent<T>();
        }
    }
}