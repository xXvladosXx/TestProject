﻿using System;
using Audio;
using Entities;
using Level;
using Saving;
using UI.Core;
using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private UIController _uiController;
    [SerializeField] private LevelGenerator _levelGenerator;
    [SerializeField] private AudioClip _music;

    private SaveContext _saveContext;
    private GameContext _gameContext;
        
    private float _timeSinceGameStarted;
    private bool _gameStarted;
        
    private void Awake()
    {
        _saveContext = new SaveContext();
        _gameContext = new GameContext();
                
        var uiData = new UIData
        {
            Player = _player,
            SaveContext = _saveContext,
            GameContext = _gameContext
        };
            
        _uiController.Init(uiData);
    }

    private void OnEnable()
    {
        _gameContext.OnGameStarted += OnGameStarted;
        _uiController.OnPopupUIOpened += DeactivatePlayerController;
        _uiController.OnPopupUIHide += ActivatePlayerController;
    }

    private void ActivatePlayerController()
    {
        _player.ActivateInputs();
    }

    private void DeactivatePlayerController()
    {
        _player.DeactivateInputs();
    }

    private void OnGameEnded()
    {
        _gameContext.OnGameEnded -= OnGameEnded;
            
        _player.ItemPicker.ResetInventory();
        _gameStarted = false;
        
        AudioManager.Instance.StopMusicSound();
            
        DeactivatePlayerController();
        _levelGenerator.DestroyObjects();
        
        _saveContext.TryToRegisterBestResult(new SaveData
        {
            Time = _timeSinceGameStarted
        });
            
        _gameContext.OnGameStarted += OnGameStarted;
    }

    private void OnGameStarted()
    {
        _gameContext.OnGameStarted -= OnGameStarted;
        _gameContext.OnGameEnded += OnGameEnded;
        
        AudioManager.Instance.PlayMusicSound(_music);
        
        _levelGenerator.InstantiateObjects();

        _gameStarted = true;
        _timeSinceGameStarted = 0;

        _player.Init();
    }

    private void Update()
    {
        if(_gameStarted)
            _timeSinceGameStarted += Time.deltaTime;
            
        _gameContext.Time = _timeSinceGameStarted;
    }

    private void OnDisable()
    {
        _uiController.OnPopupUIOpened -= DeactivatePlayerController;
        _uiController.OnPopupUIHide -= ActivatePlayerController;
    }
}