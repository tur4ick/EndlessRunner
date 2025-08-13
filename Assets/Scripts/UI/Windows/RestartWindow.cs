using System;
using Services;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class RestartWindow : BaseWindow
{
    [SerializeField] private Button _restart;
    
    private WindowService _windowService;
    private GameController _gameController;
    
    [Inject]
    public void Construct(WindowService windowService, GameController gameController)
    {
        _windowService = windowService;
        _gameController = gameController;
    }

    private void Awake()
    {
        _restart.onClick.AddListener(Restart);
    }

    private void Restart()
    {
        _windowService.Close(WindowType.RestartWindow);
        _windowService.Create<BaseWindow>(WindowType.MainMenu);
        _gameController.OnRestartButton();
    }
}