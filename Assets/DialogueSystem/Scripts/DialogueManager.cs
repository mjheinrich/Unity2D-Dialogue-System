using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public Image actorImage;
    public TextMeshProUGUI actorName;
    public TextMeshProUGUI messageText;
    public RectTransform backgroundBox;

    private Message[] currentMessages;
    private Actor[] currentActors;

    private int activeMessage = 0;

    public static bool isActive;

    public float typingSpeed = 0.04f;
    private Coroutine displayLineCoroutine;
    private bool canContinueToNextLine = false;
    public GameObject continueButton;

    private void Start()
    {
        backgroundBox.transform.localScale = Vector3.zero;
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
        currentMessages = messages;
        currentActors = actors;
        activeMessage = 0;
        isActive = true;

        Debug.Log("Started conversation. Loaded messages: " + messages.Length);
        DisplayMessage();

        // animate dialogue box using LeanTween
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
        actorImage.sprite = actorToDisplay.sprite;
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
            // scale down the dialogue box using LeanTween
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
            messageText.text += c;
            yield return new WaitForSeconds(typingSpeed);
        }

        continueButton.SetActive(true);

        canContinueToNextLine = true;
    }
}
