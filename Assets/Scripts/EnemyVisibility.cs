using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum playerLastOrientation { Front, Left,Right};

public class EnemyVisibility : MonoBehaviour {

    // Use this for initialization
    // testers for each direction 
    public bool wForward, wBackward, wLeft, wRight;
    // ray max distance, à noter que la valeur 4 atteint le milieu de la case suivante ce qui est parfait pour detecter les obstacles et les ennemis selon les config actuelles.
    public float rayDistance = 8f;

    //View
    public float viewDistanceByCase = 1;
    [HideInInspector]
    public float viewDistance;
    //Attack
    public float attackDistanceByCase;
    private float attackDistance;

    public float distanceToPlayer = Mathf.Infinity;

    private enum States { passive, offensive };
    private States state = States.passive;

    public float viewField;
    public RayChecker rayCheck;
    GameObject player;

    [HideInInspector]
    public playerLastOrientation plOrientation = playerLastOrientation.Front;


    void Start() {

        player = GameObject.FindGameObjectWithTag("Player");

        //Get the real view distance
        viewDistance = viewDistanceByCase * rayDistance;
        rayCheck = GetComponent<RayChecker>();

    }

    void Update() {

        //Put the enemy in front of the player
        //transform.LookAt(player.transform);
        //transform.rotation = Quaternion.AngleAxis(30, Vector3.right);

       /* if (CheckVisibilty()) {
            state = States.offensive;

        }*/


    }


    public bool CheckVisibilty() {

       /* RaycastHit hit;
        Debug.DrawRay(transform.position, this.transform.forward * viewDistance, Color.yellow);

        // he attac
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, viewDistance, 1))
        {

            if (hit.collider.tag == "Player")
            {
                Debug.Log("I'm gonna kick your ass");

                return true;
            }

        }*/
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        // but most importantly he sees
        // Debug.Log(viewDistance);
        Vector3 direction = player.transform.position - transform.position;
        Debug.Log(Vector3.Distance(player.transform.position, transform.position));
        if (Vector3.Distance(player.transform.position,transform.position) <= viewDistance+2 && Vector3.Angle(direction, this.transform.forward) <= 45) // 
        {
            float signedAngle = Vector3.SignedAngle(direction, this.transform.forward, Vector3.up);
            if (signedAngle > -30 && signedAngle < 30)            
            {
                plOrientation = playerLastOrientation.Front;
                //Debug.Log("you're infront of me bitch");
                return true;
            }

            if(signedAngle < -30)
            {
                plOrientation = playerLastOrientation.Right;
                return true;
                //Debug.Log("you're on my right bitch");
            }
            if (signedAngle > 30)
            {
                plOrientation = playerLastOrientation.Left;
                return true;
                //Debug.Log("you're on my left bitch");
            }

        }
        return false;
    }

}
