using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConversationHolder : MonoBehaviour
{
    public Message[] messages;
    public Actor[] actors;

    public GameObject dialogueBox;

    public GameObject dialogueTrigger;
    public DialogueManager dialogueManager;

    //public DialogueDisplay dialogueDisplay;
    //public Conversations conversation;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && DialogueManager.isActive == false)
        {
            //dialogueBox.SetActive(true);
            //dialogueDisplay.conversations = conversation;
            StartDialogue();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //dialogueDisplay.ChangeConversation(conversation.nextConversation);
            //dialogueTrigger.SetActive(false);
            //dialogueBox.SetActive(false);
        }
    }

    public void StartDialogue()
    {
        // replace with singleton pattern
        FindObjectOfType<DialogueManager>().OpenDialogue(messages, actors);
    }
}
