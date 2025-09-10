using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SkinsConfig", menuName = "Shop/SkinsConfig")]
public class SkinsConfig : ScriptableObject
{
    public SkinDefinition[] skins;
}
