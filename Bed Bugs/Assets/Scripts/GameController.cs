using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameController : MonoBehaviour
{
    [Header("CONTROL")]
    public string nextScene = "SCENE";
    public int interactions = 0;
    public GameObject interactables;
    private int currentDialogue = 0;


    [Header("UI")]
    public float dialogueSpeed = 0.025f;
    public Image dialogueBubble;
    public TMP_Text dialogueText;
    public Animator transition;
    public GameObject nextButton;


    [Header("NARRATIVE")]
    public string[] dialogueList;
    public Sprite[] dialogueCharacters;
    public Animation[] characterAnimations;


    [Header("SOUNDS")]
    public AudioSource[] voiceSounds;
    public AudioSource[] dreamSounds;


    void Start()
    {
        nextButton.SetActive(false);
        transition.Play("FadeOut");
        dreamSounds[0].Play();
        currentDialogue = 0;
        if (dialogueBubble != null) dialogueBubble.sprite = dialogueCharacters[currentDialogue];
        if (characterAnimations.Length != 0) characterAnimations[currentDialogue].Play();
        StartCoroutine(TypeSentence(dialogueList[currentDialogue]));
    }


    public void Next(bool playing = false)
    {
        if (characterAnimations.Length != 0) characterAnimations[currentDialogue].Stop();
        currentDialogue += 1;

        if (currentDialogue >= dialogueList.Length)
        {
            nextButton.SetActive(false);
            if (dialogueBubble != null) dialogueBubble.gameObject.SetActive(false);
            if (interactables != null) interactables.SetActive(true);

            if (playing) interactions -= 1;

            if (interactions <= 0)
            {
                StartCoroutine(LoadNextScene());

            }
        }

        else
        {
            if (dialogueBubble != null) dialogueBubble.sprite = dialogueCharacters[currentDialogue];
            if (characterAnimations.Length != 0) characterAnimations[currentDialogue].Play();
            StartCoroutine(TypeSentence(dialogueList[currentDialogue]));
        }
        
    }


    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        nextButton.SetActive(false);

        voiceSounds[(currentDialogue + dialogueList.Length) % voiceSounds.Length].Play();

        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(dialogueSpeed);
        }

        nextButton.SetActive(true);
    }


    IEnumerator LoadNextScene()
    {
        transition.Play("FadeIn");
        dreamSounds[1].Play();
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(nextScene);
    }
}
