using System.Collections.Generic;
using System.Collections;

using UnityEngine;
using TMPro;

// Code referenced: https://www.youtube.com/watch?v=_nRzoTzeyxU&t=89s&ab_channel=Brackeys
//
//
//

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    [SerializeField] private Queue<string> sentences;

    [SerializeField] private TMP_Text titleText;
    [SerializeField] private TMP_Text dialogueText;

    [SerializeField] private Animator animator;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        sentences = new Queue<string>();
    }

    internal void StartDialogue(Dialogue dialogue)
    {
        animator.SetBool("IsOpen", true);

        titleText.text = dialogue.title;

        sentences.Clear();

        foreach (string sentence in dialogue.setences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();

        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";

        AudioManager.Instance.Play("Keys Clicking Clacking " + Random.Range(1, 4));

        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;


            yield return new WaitForFixedUpdate();
        }
    }

    public void EndDialogue()
    {
        animator.SetBool("IsOpen", false);
    }
}
