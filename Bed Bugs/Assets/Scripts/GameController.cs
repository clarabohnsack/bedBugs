using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class GameController : MonoBehaviour
{
    [Header("SCENES")]
    public GameObject sleepoverScene;
    public GameObject medievalScene;
    public GameObject spaceScene;
    public GameObject westernScene;
    public GameObject pirateScene;


    [Header("BACKGROUND")]
    public SpriteRenderer background;
    public Sprite[] backgroundOptions;


    [Header("UI")]
    public float dialogueSpeed = 0.05f;
    public TMP_Text dialogueText;
    public TMP_Text nameText;
    public Image characterPhoto;
    public Sprite[] characterOptions;
    public Image transition;


    [Header("NARRATIVE")]
    public string[] dialogueList;
    public string[] nameList;


    [Header("CONTROL")]
    private string currentScene = "SLEEPOVER"; // 0: SLEEPOVER, 1: MEDIEVAL, 2: SPACE, 3: WESTERN, 4: PIRATE
    private int currentDialogue = 0;


    void Awake()
    {
        float worldScreenHeight = Camera.main.orthographicSize * 2f;
        float worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;

        Vector3 spriteSize = background.sprite.bounds.size;
        Vector3 scale = Vector3.one;
        scale.x = worldScreenWidth / spriteSize.x;
        scale.y = worldScreenHeight / spriteSize.y;

        background.gameObject.transform.localScale = scale;
    }


    void Start()
    {
        currentScene = "SLEEPOVER";
        showScene();

        currentDialogue = 0;
        nameText.text = nameList[currentDialogue];
        StartCoroutine(TypeSentence(dialogueList[currentDialogue]));
    }


    private void showScene()
    {
        sleepoverScene.SetActive(false);
        medievalScene.SetActive(false);
        spaceScene.SetActive(false);
        westernScene.SetActive(false);
        pirateScene.SetActive(false);

        int backgroundNumber = 0;

        switch (currentScene)
        {
            case "SLEEPOVER":
                sleepoverScene.SetActive(true);
                backgroundNumber = 0;
                break;

            case "MEDIEVAL":
                medievalScene.SetActive(true);
                backgroundNumber = 1;
                break;

            case "SPACE":
                spaceScene.SetActive(true);
                backgroundNumber = 2;
                break;

            case "WESTERN":
                westernScene.SetActive(true);
                backgroundNumber = 3;
                break;

            case "PIRATE":
                pirateScene.SetActive(true);
                backgroundNumber = 4;
                break;
        }

        background.sprite = backgroundOptions[backgroundNumber];
    }


    public void Next()
    {
        currentScene = "SLEEPOVER";
        showScene();

        currentDialogue += 1;
        nameText.text = nameList[currentDialogue];
        StartCoroutine(TypeSentence(dialogueList[currentDialogue]));
    }


    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";

        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(dialogueSpeed);
        }
    }
}
