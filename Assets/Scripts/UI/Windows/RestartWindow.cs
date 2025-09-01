using Services.WindowService;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI.Windows
{
    public class RestartWindow : BaseWindow
    {
        [SerializeField] private Button _restartButton;
    
        [Inject] private WindowService _windowService;
        [Inject] private GameController.GameController _gameController;

        private void Awake()
        {
            _restartButton.onClick.AddListener(OnRestart);
        }

        private void OnRestart()
        {
            _windowService.Close(WindowType.RestartWindow);
            _gameController.Restart();
        }
    }
}