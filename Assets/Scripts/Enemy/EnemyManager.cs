using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {

    public int health = 100;
    public int healthDimmer = 20;
    public int damage = 10;
    public int damageDimmer = 3;
    //Attack Distance by Box
    public int attackDistance = 1;

    public GameObject DamageText;
    GameObject player;
    Toolbox toolbox;

    // Use this for initialization
    void Start() {
        toolbox = new Toolbox();
        player = GameObject.FindGameObjectWithTag("Player");
        health = toolbox.randomValue(health, healthDimmer);
    }

    // Update is called once per frame
    void Update() {

        //For test
        if (Input.GetKeyDown(KeyCode.P)) {
            Attack();
        }

    }

    /// <summary>
    /// Control how many damage the enemy take and manage the damage display
    /// </summary>
    /// <param name="damage"></param>
    public void TakeDamage(int damage) {

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

        dmg = toolbox.randomValue(damage, damageDimmer);
        Debug.Log("Damage : " + dmg);
        player.GetComponent<PlayerManager>().TakeDamage(dmg);
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
