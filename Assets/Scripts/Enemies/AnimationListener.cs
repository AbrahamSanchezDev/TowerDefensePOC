using UnityEngine;

namespace WorldsDev
{
    public class AnimationListener : MonoBehaviour, ISpecialSetup
    {
        private MovementController _movementController;

        public void DoSetup()
        {
            _movementController = transform.parent.GetComponent<MovementController>();
        }

        public void AlertObservers(string message)
        {
            if (_movementController)
                _movementController.AlertObservers(message);
        }
    }
}