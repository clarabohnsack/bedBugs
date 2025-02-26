using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switchable : MonoBehaviour
{
    public Sprite[] textures;

    private SpriteRenderer sr;
    private Animation anim;

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
        sr.sprite = textures[0];
    }


    public void Switch()
    {
        if (!locked)
        {
            locked = true;
            anim.Stop();
            sr.sprite = textures[1];
            transform.localScale = Vector3.one;
        }
    }
}
