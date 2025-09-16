using System;
using Character.Skins;
using Services.CoinService;
using Services.GameDataService;
using Services.ModelPreviewService;
using Services.WindowService;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI.Windows.ShopWindow
{
    public class ShopWindow : BaseWindow
    {
        [SerializeField] private SkinsConfig _skinsConfig;
        [SerializeField] private Button _prevButton;
        [SerializeField] private Button _nextButton;
        [SerializeField] private Button _buyButton;
        [SerializeField] private Button _selectButton;
        [SerializeField] private Button _closeButton;
        [SerializeField] private PriceCoinsViewTMP _priceViewForBuy;

        [Inject] private CoinService _coinService;
        [Inject] private ModelPreviewService _previewService;
        [Inject] private GameDataService _gameDataService;

        private int _index = -1;

        private SkinDefinition Current => _skinsConfig.skins[_index];

        private void Start()
        {
            _prevButton.onClick.AddListener(Prev);
            _nextButton.onClick.AddListener(Next);
            _buyButton.onClick.AddListener(OnBuyClicked);
            _selectButton.onClick.AddListener(OnSelectClicked);
            _closeButton.onClick.AddListener(Close);
            
            int start = ChooseStartIndex();
            SetIndex(start);
        }

        private void OnDestroy()
        {
            _previewService.Despawn();
        }

        private int ChooseStartIndex()
        {
            for (int i = 0; i < _skinsConfig.skins.Length; i++)
            {
                if (!_gameDataService.Data.IsOwned(_skinsConfig.skins[i].id))
                {
                    return i;
                }
            }

            SkinName selected = _gameDataService.Data.SelectedSkinId;
            
            for (int i = 0; i < _skinsConfig.skins.Length; i++)
            {
                if (_gameDataService.Data.IsOwned(_skinsConfig.skins[i].id) && _skinsConfig.skins[i].id != selected)
                {
                    return i;
                }
            }

            for (int i = 0; i < _skinsConfig.skins.Length; i++)
            {
                if (_skinsConfig.skins[i].id == selected)
                {
                    return i;
                }
            }

            return 0;
        }

        private void Prev()
        {
            int next = _index - 1;
            if (next < 0) next = _skinsConfig.skins.Length - 1;
            SetIndex(next);
        }

        private void Next()
        {
            int next = _index + 1;
            if (next >= _skinsConfig.skins.Length) next = 0;
            SetIndex(next);
        }

        private void SetIndex(int newIndex)
        {
            _index = Mathf.Clamp(newIndex, 0, _skinsConfig.skins.Length - 1);
            var skin = Current;
            _previewService.Spawn(skin.visualsPrefab);
            RefreshControls();
        }

        private void RefreshControls()
        {
            var skin = Current;
            bool owned = _gameDataService.Data.IsOwned(skin.id);
            bool selected = _gameDataService.Data.SelectedSkinId == skin.id;

            _buyButton.gameObject.SetActive(!owned);
            _selectButton.gameObject.SetActive(owned && !selected);

            if (!owned)
                _priceViewForBuy.Initialize(skin.price);
        }

        private void OnBuyClicked()
        {
            var skin = Current;
            if (_gameDataService.Data.IsOwned(skin.id)) return;
            if (_coinService.TrySpend(skin.price))
            {
                _gameDataService.Data.MarkOwned(skin.id);
                RefreshControls();
            }
        }

        private void OnSelectClicked()
        {
            var skin = Current;
            if (!_gameDataService.Data.IsOwned(skin.id)) return;
            _gameDataService.Data.SelectedSkinId = skin.id;
            RefreshControls();
        }
    }
}
