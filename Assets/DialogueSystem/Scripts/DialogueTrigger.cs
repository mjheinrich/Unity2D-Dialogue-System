using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    //public Dialogue dialogue;

    public Message[] messages;
    public Actor[] actors;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //TriggerDialogue();
            StartDialogue();
        }
    }

    /*public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }*/

    public void StartDialogue()
    {
        // replace with singleton pattern
        FindObjectOfType<DialogueManager>().OpenDialogue(messages, actors);
    }
}
