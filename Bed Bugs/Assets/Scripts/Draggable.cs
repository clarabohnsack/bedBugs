using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draggable : MonoBehaviour
{
    public GameObject gameController;
    public GameObject deactivatedObject;
    // public AudioSource[] soundEffects;


    private SpriteRenderer sr;
    private Animation anim;

    private GameObject currentShadow = null;

    private Vector3 mousePositionOffset;
    private Vector3 initialPosition;
    private float cameraHeight, cameraWidth;
    private bool outOfBoundsX, outOfBoundsY = false;

    private bool locked = false;

    void Awake()
    {
        sr = transform.GetChild(0).GetComponent<SpriteRenderer>();
        anim = GetComponent<Animation>();
    }


    void Start()
    {
        initialPosition = transform.position;

        cameraHeight = 2f * Camera.main.orthographicSize;
        cameraWidth = cameraHeight * Camera.main.aspect;

        anim.Play();
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
            anim.Stop();
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
            if (currentShadow != null)
            {
                locked = true;

                transform.localScale = Vector3.one;
                transform.position = currentShadow.transform.position;

                deactivatedObject.SetActive(false);
                currentShadow.SetActive(false);

                gameController.GetComponent<GameController>().Next(true);
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
        }
    }


    void OnTriggerExit2D(Collider2D col)
    {
        if (gameObject.tag == col.gameObject.tag && col.gameObject.GetComponent<Draggable>() == null)
        {
            currentShadow = null;
        }
    }
}
