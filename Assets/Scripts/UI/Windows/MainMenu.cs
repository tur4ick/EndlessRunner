using Services;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class MainMenu : BaseWindow
{
    [SerializeField] private Button _starto;
    
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
        _starto.onClick.AddListener(StartButton);
    }

    private void StartButton()
    {
        _windowService.Close(WindowType.MainMenu);
        _windowService.Create<BaseWindow>(WindowType.InGameUI);
        _gameController.OnStartButton();
    }
}
