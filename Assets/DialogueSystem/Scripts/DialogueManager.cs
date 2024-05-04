using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    [Header("Dialogue Box")]
    public GameObject dialogueBox;
    public float typingSpeed = 0.005f;
    public TextMeshProUGUI actorName;
    public TextMeshProUGUI messageText;
    public RectTransform backgroundBox;
    public GameObject continueButton;

    [Header("Left Actor")]
    public Image actorImageLeft;
    public GameObject actorImageLeftObject;

    [Header("Right Actor")]
    public Image actorImageRight;
    public GameObject actorImageRightObject;

    private Message[] currentMessages;
    private Actor[] currentActors;

    private int activeMessage = 0;

    public static bool isActive;

    private Coroutine displayLineCoroutine;
    private bool canContinueToNextLine = false;

    bool isAddingRichTextTag = false;

    private void Start()
    {
        backgroundBox.transform.localScale = Vector3.zero;
        actorImageLeftObject.SetActive(false);
        actorImageRightObject.SetActive(false);
    }

    public void Update()
    {
        // advance dialogue using key instead of UI button
        if (Input.GetKeyDown(KeyCode.E) && isActive == true && canContinueToNextLine == true)
        {
            NextMessage();
        }
    }

    public void OpenDialogue(Message[] messages, Actor[] actors)
    {
        backgroundBox.transform.localScale = Vector3.one;

        currentMessages = messages;
        currentActors = actors;
        activeMessage = 0;
        isActive = true;

        Debug.Log("Started conversation. Loaded messages: " + messages.Length);
        DisplayMessage();
    }

    void DisplayMessage()
    {
        Message messageToDisplay = currentMessages[activeMessage];
        //messageText.text = messageToDisplay.messages;

        if (displayLineCoroutine != null)
        {
            StopCoroutine(displayLineCoroutine);
        }

        displayLineCoroutine = StartCoroutine(TypeLine(messageToDisplay.messages));

        Actor actorToDisplay = currentActors[messageToDisplay.actorId];
        actorName.text = actorToDisplay.name;

        if (messageToDisplay.actorId == 1)
        {
            actorImageLeftObject.SetActive(true);
            actorImageRightObject.SetActive(false);
            //Mood = messageToDisplay.mood;
            //actorImageLeft.sprite = actorToDisplay.expression[messageToDisplay.currentExpression];
        }
        else if (messageToDisplay.actorId == 0)
        {
            actorImageLeftObject.SetActive(false);
            actorImageRightObject.SetActive(true);
            //Mood = messageToDisplay.mood;
            //actorImageRight.sprite = actorToDisplay.sprite;
            //actorImageRight.sprite = actorToDisplay.expression[messageToDisplay.currentExpression];
        }
    }

    public void NextMessage()
    {
        activeMessage++;
        if (activeMessage < currentMessages.Length)
        {
            DisplayMessage();
        }
        else
        {
            isActive = false;
            //dialogueBox.SetActive(false);
            backgroundBox.transform.localScale = Vector3.zero;
            Debug.Log("Conversation ended.");
        }
    }

    IEnumerator TypeLine(string line)
    {
        // empty the dialogue text
        messageText.text = "";

        continueButton.SetActive(false);

        canContinueToNextLine = false;

        foreach (char c in line.ToCharArray())
        {
            // if the advance button is pressed, finish the sentence immediately
            if (Input.GetKey(KeyCode.Space))
            {
                messageText.text = line;
                break;
            }

            if (c == '<' || isAddingRichTextTag)
            {
                isAddingRichTextTag = true;
                messageText.text += c;

                if (c == '>')
                {
                    isAddingRichTextTag = false;
                }
            }
            else
            {
                messageText.text += c;
                yield return new WaitForSeconds(typingSpeed);
            }
        }

        continueButton.SetActive(true);

        canContinueToNextLine = true;
    }
}
