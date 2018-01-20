﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum TotalAmmoDisplayType
{
    Hide,
    TotalAmmo,
    AmmoNotInMag
}

public class ReloadAnimationUI : MonoBehaviour {
    //getting tired (sorry for names)
    public Slider ReloadBar;
    public TotalAmmoDisplayType TotalAmmo;
    //replace with gun later and change to private
    public float Duration = 1;
    private Image readyImage;
    private Image fill;
    private Image background;
    private Text totalAmmo;
    private Text magAmmo;
    //private
    private float currentTime;
    private bool lerping = false;

    public void RefreshGunInfo()
    {
        //magAmmo = gun.inMag + "/" + gun.maxInMag;
        //totalAmmo = 
        if (TotalAmmo == TotalAmmoDisplayType.Hide)
        {
            totalAmmo.text = "";
        }
        else if (TotalAmmo == TotalAmmoDisplayType.TotalAmmo)
        {
            //totalAmmo.text = inMag + ammoReserve
        }
        else if (TotalAmmo == TotalAmmoDisplayType.AmmoNotInMag)
        {
            //totalAmmo.text = ammo reserve
        }

    }

    // Use this for initialization
    void Start () {
        
        foreach (Transform child in gameObject.transform)
        {
            if (child.name == "Background")
            {
                background = child.GetComponent<Image>();
            }
            else if (child.name == "Fill Area")
            {
                foreach (Transform childFill in child)
                {
                    if (childFill.name == "Fill")
                    {
                        fill = childFill.GetComponent<Image>();
                    }
                }
            }
            else if (child.name == "ReadyImage")
            {
                readyImage = child.GetComponent<Image>();
            }
            else if (child.name == "TotalAmmo")
            {
                totalAmmo = child.GetComponent<Text>();
            }
            else if (child.name == "MagAmmo")
            {
                magAmmo = child.GetComponent<Text>();
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
		if (lerping)
        {
            currentTime += Time.deltaTime;
            if (currentTime >= Duration)
            {
                lerping = false;
                Fill();
            }
            else
            {
                ReloadBar.value = Mathf.Lerp(ReloadBar.minValue, ReloadBar.maxValue, currentTime / Duration);
            }
        }
	}

    //start reload animation with specified duratuion set to 0/leave to use the duration that we currently have
    public void StartLerp(float duration = 0)
    {
        if (lerping == false)
        {
            Empty();
            if (duration > 0)
            {
                Duration = duration;
            }
            currentTime = 0;
            ReloadBar.value = ReloadBar.minValue;
            lerping = true;
        }
    }
    private void Empty()
    {
        readyImage.enabled = false;
        fill.enabled = true;
        background.enabled = true;
    }
    private void Fill()
    {
        readyImage.enabled = true;
        fill.enabled = false;
        background.enabled = false;
    }
    
}