using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {

    float sizeBox = 4;
    public float health = 100f;

    Animator animator;
    PlayerMovement PlayerMovement;
    [HideInInspector]
    public Weapon weapon;
    Toolbox toolbox;

    // Use this for initialization
    void Start() {
        animator = GetComponent<Animator>();
        PlayerMovement = GetComponent<PlayerMovement>();
        weapon = GetComponentInChildren<Weapon>();
        toolbox = new Toolbox();
    }

    // Update is called once per frame
    void Update() {
        PlayerMovement.playerMovement();
        Attack();

    }


    public void Attack() {
        RaycastHit hit;
        int dmg;

        if (Input.GetMouseButtonDown(0)) {
            animator.SetTrigger("Attack");


            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, (sizeBox * weapon.attackDistance))) {
                if (hit.collider.tag == "Enemy") {
                    dmg = toolbox.randomDamage(weapon.damage, 3);
                    EnemyManager enemy = hit.collider.gameObject.GetComponent<EnemyManager>();
                    enemy.takeDamage(dmg);
                }
            }
            Debug.DrawRay(transform.position, this.transform.forward * (sizeBox * weapon.attackDistance), Color.black);

        }
    }

}
