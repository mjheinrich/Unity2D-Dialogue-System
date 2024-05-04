using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SpeakerUI : MonoBehaviour
{
    public Image portrait;
    public TextMeshProUGUI fullName;
    public TextMeshProUGUI dialogue;

    private Character speaker;
    public Character Speaker
    {
        get { return speaker; }
        set
        {
            speaker = value;
            portrait.sprite = speaker.characterPortrait;
            fullName.text = speaker.characterName;
        }
    }

    public string Dialogue
    {
        get { return dialogue.text; }
        set { dialogue.text = value; }
    }

    public Mood Mood
    {
        set
        {
            Sprite sprite;
            if (value == Mood.Angry)
            {
                sprite = speaker.portraitAngry;
            }
            if (value == Mood.Happy)
            {
                sprite = speaker.portraitHappy;
            }
            if (value == Mood.Sad)
            {
                sprite = speaker.portraitSad;
            }
            else
            {
                sprite = speaker.characterPortrait;
            }

            portrait.sprite = sprite;
        }
    }

    public bool HasSpeaker()
    {
        return speaker != null;
    }

    public bool SpeakerIs(Character character)
    {
        return speaker == character;
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
