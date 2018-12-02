using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum playerLastOrientation { Front, Left,Right};
public enum Orientation { North, South,West,East};
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
    public Orientation orientation = Orientation.North; 
    public float distanceToPlayer = 0;
    public Vector3 playerLastPosition = Vector3.zero;

    private enum States { passive, offensive };
    private States state = States.passive;

    public float viewField = 45f;
    GameObject player;

    [HideInInspector]
    public playerLastOrientation plOrientation = playerLastOrientation.Right;


    void Start() {

        player = GameObject.FindGameObjectWithTag("Player");

        //Get the real view distance
        viewDistance = viewDistanceByCase * rayDistance;
        attackDistance = attackDistanceByCase * rayDistance;
        //rayCheck = GetComponent<RayChecker>();

    }

    public void calculateRotation()
    {
        
        float angle = transform.rotation.eulerAngles.y;
        angle %= 360;
        if (angle > 180)
            angle -= 360;
        Debug.Log("Result is " + angle);
        if (Mathf.Abs(angle - 90f) < 0.2)
        {
            orientation = Orientation.North;
            //Debug.Log("Forward");
        }
        if (Mathf.Abs(angle + 90f) < 0.2)
        {
            orientation = Orientation.South;
        }
        if (Mathf.Abs(angle - 0) < 0.2)
        {
            orientation = Orientation.East;
        }
        if (Mathf.Abs(angle - 180f) < 0.2)
        {
            orientation = Orientation.West;
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
            return true;

        }
        return false;
    }

    public void playerOrientation()
    {
        if (CheckVisibilty()) { 
            playerLastPosition = player.transform.position;
            Vector3 direction = player.transform.position - transform.position;
            float signedAngle = Vector3.SignedAngle(direction, this.transform.forward, Vector3.up);
            
            if (signedAngle > -20 && signedAngle < 20)
            {
                plOrientation = playerLastOrientation.Front;
                //Debug.Log("you're infront of me bitch");
                //return true;
            }

            if (signedAngle < -20)
            {
                plOrientation = playerLastOrientation.Right;
                //return true;
                //Debug.Log("you're on my right bitch");
            }
            if (signedAngle > 20)
            {
                plOrientation = playerLastOrientation.Left;
                //return true;
                //Debug.Log("you're on my left bitch");
            }
        }
    }

}
