using System;
using TMPro;
using UI.Core;
using UI.TimeCounter;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Menu
{
    public class MainMenuUI : StaticUIElement, IBestTimeRefreshable
    {
        [SerializeField] private Button _startGame;
        [SerializeField] private TextMeshProUGUI _time;

        public override void Init(UIData uiData)
        {
            base.Init(uiData);
            
            
            _startGame.onClick.AddListener(StartGame);
        }

        private void StartGame()
        {
            UIData.GameContext.StartGame();
            Hide();
        }
        
        private void OnDisable()
        {
            _startGame.onClick.RemoveAllListeners();
        }

        public void Refresh()
        {
        }
    }
}