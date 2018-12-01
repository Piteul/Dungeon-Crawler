using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {

    int health = 100;
    int damage = 10;
    //Attack Distance by Box
    int attackDistance = 1;

    public GameObject DamageText;
    GameObject player;
    Toolbox toolbox;

    // Use this for initialization
    void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update() {


    }

    /// <summary>
    /// Control how many damage the enemy take and manage the damage display
    /// </summary>
    /// <param name="damage"></param>
    public void takeDamage(int damage) {

        health -= damage;
        Debug.Log(health);

        if (health > 0 && DamageText) {
            ShowDamageText(damage);
        }
        else {
            Destroy(this.gameObject);
        }
    }

    /// <summary>
    /// Instantiate the Damage Text
    /// </summary>
    void ShowDamageText(int dmg) {
        GameObject damageTmp = Instantiate(DamageText, transform.position, Quaternion.identity, transform);
        damageTmp.GetComponent<TextMesh>().text = dmg.ToString();
        damageTmp.transform.LookAt(player.transform);
    }


}
