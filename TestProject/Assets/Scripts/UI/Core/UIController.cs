using System;
using System.Collections.Generic;
using System.Linq;
using Entities;
using Interaction.Core;
using UI.EndGame;
using UI.TimeCounter;
using UI.Warning;
using UnityEngine;

namespace UI.Core
{
    public class UIController : MonoBehaviour
    {
        private UIData _uiData;
        private List<StaticUIElement> _staticUIElements = new List<StaticUIElement>();
        private List<PopupUIElement> _popupUIElements  = new List<PopupUIElement>();
        
        private List<IBestTimeRefreshable> _timeRefreshables = new List<IBestTimeRefreshable>();

        private UIElement _currentPopupUIElement;

        public event Action OnPopupUIOpened;
        public event Action OnPopupUIHide;
        public void Init(UIData uiData)
        {
            _staticUIElements = GetComponentsInChildren<StaticUIElement>().ToList();
            _popupUIElements = GetComponentsInChildren<PopupUIElement>().ToList();
            _timeRefreshables = GetComponentsInChildren<IBestTimeRefreshable>().ToList();
            
            _uiData = uiData;
            
            foreach (var popupUIElement in _popupUIElements)
            {
                popupUIElement.Init(_uiData);
                popupUIElement.Hide();
                popupUIElement.OnElementShow += OnPopupShow;
                popupUIElement.OnElementHide += OnPopupHide;
            }

            foreach (var staticUIElement in _staticUIElements)
            {
                staticUIElement.Init(_uiData);
            }

            _uiData.GameContext.OnGameStarted += ActivateTimeCounter;
            _uiData.GameContext.OnGameEnded += ShowEndGameScreen;

            _uiData.Player.OnInteracted += TryToInteract;

            _uiData.SaveContext.OnBestTimeChanged += ChangeBestTime;
        }

        private void OnPopupHide()
        {
            OnPopupUIHide?.Invoke();
        }

        private void OnPopupShow(IUIElement obj)
        {
            OnPopupUIOpened?.Invoke();
        }

        private void ChangeBestTime()
        {
            foreach (var bestTimeRefreshable in _timeRefreshables)
            {
                bestTimeRefreshable.Refresh();
            }
        }

        private void ShowEndGameScreen()
        {
            foreach (var popupUIElement in _popupUIElements)
            {
                if (popupUIElement.GetType() == typeof(EndGameUI))
                {
                    popupUIElement.Show();
                }
            }
        }

        private void TryToInteract(IInteractable interactable, IInteractor interactor)
        {
            foreach (var popupUIElement in _popupUIElements)
            {
                if (popupUIElement is WarningUI warningUI)
                {
                    warningUI.TryToInteract(interactable, interactor);
                    warningUI.Show();
                }
            }
        }


        private void ActivateTimeCounter()
        {
            _uiData.GameContext.OnGameStarted -= ActivateTimeCounter;

            foreach (var staticUIElement in _staticUIElements)
            {
                if (staticUIElement.GetType() == typeof(TimeCounterUI))
                {
                    staticUIElement.Show();
                }
            }
        }
    }
}