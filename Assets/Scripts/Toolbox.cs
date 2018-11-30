using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toolbox {

    float sizeBox = 4f;

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    /// <summary>
    /// Return a soft randomized damage depending on dimmer
    /// </summary>
    /// <param name="damage"></param>
    public int randomDamage(int damage, int dimmer) {

        int rd = Random.Range(Mathf.Max(0, damage - dimmer), damage + dimmer + 1);
        //Debug.Log(rd.ToString());

        return rd;
    }
}
