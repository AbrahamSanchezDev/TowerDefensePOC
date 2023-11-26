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

        public void AddSummon(Sprite Icon, int Price, UnityAction onSelected)
        {
            var but = Instantiate(PrefabsRef.Prefabs.SummonButtonUi);
            but.Setup();

            but.SetInfo(Icon, $"${Price}");
            but.OnClickedEvent.AddListener(onSelected);
        }
    }
}