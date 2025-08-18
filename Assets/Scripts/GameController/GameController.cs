using Services.ChunkService;
using Services.WindowService;
using UnityEngine;
using Zenject;
using CharacterController = Character.CharacterController;

namespace GameController
{
    public class GameController : MonoBehaviour
    {
        public GameState State { get; private set; } = GameState.Menu;
        
        [SerializeField] private GameObject _playerPrefab;
        [SerializeField] private Vector3 _playerSpawnPoint = Vector3.zero;
        
        [Inject] private DiContainer _container;
        [Inject] private ChunkService _chunkService;
        [Inject] private WindowService _windowService;
        
        private GameObject _playerInstance;
        private CharacterController _controller;
        
        private void Start()
        {
            _chunkService.StopRun();
        }

        public void StartGame()
        {
            if (State == GameState.Playing) return;
            if (_playerInstance) Destroy(_playerInstance);
        
            _playerInstance = _container.InstantiatePrefab(_playerPrefab, _playerSpawnPoint, Quaternion.identity, null);
            _controller = _playerInstance.GetComponent<CharacterController>();
            _controller.OnDead += StopGame;
            
            _chunkService.StartRun();
            Debug.Log("GameStarted");
            State = GameState.Playing;
        }

        private void StopGame()
        {
            if (State != GameState.Playing) return;
        
            _windowService.Close(WindowType.InGameUI);
            _windowService.Create<BaseWindow>(WindowType.RestartWindow);
            _chunkService.StopRun();

            State = GameState.GameOver;
        }

        public void Restart()
        {
            if (_playerInstance)
            {
                _controller.OnDead -= StopGame;
                Destroy(_playerInstance);
                _playerInstance = null;
                _controller = null;
            }
            
            _chunkService.StopRun();
            _chunkService.ResetRun();

            _windowService.Create<BaseWindow>(WindowType.MainMenu);
            State = GameState.Menu;
        }
    }
}