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

        private AudioClip _onActionSfx;

        private AudioSource _audioSource;

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

        public void SetupAudio(AudioClip sfx)
        {
            _onActionSfx = sfx;
            if (_onActionSfx)
            {
                _audioSource = gameObject.GetComponent<AudioSource>();
                if (!_audioSource)
                    _audioSource = gameObject.AddComponent<AudioSource>();
                _audioSource.clip = _onActionSfx;
                _audioSource.loop = false;
                _audioSource.volume = 0.1f;
            }
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
                    if (_audioSource && _onActionSfx)
                        _audioSource.Play();
                }

                yield return delay;
            }
        }
    }
}