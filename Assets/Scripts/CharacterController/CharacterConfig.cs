using UnityEngine;

[CreateAssetMenu(fileName = "CharacterConfig", menuName = "Configs/CharacterConfig")]
public class CharacterConfig : ScriptableObject
{
    public float JumpForce = 5f;
    public float RollDuration = 1f;
    public float LaneDistance = 2.5f;
    public float GroundOffset = 0.1f;
    public float GroundCheckDistance = 0.1f;
}