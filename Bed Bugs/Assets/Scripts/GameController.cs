using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameController : MonoBehaviour
{
    [Header("GAME REFERENCES")]
    //public GameObject soundController;
    public SpriteRenderer background;
    //public SpriteRenderer loading;
    //public GameObject[] scenes;

    [Header("UI REFERENCES")]
    //public GameObject nextButton;
    //public TextMeshProUGUI topText;
    //public TextMeshProUGUI bottomText;

    [Header("NARRATIVE")]
    //public string[] topStrings;
    //public string[] bottomStrings;
    //public int[] interactionsRequired;

    [Header("AUDIO")]
    //public AudioSource dingSound;

    [Header("CONTROL")]
    private PlayerActions playerActions;
    private int currentScene = 0;
    private int currentInteractions = 0;


    private void OnEnable()
    {
        playerActions.Enable();
    }

    private void OnDisable()
    {
        playerActions.Disable();
    }


    void Awake()
    {
        playerActions = new PlayerActions();

        float worldScreenHeight = Camera.main.orthographicSize * 2f;
        float worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;

        Vector3 spriteSize = background.sprite.bounds.size;
        Vector3 scale = Vector3.one;
        scale.x = worldScreenWidth / spriteSize.x;
        scale.y = worldScreenHeight / spriteSize.y;

        background.gameObject.transform.localScale = scale;
        //loading.gameObject.transform.localScale = scale;
    }


    void Start()
    {
        playerActions.Main.Quit.performed += _ => goToMenu();

        currentScene = 0;
        currentInteractions = 0;

        //if (testingScene != 0) currentScene = testingScene;

        //scenes[currentScene].SetActive(true);
        //nextButton.SetActive(false);

        //topText.text = topStrings[currentScene];
        //bottomText.text = bottomStrings[currentScene];
    }


    public void checkInteractions()
    {
        /*currentInteractions += 1;

        if (currentInteractions == interactionsRequired[currentScene])
        {
            soundController.GetComponent<SoundController>().checkCompleteSound(currentScene);
            nextButton.SetActive(true);
            dingSound.Play();

            topText.gameObject.GetComponent<Animation>().Stop();
            bottomText.gameObject.GetComponent<Animation>().Stop();
        }*/
    }


    public void nextClick(Animation anim)
    {
        //anim.Play("FadeInOutAnim");
    }


    public void nextScene()
    {
        /*currentScene += 1;

        if (currentScene < scenes.Length)
        {
            currentInteractions = 0;
            if (interactionsRequired[currentScene] > 0) nextButton.SetActive(false);

            float randNum = Random.Range(-1.0f, 1.0f);
            background.flipX = (randNum > 0f);
            background.flipY = !background.flipY;

            scenes[currentScene].SetActive(true);
            scenes[currentScene - 1].SetActive(false);

            topText.text = topStrings[currentScene];
            bottomText.text = bottomStrings[currentScene];

            topText.gameObject.GetComponent<Animation>().Play();
            bottomText.gameObject.GetComponent<Animation>().Play();

            soundController.GetComponent<SoundController>().checkBackgroundSound(currentScene);
        }

        else
        {
            goToMenu();
        }*/
    }


    private void goToMenu()
    {
        //Camera.main.transform.GetChild(1).GetComponent<Animation>().Play("FadeInAnim");
    }
}
