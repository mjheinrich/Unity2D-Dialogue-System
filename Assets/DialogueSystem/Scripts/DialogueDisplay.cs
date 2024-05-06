using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueDisplay : MonoBehaviour
{
    [SerializeField] private float typingSpeed;
    [SerializeField] private GameObject speakerLeft;
    [SerializeField] private GameObject speakerRight;

    public static Conversations conversations;
    private SpeakerUI speakerUILeft;
    private SpeakerUI speakerUIRight;

    private int activeLineIndex;

    public static bool conversationStarted = false;
    public static bool isActive = false;
    private bool isAddingRichTextTag = false;

    private void Start()
    {
        isActive = false;
        speakerUILeft = speakerLeft.GetComponent<SpeakerUI>();
        speakerUIRight = speakerRight.GetComponent<SpeakerUI>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
            AdvanceLine();
    }

    private void EndConversation()
    {
        conversations = null;
        conversationStarted = false;
        speakerUILeft.Hide();
        speakerUIRight.Hide();
        isActive = false;
    }

    public void Initialize()
    {
        isActive = true;
        conversationStarted = true;
        activeLineIndex = 0;
        speakerUILeft.Speaker = conversations.speakerLeft;
        speakerUIRight.Speaker = conversations.speakerRight;
    }

    public void AdvanceLine()
    {
        if (conversations == null) return;
        if (!conversationStarted) Initialize();

        if (activeLineIndex < conversations.lines.Length)
            DisplayLine();
        else
            EndConversation();
    }

    public void DisplayLine()
    {
        Line line = conversations.lines[activeLineIndex];
        Character character = line.character;

        if (speakerUILeft.SpeakerIs(character))
        {
            SetDialog(speakerUILeft, speakerUIRight, line);
        }
        else
        {
            SetDialog(speakerUIRight, speakerUILeft, line);
        }

        activeLineIndex += 1;
    }

    private void SetDialog(
        SpeakerUI activeSpeakerUI,
        SpeakerUI inactiveSpeakerUI,
        Line line
    )
    {
        activeSpeakerUI.Show();
        inactiveSpeakerUI.Hide();

        activeSpeakerUI.Dialogue = "";
        activeSpeakerUI.Mood = line.mood;

        StopAllCoroutines();
        StartCoroutine(EffectTypewriter(line.text, activeSpeakerUI));
    }

    private IEnumerator EffectTypewriter(string text, SpeakerUI controller)
    {
        foreach (char character in text.ToCharArray())
        {

            // check for rich text tag and, if found, add it without waiting
            if (character == '<' || isAddingRichTextTag)
            {
                isAddingRichTextTag = true;
                controller.Dialogue += character;

                if (character == '>')
                {
                    isAddingRichTextTag = false;
                }
            }
            else
            {
                controller.Dialogue += character;
                yield return new WaitForSeconds(typingSpeed);
            }
        }
    }
}
