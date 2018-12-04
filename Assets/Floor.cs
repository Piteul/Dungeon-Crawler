using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour {

    public List<Sprite> sprites;

    // Use this for initialization
    void Start() {

        //Debug.Log(sprites.Count);
        if (sprites.Count > 0) {
            this.GetComponent<SpriteRenderer>().sprite = sprites[Random.Range(0, sprites.Count)];

        }
    }

    // Update is called once per frame
    void Update() {

    }
}
