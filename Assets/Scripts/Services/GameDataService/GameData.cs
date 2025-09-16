using System.Collections.Generic;
using Character.Skins;
using UnityEngine;

namespace Services.GameDataService
{
    public class GameData
    {
        public SkinName SelectedSkinId;
        public List<SkinName> Owned = new List<SkinName>();
        
        public int Coins;
        public float BestDistance;
        
        public bool IsOwned(SkinName skinId)
        {
            int i = 0;
            while (i < Owned.Count)
            {
                if (Owned[i] == skinId) return true;
                i = i + 1;
            }
            return false;
        }

        public void MarkOwned(SkinName skinId)
        {
            if (IsOwned(skinId) == false)
            {
                Owned.Add(skinId);
            }
        }
    }
}