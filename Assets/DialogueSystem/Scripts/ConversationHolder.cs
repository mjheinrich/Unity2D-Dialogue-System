using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConversationHolder : MonoBehaviour
{
    //public Message[] messages;
    //public Actor[] actors;

    //public GameObject dialogueBox;

    public GameObject dialogueTrigger;
    public GameObject dialogueIndicator;
    //public DialogueManager dialogueManager;

    public DialogueDisplay dialogueDisplay;
    public Conversations conversation;

    private bool disableTriggerWhenEnded = false;
    //public bool canBeDisabled = false;

    private bool conversationSet = false;
    //private bool conversationReset = false;
    //private bool convoHasChanged = false;

    private void Awake()
    {
        dialogueIndicator.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (conversationSet == false)
            {
                conversationSet = true;
                dialogueDisplay.conversations = conversation;
            }

            /*if (conversationReset)
            {
                dialogueDisplay.conversations = conversation;
            }*/

            dialogueIndicator.SetActive(true);
            //convoHasChanged = false;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (disableTriggerWhenEnded && DialogueDisplay.isActive == true)
            {
                dialogueTrigger.SetActive(false);
                dialogueIndicator.SetActive(false);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            /*if (convoHasChanged == false)
            {
                convoHasChanged = true;
                dialogueDisplay.ChangeConversation(conversation.nextConversation);
            }*/

            /*if (disableTriggerWhenEnded && DialogueDisplay.isActive == true)
            {
                dialogueTrigger.SetActive(false);
            }*/
            /*if (!disableTriggerWhenEnded)
            {
                conversationReset = true;
            }*/

            //dialogueBox.SetActive(false);
        }
    }

    /*public void StartDialogue()
    {
        // replace with singleton pattern
        FindObjectOfType<DialogueManager>().OpenDialogue(messages, actors);
    }*/
}
