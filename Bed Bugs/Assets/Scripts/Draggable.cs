using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draggable : MonoBehaviour
{
    public int type = 1; // 1: GAME, 2: MENU

    public int uses = 1;

    public bool hideAfter = false;

    public bool soundAtStart = false;

    public GameObject gameController;

    public AudioSource[] soundEffects;

    public Sprite placedTexture;

    private SpriteRenderer sr;
    private Animation anim;
    private Sprite initialSprite;

    private GameObject currentShadow = null;
    private GameObject currentSwitchable = null;

    private Vector3 mousePositionOffset;
    private Vector3 initialPosition;
    private float cameraHeight, cameraWidth;
    private bool outOfBoundsX, outOfBoundsY = false;

    private bool locked = false;
    private int currentUses = 0;


    void Awake()
    {
        sr = transform.GetChild(0).GetComponent<SpriteRenderer>();
        anim = GetComponent<Animation>();
    }


    void Start()
    {
        locked = false;
        initialPosition = transform.position;

        initialSprite = sr.sprite;

        cameraHeight = 2f * Camera.main.orthographicSize;
        cameraWidth = cameraHeight * Camera.main.aspect;

        anim.Play();

        if (soundAtStart) soundEffects[1].Play();
    }


    private Vector3 GetMouseWorldPosition()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }


    private void OnMouseDown()
    {
        if (!locked)
        {
            mousePositionOffset = gameObject.transform.position - GetMouseWorldPosition();
            soundEffects[0].Play();
            anim.Stop();
            sr.sprite = initialSprite;
        }  
    }


    private void OnMouseDrag()
    {
        if (!locked)
        {
            transform.position = GetMouseWorldPosition() + mousePositionOffset;
        }
    }


    private void OnMouseUp()
    {
        if (!locked)
        {
            if (type == 1)
            {
                if (currentShadow != null)
                {
                    transform.localScale = Vector3.one;
                    transform.position = currentShadow.transform.position;

                    currentShadow.SetActive(false);

                    if (currentSwitchable != null)
                    {
                        currentSwitchable.GetComponent<Switchable>().Switch();
                        currentSwitchable.GetComponent<BoxCollider2D>().enabled = false;
                        currentSwitchable = null;
                    }

                    if (placedTexture != null) sr.sprite = placedTexture;
                    if (!soundAtStart) soundEffects[1].Play();

                    currentUses += 1;

                    if (currentUses >= uses)
                    {
                        locked = true;
                        anim.Stop();
                        if (hideAfter) sr.color = Color.clear;
                    }

                    gameController.GetComponent<GameController>().checkInteractions();
                }

                else
                {
                    anim.Play();
                }
            }

            else if (type == 2)
            {
                soundEffects[1].Play();
                anim.Play();
                if (currentShadow != null) gameController.GetComponent<MenuController>().itemSelected(currentShadow.transform.position.y);
            }


            outOfBoundsX = transform.position.x > (cameraWidth / 2) || transform.position.x < -(cameraWidth / 2);
            outOfBoundsY = transform.position.y > (cameraHeight / 2) || transform.position.y < -(cameraHeight / 2);

            if (outOfBoundsX || outOfBoundsY)
            {
                transform.position = initialPosition;
            }
        }
    }


    void OnTriggerEnter2D(Collider2D col)
    {
        if (gameObject.tag == col.gameObject.tag && col.gameObject.GetComponent<Draggable>() == null)
        {
            currentShadow = col.gameObject;

            if (type == 2 && currentShadow.transform.position.y < 0f) gameController.GetComponent<MenuController>().creditsButton(true);
        }

        if (col.gameObject.GetComponent<Switchable>() != null)
        {
            currentSwitchable = col.gameObject;
        }
    }


    void OnTriggerExit2D(Collider2D col)
    {
        if (gameObject.tag == col.gameObject.tag && col.gameObject.GetComponent<Draggable>() == null)
        {
            if (type == 2 && currentShadow.transform.position.y < 0f) gameController.GetComponent<MenuController>().creditsButton(false);

            currentShadow = null;
        }

        if (col.gameObject.GetComponent<Switchable>() != null)
        {
            currentSwitchable = null;
        }
    }
}
