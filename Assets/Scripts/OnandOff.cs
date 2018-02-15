using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnandOff : MonoBehaviour
{

    AudioSource flashClick;

    [SerializeField]
    Image fillImage;

    [SerializeField]
    Light flashLight;

    [SerializeField]
    private float timeDecay = .05f;

    private Color green = Color.green;
    private Color yellow = Color.yellow;
    private Color red = Color.red;
    private Color currentTargetColor;

    private float drain;
    private bool isOn;
    
	void Awake ()
    {
        flashClick = GetComponent<AudioSource>();
	}

    private void Start()
    {
        drain = 0;
        //flashLight = thisFlashLight.GetComponent<Light>();
        isOn = true;
    }
    // Update is called once per frame
    void Update ()
    {
        CheckInput();
        CheckDrain();

        ChangeCurrentTargetColor();
        LerpColor();

    }

    private void CheckLight()
    {
        if(!isOn)
        {
            flashLight.enabled = false;
        }
    }

    void CheckInput()
    {
        if (Input.GetButtonDown("Flashlight"))
        {
            //flashlight.SetActive(!gameObject.activeSelf);
            if (flashLight.enabled == true)
            {
                flashLight.enabled = false;
                flashClick.PlayDelayed(0);
                isOn = false;
            }

            else
            {
                flashLight.enabled = true;
                flashClick.PlayDelayed(0);
                isOn = true;
            }
        }
    }

    void CheckDrain()
    {
        if(isOn)
        {
            drain += timeDecay * Time.deltaTime;
            if (drain > 1)
            {
                drain = 1;
            }
            fillImage.fillAmount = 1 - drain;
            if(fillImage.fillAmount <= 0)
            {
                isOn = false;
                CheckLight();
            }
        }
        else
        {
            drain -= 2*timeDecay * Time.deltaTime;
            if(drain < 0)
            {
                drain = 0;
            }
            fillImage.fillAmount = 1 - drain;
        }
    }

    private void ChangeCurrentTargetColor()
    {
        if (drain > 0.3 && drain < 0.7)
        {
            currentTargetColor = yellow;
        }
        else if (drain > 0.7)
        {
            currentTargetColor = red;
        }
        else
        {
            currentTargetColor = green;
        }
    }

    private void LerpColor()
    {
        fillImage.color = Color.Lerp(fillImage.color, currentTargetColor, .02f);
    }
}
