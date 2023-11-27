using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace WorldsDev
{
    public class GameCanvasUi : MonoBehaviour
    {
        private TextMeshProUGUI _moneyText;

        private Transform _summonsTransform;

        public static GameCanvasUi Instance;

        protected void Awake()
        {
            Instance = this;
            Setup();
        }

        public void Setup()
        {
            if (_moneyText) return;
            _moneyText = transform.Find("Money").GetComponentInChildren<TextMeshProUGUI>();
            _summonsTransform = transform.Find("Summons");
        }

        public void UpdateMoney(int amount)
        {
            _moneyText.text = $"${amount}";
        }

        public void AddSummon(Sprite Icon, int Price, UnityAction onSelected, int index)
        {
            var but = Instantiate(PrefabsRef.Prefabs.SummonButtonUi, _summonsTransform);
            but.Setup();

            but.SetInfo(Icon, $"${Price}");
            but.OnClickedEvent.AddListener(onSelected);
            if (index == 0)
            {
                but.Selected(true);
            }
        }
    }
}