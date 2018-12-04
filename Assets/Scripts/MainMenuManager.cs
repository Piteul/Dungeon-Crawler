﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour {

    private bool sound = true;
    private bool show = true;
    private bool locked = false;
    public Text soundText;
    public Text titleText;
    public Text startText;
    public Text quitText;
    public Text[] textClignotant;
    // Use this for initialization
    void Start () {
        textClignotant = new Text[] { startText, quitText };
    }
	
	// Update is called once per frame
	void Update () {
        CoolFlash(titleText);
        if (!locked) { 

            StartCoroutine(Clignoter(textClignotant));

        }
    }

    void CoolFlash(Text text)
    {
        text.color = new Color(Random.value, Random.value, Random.value);
    }

    IEnumerator Clignoter(Text[] texts)
    {
        locked = true;
        yield return new WaitForSeconds(0.3f);
        if (show)
        {
            for(int i = 0; i < texts.Length; i++) {
                
                texts[i].color = new Color(texts[i].color.r, texts[i].color.g, texts[i].color.b, 1f);
            }
        }
        else
        {
            for (int i = 0; i < texts.Length; i++)
            {
                Debug.Log("changing");
                texts[i].color = new Color(texts[i].color.r, texts[i].color.g, texts[i].color.b, 0f);
            }
            
        }
        locked = false;
        show = !show;
    }
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }
    public void ExitGame()
    {
        Debug.Log("Clicked");
        Application.Quit();
    }
    public void SwitchSound()
    {
        sound = !sound;
        if (sound)
        {
            soundText.text = "Sound : ON";
        }
        else
        {
            soundText.text = "Sound : OFF";
        }
    }
}
