using System.Collections;
using UnityEngine;

namespace WorldsDev
{
    public class AddMoneyObj : MonoBehaviour
    {
        private int Amount = 1;
        private float Speed = 0;

        private IDamageable _self;

        private GameObject _onAddMoneyEffect;

        protected void Awake()
        {
            _self = gameObject.GetComponent<IDamageable>();
        }

        public void Setup(int amount, float speed, GameObject effectGo)
        {
            Amount = amount;
            Speed = speed;
            _onAddMoneyEffect = effectGo;
        }

        protected void Start()
        {
            StartCoroutine(nameof(AddMoney));
        }


        protected IEnumerator AddMoney()
        {
            var delay = new WaitForSeconds(Speed);
            while (_self.Alive())
            {
                if (PlayerControl.Instance && GameControl.CurGameState == GameState.StartedPlaying)
                {
                    PlayerControl.Instance.AddMoney(Amount);
                    var pos = transform.position;
                    pos.y += 1.5f;
                    if (_onAddMoneyEffect)
                        Instantiate(_onAddMoneyEffect, pos, Quaternion.identity);
                }

                yield return delay;
            }
        }
    }
}