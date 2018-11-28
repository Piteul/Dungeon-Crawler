using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour {

    // Use this for initialization
    // testers for each direction 
    public bool wForward, wBackward, wLeft, wRight;
    // ray max distance, à noter que la valeur 4 atteint le milieu de la case suivante ce qui est parfait pour detecter les obstacles et les ennemis selon les config actuelles.
    public float rayDistance = 8f;

    //View
    public float viewDistanceByCase = 1;
    private float viewDistance;
    //Attack
    public float attackDistanceByCase;
    private float attackDistance;

    private enum States { passive, offensive };
    private States state = States.passive;

    public RayChecker rayCheck;
    GameObject player;


    void Start() {

        player = GameObject.FindGameObjectWithTag("Player");

        //Get the real view distance
        viewDistance = viewDistanceByCase * rayDistance;
        rayCheck = GetComponent<RayChecker>();

    }

    void Update() {

        //Put the enemy in front of the player
        transform.LookAt(player.transform);
        //transform.rotation = Quaternion.AngleAxis(30, Vector3.right);

        if (SeeThePlayer()) {
            state = States.offensive;



        }


    }


    private bool SeeThePlayer() {

        RaycastHit hit;
        Debug.DrawRay(transform.position, this.transform.forward * viewDistance, Color.yellow);


        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, viewDistance, 1)) {

            if (hit.collider.tag == "Player") {
                Debug.Log("I'm gonna kick your ass");
                return true;
            }

        }
        return false;
    }

}
