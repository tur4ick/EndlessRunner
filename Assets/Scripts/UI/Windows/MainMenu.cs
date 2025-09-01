using Services.CoinService;
using Services.WindowService;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI.Windows
{
    public class MainMenu : BaseWindow
    {
        [SerializeField] private Button _startButton;
        [SerializeField] private Button _settingsButton;
    
        [Inject] private WindowService _windowService;
        [Inject] private GameController.GameController _gameController;
        [Inject] private DistanceService _distanceService;
    
        private void Awake()
        {
            _startButton.onClick.AddListener(OnStart);
            _settingsButton.onClick.AddListener(OnSettingsButton);
        }

        private void OnStart()
        {
            _windowService.Close(WindowType.MainMenu);
            _windowService.Create<BaseWindow>(WindowType.InGameUI);
            _gameController.StartGame();
        }

        private void OnSettingsButton()
        {
            _windowService.Create<BaseWindow>(WindowType.Settings);
        }
    }
}
