using Character;
using Character.Skins;
using Services.ChunkService;
using Services.WindowService;
using UnityEngine;
using Zenject;
using CharacterController = Character.CharacterController;
using Services.AudioService;
using Services.GameDataService;

namespace GameController
{
    public class GameController : MonoBehaviour
    {
        public GameState State { get; private set; } = GameState.Menu;
        
        [SerializeField] private Vector3 _playerSpawnPoint = Vector3.zero;
        [SerializeField] private CharacterMovement _characterMovement;
        
        [Inject] private DiContainer _container;
        [Inject] private ChunkService _chunkService;
        [Inject] private WindowService _windowService;
        [Inject] private AudioService _audio;
        [Inject] private SkinsConfig _skinsConfig;
        [Inject] private GameDataService _gameDataService;

        
        private GameObject _movementInstance;
        private GameObject _visualsInstance;

        private CharacterController _controller;
        
        private void Start()
        {
            _chunkService.StopRun();
            _audio.PlayMenuMusic(); 
        }

        public void StartGame()
        {
            if (State == GameState.Playing) return;

            var visualsPrefab = _skinsConfig.GetSkinById(_gameDataService.Data.SelectedSkinId).visualsPrefab;
            
            
            CleanupPlayer();

            _movementInstance = _container.InstantiatePrefab(_characterMovement, _playerSpawnPoint,
                Quaternion.identity, null);
            
            _controller = _movementInstance.GetComponent<CharacterController>();
            _controller.OnDead += StopGame;
            
            if (visualsPrefab != null)
            {
                _visualsInstance = _container.InstantiatePrefab(visualsPrefab, _playerSpawnPoint, Quaternion.identity, _movementInstance.transform);
                
                CharacterVisuals visuals = _visualsInstance.GetComponent<CharacterVisuals>();
                if (visuals != null) visuals.Initialize(_controller);
            }
            _audio.PlayGameMusic();
            _chunkService.StartRun();
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
            CleanupPlayer();
            
            _chunkService.StopRun();
            _chunkService.ResetRun();
            _audio.PlayMenuMusic();

            _windowService.Create<BaseWindow>(WindowType.MainMenu);
            State = GameState.Menu;
        }
        
        private void CleanupPlayer()
        {
            if (_controller != null)
            {
                _controller.OnDead -= StopGame;
                _controller = null;
            }

            if (_movementInstance != null)
            {
                Destroy(_movementInstance);
                _movementInstance = null;
            }

            if (_visualsInstance != null)
            {
                CharacterVisuals visuals = _visualsInstance.GetComponent<CharacterVisuals>();
                if (visuals != null) visuals.Dispose();

                Destroy(_visualsInstance);
                _visualsInstance = null;
            }
        }
    }
}