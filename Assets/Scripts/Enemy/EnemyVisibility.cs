using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum playerLastOrientation { Front, Left,Right};
public enum Orientation { Forward, Backward,Left,Right};
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
    public int attackDistanceByCase = 1;
    // i don't think we need this viewDistance is sufficient
    [HideInInspector]
    public float attackDistance;
    [HideInInspector]
    public Orientation orientation = Orientation.Forward; 
    public float distanceToPlayer = 0;
    public Vector3 playerLastPosition = Vector3.zero;

    private enum States { passive, offensive };
    private States state = States.passive;

    public float viewField = 45f;
    GameObject player;

    [HideInInspector]
    public playerLastOrientation plOrientation = playerLastOrientation.Front;


    void Start() {

        player = GameObject.FindGameObjectWithTag("Player");

        //Get the real view distance
        viewDistance = viewDistanceByCase * rayDistance;
        attackDistance = attackDistanceByCase * rayDistance;
        //rayCheck = GetComponent<RayChecker>();

    }

    void calculateRotation()
    {
        if(transform.rotation.y - 90f < 0.2)
        {
            orientation = Orientation.Forward;
            Debug.Log("Forward");
        }
        if (transform.rotation.y + 90f < 0.2)
        {
            orientation = Orientation.Backward;
        }
        if (transform.rotation.y - 0 < 0.2)
        {
            orientation = Orientation.Left;
        }
        if (transform.rotation.y - 180f < 0.2)
        {
            orientation = Orientation.Right;
        }
    }



    public bool CheckVisibilty() {

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        // but most importantly he sees
        // Debug.Log(viewDistance);
        Vector3 direction = player.transform.position - transform.position;
        distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
        //Debug.Log(Vector3.Distance(player.transform.position, transform.position));
        if (distanceToPlayer <= viewDistance+2 && Vector3.Angle(direction, this.transform.forward) <= viewField) // 
        {
            playerLastPosition = player.transform.position;
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
