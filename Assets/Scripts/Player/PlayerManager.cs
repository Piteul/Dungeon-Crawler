using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {

    public float health = 100f;
    public float attackPower = 20f;

    public float sizeCase = 4;

    Animator animator;
    PlayerMovement PlayerMovement;
    Weapon weapon;
    // Use this for initialization
    void Start () {
        animator = GetComponent<Animator>();
        PlayerMovement = GetComponent<PlayerMovement>();
        weapon = GetComponentInChildren<Weapon>();

    }

    // Update is called once per frame
    void Update () {
        PlayerMovement.playerMovement();
		
	}


    public void Attack() {
        RaycastHit hit;

        if (Input.GetMouseButtonDown(0)) {
            animator.SetTrigger("Attack");

            //if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, weapon.attackDistance * sizeCase, 1)) {
            //    if (hit.collider.tag == "Enemy") {
            //        //Debug.Log("Can't go forward baby!");
            //        wForward = false;
            //    }
            //}
            //Debug.DrawRay(transform.position, this.transform.forward * rayDistance, Color.blue);
        }
    }
}
