using System;
using Services;
using UnityEngine;
using Zenject;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject _playerPrefab;
    [SerializeField] private Vector3 _playerSpawnPoint = Vector3.zero;
    [SerializeField] private Animator _animator;

    private GameObject _playerInstance;
    public GameState State { get; private set; } = GameState.Menu;

    [Inject] private DiContainer _container;
    [Inject] private ChunkService _chunkService;
    [Inject] private WindowService _windowService;

    private void Start()
    {
        _chunkService.StopRun();
    }

    public void OnStartButton()
    {
        if (State == GameState.Playing) return;
        if (_playerInstance) Destroy(_playerInstance);
        
        _playerInstance = _container.InstantiatePrefab(_playerPrefab, _playerSpawnPoint, Quaternion.identity, null);
        
        _chunkService.StartRun();
        State = GameState.Playing;
    }

    public void OnPlayerDead()
    {
        if (State != GameState.Playing) return;
        
        _windowService.Close(WindowType.InGameUI);
        _windowService.Create<BaseWindow>(WindowType.RestartWindow);
        _chunkService.StopRun();
    }

    public void OnRestartButton()
    {
        if (_playerInstance) Destroy(_playerInstance);
        _chunkService.StopRun();
        _chunkService.ResetRun();

        State = GameState.Menu;
    }
}