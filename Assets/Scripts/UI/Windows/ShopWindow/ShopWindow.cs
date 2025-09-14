using Services.CoinService;
using Services.WindowService;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

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
    [Inject] private PlayerSkinsData _playerSkins;
    [Inject] private ModelPreviewService _previewService;

    private SkinDefinition[] _skins;
    private int _index = -1;

    private SkinDefinition Current =>
        (_skins != null && _index >= 0 && _index < _skins.Length) ? _skins[_index] : null;

    private void OnEnable()
    {
        _skins = _skinsConfig != null ? _skinsConfig.skins : null;

        if (_prevButton) _prevButton.onClick.AddListener(Prev);
        if (_nextButton) _nextButton.onClick.AddListener(Next);
        if (_buyButton) _buyButton.onClick.AddListener(OnBuyClicked);
        if (_selectButton) _selectButton.onClick.AddListener(OnSelectClicked);
        if (_closeButton) _closeButton.onClick.AddListener(Close);

        if (_skins != null && _skins.Length > 0)
        {
            int start = ChooseStartIndex();
            SetIndex(start);
        }
    }

    private void OnDisable()
    {
        if (_prevButton) _prevButton.onClick.RemoveAllListeners();
        if (_nextButton) _nextButton.onClick.RemoveAllListeners();
        if (_buyButton) _buyButton.onClick.RemoveAllListeners();
        if (_selectButton) _selectButton.onClick.RemoveAllListeners();
        if (_closeButton) _closeButton.onClick.RemoveAllListeners();

        if (_previewService != null) _previewService.Despawn();
    }

    private int ChooseStartIndex()
    {
        int idx = 0;
        string sel = _playerSkins.GetSelected();

        int unowned = -1;
        for (int i = 0; i < _skins.Length; i++)
            if (!_playerSkins.IsOwned(_skins[i].id)) { unowned = i; break; }

        if (unowned >= 0) return unowned;

        int ownedNotSelected = -1;
        for (int i = 0; i < _skins.Length; i++)
            if (_playerSkins.IsOwned(_skins[i].id) && _skins[i].id != sel) { ownedNotSelected = i; break; }

        if (ownedNotSelected >= 0) return ownedNotSelected;

        if (!string.IsNullOrEmpty(sel))
        {
            for (int i = 0; i < _skins.Length; i++)
                if (_skins[i].id == sel) { idx = i; break; }
        }

        return idx;
    }

    private void Prev()
    {
        if (_skins == null || _skins.Length == 0) return;
        int next = _index - 1;
        if (next < 0) next = _skins.Length - 1;
        SetIndex(next);
    }

    private void Next()
    {
        if (_skins == null || _skins.Length == 0) return;
        int next = _index + 1;
        if (next >= _skins.Length) next = 0;
        SetIndex(next);
    }

    private void SetIndex(int newIndex)
    {
        if (_skins == null || _skins.Length == 0) return;
        _index = Mathf.Clamp(newIndex, 0, _skins.Length - 1);
        var skin = Current;
        if (_previewService != null) _previewService.Spawn(skin != null ? skin.visualsPrefab : null);
        RefreshControls();
    }

    private void RefreshControls()
    {
        var skin = Current;
        if (skin == null) return;

        bool owned = _playerSkins.IsOwned(skin.id);
        bool selected = _playerSkins.GetSelected() == skin.id;

        _buyButton.gameObject.SetActive(!owned);
        _selectButton.gameObject.SetActive(owned && !selected);

        if (!owned && _priceViewForBuy != null)
            _priceViewForBuy.Initialize(_coinService, skin.price);
    }

    private void OnBuyClicked()
    {
        var skin = Current;
        if (skin == null) return;
        if (_playerSkins.IsOwned(skin.id)) return;
        if (_coinService.TrySpend(skin.price))
        {
            _playerSkins.MarkOwned(skin.id);
            RefreshControls();
        }
    }

    private void OnSelectClicked()
    {
        var skin = Current;
        if (skin == null) return;
        if (!_playerSkins.IsOwned(skin.id)) return;
        _playerSkins.Select(skin.id);
        RefreshControls();
    }
}
