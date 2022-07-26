using System;
using Entities;
using Saving;
using UI.Core;
using UnityEngine;

namespace DefaultNamespace
{
    public class Game : MonoBehaviour
    {
        [SerializeField] private Player _player;
        [SerializeField] private UIController _uiController;

        private SaveContext _saveContext;
        private float _timeSinceGameStarted;
        
        private void Awake()
        {
            _saveContext = new SaveContext();

            var uiData = new UIData
            {
                Player = _player,
                SaveContext = _saveContext
            };
            
            _uiController.Init(uiData);
        }

        private void Update()
        {
            _timeSinceGameStarted += Time.deltaTime;
        }
    }
}