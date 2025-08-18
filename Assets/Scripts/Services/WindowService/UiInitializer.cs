using UnityEngine;
using Zenject;

namespace Services.WindowService
{
    public class UiInitializer : MonoBehaviour
    {
        [Inject] private WindowService _windowService;
        
        private void Start()
        {
            _windowService.Create<BaseWindow>(WindowType.MainMenu);
        }
    }
}