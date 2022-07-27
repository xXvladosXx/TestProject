using TMPro;
using UI.Core;
using UI.TimeCounter;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Utilities;

namespace UI.EndGame
{
    public class EndGameUI : PopupUIElement, IBestTimeRefreshable
    {
        [SerializeField] private TextMeshProUGUI _currentTime;
        [SerializeField] private TextMeshProUGUI _bestTime;
        [SerializeField] private Button _button;

        public override void Show()
        {
            base.Show();
            EndGame();
        }

        private void EndGame()
        {
            _currentTime.text = TextFormatter.FormatToTwoDecimalAfterPoint(UIData.GameContext.Time);
            _bestTime.text = UIData.SaveContext.GetBestTime();
            
            _button.onClick.AddListener(RestartGame);
        }

        private void RestartGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            Hide();
        }

        private void OnDisable()
        {
            _button.onClick.RemoveAllListeners();
        }

        public void Refresh()
        {
            _bestTime.text = UIData.SaveContext.GetBestTime();
        }
    }
}