using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace WorldsDev
{
    public class GameStatusUi : MonoBehaviour
    {
        private GameObject _mainUi;
        private TextMeshProUGUI _displayText;

        private string _loseText = $"Game Over \n Resetting LEVEL {GameControl.Level+1}";

        private string _winText = $"YOU WIN! \n MOVE TO LEVEL {GameControl.Level+1}";

        protected void Awake()
        {
            Setup();
        }

        protected void Setup()
        {
            _mainUi = transform.Find("GameStatusUi").gameObject;
            _displayText = _mainUi.transform.Find("MainDisplay").GetComponent<TextMeshProUGUI>();

            var but = _mainUi.transform.Find("OkButton").GetComponent<Button>();
            but.onClick.AddListener(OnClick);
            ShowUi(false);
        }

        protected void OnEnable()
        {
            GameControl.OnGameStarStateEvent.AddListener(OnGameStatusChanged);
        }

        protected void OnDisable()
        {
            GameControl.OnGameStarStateEvent.RemoveListener(OnGameStatusChanged);
        }

        private void OnGameStatusChanged(GameState state)
        {
            switch (state)
            {
                case GameState.Win:
                    OnWin();
                    break;
                case GameState.Lost:
                    OnLose();
                    break;
            }
        }


        private void OnWin()
        {
            SetText(_winText);
            ShowUi();
        }

        private void OnLose()
        {
            SetText(_loseText);
            ShowUi();
        }

        private void ShowUi(bool show = true)
        {
            _mainUi.SetActive(show);
        }

        private void OnClick()
        {
            switch (GameControl.CurGameState)
            {
                case GameState.Win:
                    break;
                case GameState.Lost:
                    GameControl.Level--;
                    break;
            }

            ResetScene();
        }

        private void SetText(string theText)
        {
            _displayText.text = theText;
        }

        private void ResetScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}