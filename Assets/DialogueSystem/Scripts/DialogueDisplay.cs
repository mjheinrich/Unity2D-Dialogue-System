using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueDisplay : MonoBehaviour
{
    public Conversations conversations;
    public GameObject speakerLeft;
    public GameObject speakerRight;

    private SpeakerUI speakerUILeft;
    private SpeakerUI speakerUIRight;

    public float typingSpeed;

    private int activeLineIndex;
    private bool conversationStarted = false;
    //private bool canContinueToNextLine = false;
    private bool isAddingRichTextTag = false;

    public void ChangeConversation(Conversations nextConversation)
    {
        conversationStarted = false;
        conversations = nextConversation;
        AdvanceLine();
    }

    private void Start()
    {
        speakerUILeft = speakerLeft.GetComponent<SpeakerUI>();
        speakerUIRight = speakerRight.GetComponent<SpeakerUI>();
    }

    private void Update()
    {
        if (Input.GetKeyDown("space"))
            AdvanceLine();
        else if (Input.GetKeyDown("x"))
            EndConversation();
    }

    private void EndConversation()
    {
        //conversations = defaultConversation;
        conversationStarted = false;
        speakerUILeft.Hide();
        speakerUIRight.Hide();
    }

    public void Initialize()
    {
        conversationStarted = true;
        activeLineIndex = 0;
        speakerUILeft.Speaker = conversations.speakerLeft;
        speakerUIRight.Speaker = conversations.speakerRight;
    }

    public void AdvanceLine()
    {
        //if (conversations == null) return;
        if (!conversationStarted) Initialize();

        if (activeLineIndex < conversations.lines.Length)
            DisplayLine();
        else
            AdvanceConversation();
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

    private void AdvanceConversation()
    {
        // These are really three types of dialog tree node
        // and should be three different objects with a standard interface

        if (conversations.nextConversation != null)
            ChangeConversation(conversations.nextConversation);
        else
            EndConversation();
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
