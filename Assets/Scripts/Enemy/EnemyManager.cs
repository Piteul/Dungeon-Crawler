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
        //Debug.Log(health);

        if (health > 0 && DamageText) {
            ShowDamageText(damage);
        }
        else {
            Destroy(this.gameObject);
        }
    }

    public void Attack() {
        int dmg;

        dmg = toolbox.randomDamage(damage, 3);
        player.GetComponent<PlayerManager>().takeDamage(dmg) ;
    }

    /// <summary>
    /// Instantiate the Damage Text
    /// </summary>
    public void ShowDamageText(int dmg) {
        Color color = new Color(1f, 1f, 1f);

        GameObject damageTmp = Instantiate(DamageText, transform.position, Quaternion.identity, transform);
        damageTmp.GetComponent<TextMesh>().text = dmg.ToString();

        if (dmg < player.GetComponent<PlayerManager>().weapon.damage) {
            color = new Color(0.88f, 0.86f, 0.15f);
        }
        else if (dmg == player.GetComponent<PlayerManager>().weapon.damage) {

            color = new Color(0.95f, 0.70f, 0.15f);
        }
        else if (dmg > player.GetComponent<PlayerManager>().weapon.damage) {

            color = new Color(0.69f, 0.09f, 0.09f);
        }

        damageTmp.GetComponent<TextMesh>().color = color;
        damageTmp.transform.LookAt(player.transform);
    }


}
