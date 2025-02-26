using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static TreeEditor.TreeEditorHelper;

public class Clickable : MonoBehaviour
{
    public int type = 0; // 1: ONCE, 2: MANY, 3: HIDE, 4: MOVE

    public GameObject gameController;

    // public AudioSource[] soundEffects;

    // public Sprite[] textures;

    // public GameObject activatedObject;
    // public GameObject deactivatedObject;

    // public bool soundAtStart = false;

    // public bool loopTextures = true;

    private SpriteRenderer sr;
    private Animation anim;

    // private int currentTex = 0;

    private int currentInteractions = 0;

    public Rigidbody2D[] cannonballs;

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
        // currentTex = 0;
        // sr.sprite = textures[0];
        anim.Play();

        // if (soundAtStart) soundEffects[0].Play();
    }


    private void OnMouseDown()
    {
        if (type == 1 && !locked)
        {
            locked = true;
            anim.Stop();
            gameController.GetComponent<GameController>().checkInteractions();
            /*sr.sprite = textures[1];
            if (!soundAtStart)  soundEffects[0].Play();
            transform.localScale = Vector3.one;

            if (activatedObject != null) activatedObject.SetActive(true);
            if (deactivatedObject != null) deactivatedObject.SetActive(false);*/
        }

        else if (type == 2 && !locked)
        {
            gameController.GetComponent<GameController>().checkInteractions();

            /*soundEffects[currentTex].Stop();

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
            if (deactivatedObject != null) deactivatedObject.SetActive(false);*/
        }

        else if (type == 3 && !locked)
        {
            locked = true;
            anim.Stop();
            gameController.GetComponent<GameController>().checkInteractions();
            sr.color = Color.clear;
            /*if (!soundAtStart) soundEffects[0].Play();
            transform.localScale = Vector3.one;

            if (activatedObject != null) activatedObject.SetActive(true);
            if (deactivatedObject != null) deactivatedObject.SetActive(false);*/
        }

        else if (type == 4 && !locked)
        {
            locked = true;
            cannonballs[currentInteractions].bodyType = RigidbodyType2D.Dynamic;
            cannonballs[currentInteractions].AddForce(new Vector2(350f, 250f), ForceMode2D.Force);
            currentInteractions += 1;
            StartCoroutine(MoveOctopus());
        }
    }
    
    IEnumerator MoveOctopus()
    {
        yield return new WaitForSeconds(1.5f);

        Vector3 targetPos = new Vector3(transform.position.x, transform.position.y - 4f, transform.position.z);
        
        while(transform.position != targetPos)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, 20f * Time.deltaTime);
            yield return null;
        }

        yield return new WaitForSeconds(1f);

        targetPos = new Vector3(transform.position.x + 4.25f, transform.position.y, transform.position.z);
        transform.position = targetPos;

        yield return new WaitForSeconds(1f);

        targetPos = new Vector3(transform.position.x, transform.position.y + 4f, transform.position.z);
        
        while (transform.position != targetPos)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, 10f * Time.deltaTime);
            yield return null;
        }

        locked = false;
        Destroy(cannonballs[currentInteractions - 1].gameObject);
    }
}
