
using UnityEngine;

[CreateAssetMenu(fileName = "Character",menuName ="ScriptableObjects/CharacterDataScriptableObject", order = 1)]
public class CharacterData : ScriptableObject
{
    [Header("Movimento")]
    public float jogSpeed;
    public float sprintSpeed;
    public float jumpHeight;
    public float crouchSpeed;
    public float climbSpeed;
}
