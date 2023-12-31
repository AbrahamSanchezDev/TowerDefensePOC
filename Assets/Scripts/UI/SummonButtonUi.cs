﻿using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace WorldsDev
{
    public class SummonButtonUi : MonoBehaviour
    {
        private Image _icon;
        private TextMeshProUGUI _price;
        private GameObject _selectedGo;

        public UnityEvent OnClickedEvent = new UnityEvent();

        private static UnityEvent<GameObject> _onSelected = new UnityEvent<GameObject>();


        protected void Awake()
        {
            Setup();
        }

        public void Setup()
        {
            if (_icon) return;
            _icon = transform.Find("Icon").GetComponent<Image>();
            _price = transform.Find("Price").GetComponent<TextMeshProUGUI>();
            _selectedGo = transform.Find("Selected").gameObject;

            Selected(false);

            var button = gameObject.GetComponent<Button>();
            button.onClick.AddListener(Clicked);
        }

        protected void OnEnable()
        {
            _onSelected.AddListener(OnSelectedObj);
        }

        protected void OnDisable()
        {
            _onSelected.RemoveListener(OnSelectedObj);
        }

        private void OnSelectedObj(GameObject go)
        {
            Selected(go == gameObject);
        }


        public void SetInfo(Sprite icon, string price)
        {
            _icon.sprite = icon;
            _price.text = price;
        }

        public void Selected(bool selected)
        {
            _selectedGo.SetActive(selected);
        }

        private void Clicked()
        {
            OnClickedEvent.Invoke();
            _onSelected.Invoke(gameObject);
        }
    }
}