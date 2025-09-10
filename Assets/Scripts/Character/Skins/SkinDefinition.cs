using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Skin", menuName = "Shop/Skin")]
public class SkinDefinition : ScriptableObject
{
    public string id;
    public string displayName;
    public int price;
    public GameObject visualsPrefab;
}
