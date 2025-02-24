using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class WobbleTestManager : MonoBehaviour
{

    public WobbleEffect wobbleEffect;
    private Keyboard keyboard;
    // Start is called before the first frame update
    void Start()
    {
        keyboard = keyboard.current;
    }

    // Update is called once per frame
    void Update()
    {
        if(keyboard.aKey.wasPressedThisFrame)
        {

        }
        else if(keyboard.bKey.wasPressedThisFrame)
        {

        }
    }

    private void WobbleOn()
    {
        wobbleEffect.enabled = true;
        wobbleEffect.StartWobble();
    }

    private void WobbleOff()
    {
        wobbleEffect.StopWobble();
    }
}
