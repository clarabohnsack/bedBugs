using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.Net.Sockets;


public class MenuController : MonoBehaviour
{
    [Header("PICK TRANSFORMS")]
    public Transform playTrans;
    public Transform quitTrans;

    [Header("BACKGROUND SPRITES")]
    public SpriteRenderer background;
    public SpriteRenderer quitGame;
    public SpriteRenderer loading;
    public Sprite[] textures;

    private bool inQuitScreen = false;
    private PlayerActions playerActions;


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
        loading.gameObject.transform.localScale = scale;
        quitGame.gameObject.transform.localScale = scale;
    }


    private void Start()
    {
        playerActions.Main.Confirm.performed += _ => confirmQuitGame();
        playerActions.Main.Quit.performed += _ => cancelQuitGame();
    }


    public void itemSelected(float pickPosition)
    {
        if (pickPosition == playTrans.position.y) playButton();
        else if (pickPosition == quitTrans.position.y) quitButton();
    }


    public void playButton()
    {
        Camera.main.transform.GetChild(1).GetComponent<Animation>().Play("FadeInAnim");
    }


    public void quitButton()
    {
        inQuitScreen = true;
        quitGame.color = Color.white;
    }


    private void confirmQuitGame()
    {
        if (inQuitScreen)
        {
            Application.Quit();
        }
    }

    private void cancelQuitGame()
    {
        if (inQuitScreen)
        {
            inQuitScreen = false;
            quitGame.color = Color.clear;
        }
    }


    public void creditsButton(bool show)
    {
        if (show) background.sprite = textures[1];
        else background.sprite = textures[0];
    }
}
