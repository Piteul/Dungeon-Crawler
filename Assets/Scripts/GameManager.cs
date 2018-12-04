using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    public float gameTime = 59f;
    public Text gameTimeUi;
    public Image gameTimeImage;

    private float time;
	// Use this for initialization
	void Start () {
        time = gameTime;
	}

    public void CountDown()
    {
        time -= Time.deltaTime;

        int minutes = Mathf.FloorToInt(time / 60F);
        int seconds = Mathf.FloorToInt(time - minutes * 60);
        gameTimeUi.text = string.Format("{0:0}:{1:00}", minutes, seconds);

        gameTimeImage.color = new Vector4(gameTimeImage.color.r, gameTimeImage.color.g, gameTimeImage.color.b, time / gameTime); 
    }
	
	// Update is called once per frame
	void Update () {
        CountDown();

    }
}
