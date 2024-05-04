using UnityEngine;

[CreateAssetMenu(fileName = "New Character", menuName = "Character")]
public class Character : ScriptableObject
{
    public string characterName;
    public Sprite characterPortrait;
    public Sprite portraitAngry;
    public Sprite portraitHappy;
    public Sprite portraitSad;
}
