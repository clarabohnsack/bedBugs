using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{

    private Queue<string> sentences;
    private Queue<string> names;

    public TMP_Text nameText;
    public TMP_Text dialogueText;
    public Animator animator;
    

    void Start()
    {
        sentences = new Queue<string>();
        names = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        
        animator.SetBool("isOpen", true);


        sentences.Clear();
        names.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        foreach (string name in dialogue.names)
        {
            names.Enqueue(name);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if(sentences.Count == 0)
        {
            EndDialogue();
            return;
        }
        string sentence = sentences.Dequeue();
        string name = names.Dequeue();
        StopAllCoroutines();
        nameText.text = name;
        StartCoroutine(TypeSentence(sentence));

    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";

        foreach(char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(0.01f); 
            //yield return null;
        }
    }

    void EndDialogue()
    {
        animator.SetBool("isOpen", false);
        Debug.Log("End of Dialogue");
    }
}
