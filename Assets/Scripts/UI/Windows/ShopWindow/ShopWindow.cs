using System;
using System.Collections;
using System.Collections.Generic;
using Services.CoinService;
using Services.WindowService;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class ShopWindow : BaseWindow
{
        [Header("Config & Prefabs")]
        [SerializeField] private SkinsConfig _skinsConfig;
        [SerializeField] private SkinShopItemButton _itemPrefab;
        [SerializeField] private Transform _listContent;

        [Header("Preview & Controls")]
        [SerializeField] private Button _buyButton;
        [SerializeField] private Button _selectButton;
        [SerializeField] private GameObject _selectedText;
        [SerializeField] private PriceCoinsViewTMP _previewPriceView;

        [Header("Header UI")]
        [SerializeField] private TextMeshProUGUI _coinsText;
        [SerializeField] private Button _closeButton; 

        [Inject] private CoinService _coinService;
        [Inject] private WindowService _windowService;
        [Inject] private ModelPreviewService _previewService; 
        [Inject] private PlayerSkinsData _playerSkins; 

        private readonly List<SkinShopItemButton> _items = new();
        private SkinShopItemButton _currentItem;
        private int _lastCoinCount = -1;

        private void Awake()
        {
            if (_buyButton != null)    _buyButton.onClick.AddListener(OnBuyClicked);
            if (_selectButton != null) _selectButton.onClick.AddListener(OnSelectClicked);
            if (_closeButton != null)  _closeButton.onClick.AddListener(CloseSelf);
        }

        private void OnEnable()
        {
            BuildList();
            RefreshCoinsText(true);

            _coinService.OnCoinsChanged += OnCoinsChanged;
            _playerSkins.OnOwnedChanged += RefreshAllStatuses;
            _playerSkins.OnSelectedChanged += RefreshAllStatuses;

            ShowFirstOrSelected();
        }

        private void OnDisable()
        {
            _coinService.OnCoinsChanged -= OnCoinsChanged;
            _playerSkins.OnOwnedChanged -= RefreshAllStatuses;
            _playerSkins.OnSelectedChanged -= RefreshAllStatuses;

            ClearList();

            if (_previewService != null)
                _previewService.Despawn();
        }

        private void OnDestroy()
        {
            if (_buyButton != null)    _buyButton.onClick.RemoveListener(OnBuyClicked);
            if (_selectButton != null) _selectButton.onClick.RemoveListener(OnSelectClicked);
            if (_closeButton != null)  _closeButton.onClick.RemoveListener(CloseSelf);
        }

        private void CloseSelf()
        {
            _windowService.Close(WindowType.Shop);
        }


        private void OnCoinsChanged()
        {
            RefreshCoinsText();
            if (_currentItem != null)
                RefreshControlsFor(_currentItem.Skin); 
        }

        private void RefreshCoinsText(bool force = false)
        {
            if (_coinsText == null) return;
            int c = _coinService.CoinCount;
            if (force || c != _lastCoinCount)
            {
                _lastCoinCount = c;
                _coinsText.text = c.ToString();
            }
        }
        
        private void BuildList()
        {
            if (_skinsConfig == null || _skinsConfig.skins == null) return;

            foreach (var skin in _skinsConfig.skins)
            {
                var item = Instantiate(_itemPrefab, _listContent);
                item.Initialize(skin, _coinService); 
                item.OnClicked += OnItemClicked;
                _items.Add(item);
            }

            RefreshAllStatuses();
        }

        private void ClearList()
        {
            for (int i = 0; i < _items.Count; i++)
            {
                _items[i].OnClicked -= OnItemClicked;
                if (_items[i] != null && _items[i].gameObject != null)
                    Destroy(_items[i].gameObject);
            }
            _items.Clear();
            _currentItem = null;
        }


        private void ShowFirstOrSelected()
        {
            string selectedId = _playerSkins.GetSelected();
            int foundIndex = -1;

            for (int i = 0; i < _items.Count; i++)
            {
                if (_items[i].Skin.id == selectedId)
                {
                    foundIndex = i;
                    break;
                }
            }

            if (foundIndex < 0 && _items.Count > 0)
                OnItemClicked(_items[0]);
            else if (foundIndex >= 0)
                OnItemClicked(_items[foundIndex]);
        }

        private void OnItemClicked(SkinShopItemButton item)
        {
            _currentItem = item;

            for (int i = 0; i < _items.Count; i++)
                _items[i].SetSelectionFrame(false);
            item.SetSelectionFrame(true);

            if (_previewService != null)
                _previewService.Spawn(item.Skin.visualsPrefab);

            RefreshControlsFor(item.Skin);
        }

        private void RefreshAllStatuses()
        {
            for (int i = 0; i < _items.Count; i++)
            {
                var s = _items[i].Skin;
                if (_playerSkins.IsOwned(s.id))
                {
                    if (_playerSkins.GetSelected() == s.id)
                        _items[i].SetPurchasedSelected();
                    else
                        _items[i].SetPurchasedNotSelected();
                }
                else
                {
                    _items[i].SetNotPurchased();
                }
            }

            if (_currentItem != null)
                RefreshControlsFor(_currentItem.Skin);
        }

        private void RefreshControlsFor(SkinDefinition skin)
        {
            bool owned = _playerSkins.IsOwned(skin.id);
            bool selected = _playerSkins.GetSelected() == skin.id;

            if (owned)
            {
                if (selected)
                {
                    _buyButton.gameObject.SetActive(false);
                    _selectButton.gameObject.SetActive(false);
                    _selectedText.SetActive(true);
                }
                else
                {
                    _buyButton.gameObject.SetActive(false);
                    _selectButton.gameObject.SetActive(true);
                    _selectedText.SetActive(false);
                }
            }
            else
            {
                _buyButton.gameObject.SetActive(true);
                _selectButton.gameObject.SetActive(false);
                _selectedText.SetActive(false);

                if (_previewPriceView != null)
                    _previewPriceView.Initialize(_coinService, skin.price); 
            }
        }


        private void OnBuyClicked()
        {
            if (_currentItem == null) return;

            var s = _currentItem.Skin;
            if (_playerSkins.IsOwned(s.id)) return;

            if (_coinService.TrySpend(s.price))
            {
                _playerSkins.MarkOwned(s.id);
                RefreshAllStatuses();
            }
           
        }

        private void OnSelectClicked()
        {
            if (_currentItem == null) return;

            var s = _currentItem.Skin;
            if (_playerSkins.IsOwned(s.id))
            {
                _playerSkins.Select(s.id);
                RefreshAllStatuses();
            }
        }
}
