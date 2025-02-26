using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clickable : MonoBehaviour
{
    public int type = 0; // 1: ONCE, 2: ANY, 3: HIDE

    public GameObject gameController;

    public AudioSource[] soundEffects;

    public Sprite[] textures;

    public GameObject activatedObject;
    public GameObject deactivatedObject;

    public bool soundAtStart = false;

    public bool loopTextures = true;

    private SpriteRenderer sr;
    private Animation anim;

    private int currentTex = 0; 

    private bool locked = false;  


    void Awake()
    {
        sr = transform.GetChild(0).GetComponent<SpriteRenderer>();
        anim = GetComponent<Animation>();
    }


    void Start()
    {
        gameObject.SetActive(true);
        locked = false;
        currentTex = 0;
        sr.sprite = textures[0];
        anim.Play();

        if (soundAtStart) soundEffects[0].Play();
    }


    private void OnMouseDown()
    {
        if (type == 1 && !locked)
        {
            locked = true;
            anim.Stop();
            gameController.GetComponent<GameController>().checkInteractions();
            sr.sprite = textures[1];
            if (!soundAtStart)  soundEffects[0].Play();
            transform.localScale = Vector3.one;

            if (activatedObject != null) activatedObject.SetActive(true);
            if (deactivatedObject != null) deactivatedObject.SetActive(false);
        }

        else if (type == 2 && !locked)
        {
            gameController.GetComponent<GameController>().checkInteractions();

            soundEffects[currentTex].Stop();

            currentTex += 1;

            if (loopTextures && currentTex == textures.Length)
            {
                currentTex = 0;
            }

            else if (!loopTextures && currentTex == textures.Length - 1)
            {
                locked = true;
                anim.Stop();
            }
                
            sr.sprite = textures[currentTex];
            soundEffects[currentTex].Play();
            transform.localScale = Vector3.one;

            if (activatedObject != null) activatedObject.SetActive(true);
            if (deactivatedObject != null) deactivatedObject.SetActive(false);
        }

        else if (type == 3 && !locked)
        {
            locked = true;
            anim.Stop();
            gameController.GetComponent<GameController>().checkInteractions();
            sr.color = Color.clear;
            if (!soundAtStart) soundEffects[0].Play();
            transform.localScale = Vector3.one;

            if (activatedObject != null) activatedObject.SetActive(true);
            if (deactivatedObject != null) deactivatedObject.SetActive(false);
        }
    }  
}
