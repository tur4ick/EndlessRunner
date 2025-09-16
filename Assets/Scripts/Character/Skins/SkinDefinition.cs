using UnityEngine;

namespace Character.Skins
{
    [CreateAssetMenu(fileName = "Skin", menuName = "Shop/Skin")]
    public class SkinDefinition : ScriptableObject
    {
        public SkinName id;
        public string displayName;
        public int price;
        public CharacterVisuals visualsPrefab;
    }
}
