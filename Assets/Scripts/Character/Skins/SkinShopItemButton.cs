using System;
using Services.CoinService;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkinShopItemButton : MonoBehaviour
{
    public event Action<SkinShopItemButton> OnClicked;
    
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private GameObject selectedMark;
    [SerializeField] private GameObject priceGroup;
    [SerializeField] private PriceCoinsViewTMP priceView;
    [SerializeField] private RawImage selectionFrame;
    [SerializeField] private Button button;

    [SerializeField] private SkinDefinition skin;

    public SkinDefinition Skin { get { return skin; } }

    public void Initialize(SkinDefinition data, CoinService service)
    {
        skin = data;
        nameText.text = data.displayName;
        priceView.Initialize(service, data.price);
        selectionFrame.enabled = false;
        selectedMark.SetActive(false);
    }

    void Start()
    {
        button.onClick.AddListener(HandleClick);
    }

    void OnDestroy()
    {
        button.onClick.RemoveListener(HandleClick);
    }

    private void HandleClick()
    {
        if (OnClicked != null) OnClicked.Invoke(this);
    }

    public void SetPurchasedNotSelected()
    {
        selectedMark.SetActive(false);
        priceGroup.SetActive(false);
    }

    public void SetPurchasedSelected()
    {
        selectedMark.SetActive(true);
        priceGroup.SetActive(false);
    }

    public void SetNotPurchased()
    {
        selectedMark.SetActive(false);
        priceGroup.SetActive(true);
    }

    public void SetSelectionFrame(bool value)
    {
        selectionFrame.enabled = value;
    }

}