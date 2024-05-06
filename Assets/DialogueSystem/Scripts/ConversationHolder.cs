using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConversationHolder : MonoBehaviour
{
    [SerializeField] private GameObject dialogueTrigger;
    [SerializeField] private GameObject dialogueIndicator;
    [SerializeField] private Conversations conversation;

    private readonly bool disableTriggerWhenEnded = true;
    private bool conversationSet = false;

    private void Awake()
    {
        dialogueIndicator.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (!conversationSet)
            {
                conversationSet = true;
                DialogueDisplay.conversations = conversation;
            }

            dialogueIndicator.SetActive(true);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (disableTriggerWhenEnded && DialogueDisplay.isActive)
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
            dialogueIndicator.SetActive(false);
        }
    }
}
