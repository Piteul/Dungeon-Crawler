using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// RayChecker is use to check with raycast what is all around the entity.
/// </summary>
public class RayChecker : MonoBehaviour {

    // Use this for initialization
    // testers for each direction 
    public bool wForward, wBackward, wLeft, wRight;
    // ray max distance, à noter que la valeur 4 atteint le milieu de la case suivante ce qui est parfait pour detecter les obstacles et les ennemis selon les config actuelles.
    public float rayDistance = 4f;

    LayerMask unwalkable;



    private void Start() {
        unwalkable = LayerMask.GetMask("Unwalkable");

    }


    /// <summary>
    /// Check if the entity can move or not
    /// </summary>
    public void CheckMovement(string direction) {
        wForward = wBackward = wLeft = wRight = true;
        //RaycastHit hitForward, hitLeft, hitRight, hitBackward;
        // slightly push rays positions so it doesn't overlap with wall when the player is in front of a wall.


        // test each ray collision with something
        switch (direction) {

            case "Forward":

                if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), rayDistance, unwalkable)) {
                    //Debug.Log("Can't go forward baby!");
                    wForward = false;
                }
                Debug.DrawRay(transform.position, this.transform.forward * rayDistance, Color.blue);
                break;

            case "Right":

                if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.right), rayDistance, unwalkable)) {
                    // Debug.Log("Can't go right baby!");
                    wRight = false;

                }
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.right) * rayDistance, Color.green);
                break;

            case "Left":

                if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.left), rayDistance, unwalkable)) {
                    // Debug.Log("Can't go left baby!");
                    wLeft = false;
                }
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.left) * rayDistance, Color.red);
                break;

            case "Backward":

                if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.back), rayDistance, unwalkable)) {
                    // Debug.Log("Can't go back baby!");
                    wBackward = false;
                }
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.back) * rayDistance, Color.cyan);
                break;

            default:
                Debug.Log("Direction Unknown");
                break;
        }

        //Draw Ray for debug
    }
}
