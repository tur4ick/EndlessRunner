using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerSkinsData : MonoBehaviour
{
    [SerializeField] private string selectedSkinId;
    [SerializeField] private List<string> owned = new List<string>();

    public event Action OnOwnedChanged;
    public event Action OnSelectedChanged;

    public bool IsOwned(string skinId)
    {
        int i = 0;
        while (i < owned.Count)
        {
            if (owned[i] == skinId) return true;
            i = i + 1;
        }
        return false;
    }

    public void MarkOwned(string skinId)
    {
        if (IsOwned(skinId) == false)
        {
            owned.Add(skinId);
            if (OnOwnedChanged != null) OnOwnedChanged.Invoke();
        }
    }

    public string GetSelected() { return selectedSkinId; }

    public void Select(string skinId)
    {
        selectedSkinId = skinId;
        if (OnSelectedChanged != null) OnSelectedChanged.Invoke();
    }
}
