using UnityEngine;

namespace WorldsDev
{
    public class AnimationListener : MonoBehaviour, ISpecialSetup
    {
        private EnemyObj _enemyObj;

        public void DoSetup()
        {
            _enemyObj = transform.parent.GetComponent<EnemyObj>();
        }

        public void AlertObservers(string message)
        {
            if (_enemyObj)
                _enemyObj.AlertObservers(message);
        }
    }
}