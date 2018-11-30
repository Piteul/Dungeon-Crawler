using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {

    public float health = 100f;
    public float attackPower = 20f;

    float sizeCase = 4;

    Animator animator;
    PlayerMovement PlayerMovement;
    Weapon weapon;
    // Use this for initialization
    void Start() {
        animator = GetComponent<Animator>();
        PlayerMovement = GetComponent<PlayerMovement>();
        weapon = GetComponentInChildren<Weapon>();

    }

    // Update is called once per frame
    void Update() {
        PlayerMovement.playerMovement();
        Attack();

    }


    public void Attack() {
        RaycastHit hit;

        if (Input.GetMouseButtonDown(0)) {
            animator.SetTrigger("Attack");


            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, (sizeCase * weapon.attackDistance))) {
                if (hit.collider.tag == "Enemy") {
                    Debug.Log("Headshot");
                }
            }
            Debug.DrawRay(transform.position, this.transform.forward * (sizeCase * weapon.attackDistance), Color.black);

        }
    }
}
