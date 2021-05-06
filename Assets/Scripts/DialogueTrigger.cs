using UnityEngine;

// Code referenced: https://www.youtube.com/watch?v=_nRzoTzeyxU&t=89s&ab_channel=Brackeys
//
//
//

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] private Dialogue dialogue;

    private void OnTriggerEnter(Collider other)
    {
        DialogueManager.Instance.StartDialogue(dialogue);
    }

    private void OnTriggerExit(Collider other)
    {
        DialogueManager.Instance.EndDialogue();
    }
}
