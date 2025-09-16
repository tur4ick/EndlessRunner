using UnityEngine;

namespace Character.Skins
{
    [CreateAssetMenu(fileName = "SkinsConfig", menuName = "Shop/SkinsConfig")]
    public class SkinsConfig : ScriptableObject
    {
        public SkinDefinition[] skins;

        public SkinDefinition GetSkinById(SkinName id)
        {
            foreach (var skinDefinition in skins)
            {
                if (skinDefinition.id == id)
                {
                    return skinDefinition;
                }
            }
            return null;
        }
    }
}
