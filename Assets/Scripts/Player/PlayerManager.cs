using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour {

    float sizeBox = 4;
    public float health = 100f;

    Animator animator;
    PlayerMovement PlayerMovement;
    [HideInInspector]
    public Weapon weapon;
    Toolbox toolbox;

    //UI
    public Slider healthBar;

    // Use this for initialization
    void Start() {
        animator = GetComponent<Animator>();
        PlayerMovement = GetComponent<PlayerMovement>();
        weapon = GetComponentInChildren<Weapon>();
        toolbox = new Toolbox();
        healthBar.value = health;
    }

    // Update is called once per frame
    void Update() {
        PlayerMovement.playerMovement();
        Attack();


        //For test
        if (Input.GetKeyDown(KeyCode.P)) {
            TakeDamage(20);
        }

    }

    /// <summary>
    /// Manage the player attack
    /// </summary>
    public void Attack() {
        RaycastHit hit;
        int dmg;

        if (Input.GetMouseButtonDown(0)) {
            animator.SetTrigger("Attack");


            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, (sizeBox * weapon.attackDistance))) {
                if (hit.collider.tag == "Enemy") {
                    dmg = toolbox.randomDamage(weapon.damage, 3);
                    EnemyManager enemy = hit.collider.gameObject.GetComponent<EnemyManager>();
                    enemy.TakeDamage(dmg);
                }
            }
            Debug.DrawRay(transform.position, this.transform.forward * (sizeBox * weapon.attackDistance), Color.black);

        }
    }

    /// <summary>
    /// Control how many damage the player take
    /// </summary>
    /// <param name="damage"></param>
    public void TakeDamage(int damage) {

        health -= damage;
        UIGestion();
        Debug.Log(health);

    }

    public void UIGestion() {
        healthBar.value = health;
    }
}
