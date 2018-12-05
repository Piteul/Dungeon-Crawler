using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class EnemyMovement : MonoBehaviour {

    EnemyVisibility enemVis;
    public bool isMove = true;
    private bool dejaVu = false;
    public bool inRangeforAttack = false;
    private int stepSize = 4;
    private bool boxesCalculated = false;
    public float fRotateIncrement = 4.5f;
    private RayChecker wallCheck;
    public int iRotateInterval = 20;
    private bool canAttack = false;
    private EnemyManager enemyManager;
    private GameObject sprite;
    void Start()
    {
        isMove = true;
        sprite = gameObject.transform.GetChild(0).gameObject;
        enemVis = GetComponent<EnemyVisibility>();
        enemyManager = GetComponent<EnemyManager>();
        wallCheck = gameObject.AddComponent<RayChecker>();
    }

    void Update()
    {
        sprite.transform.LookAt(GameObject.FindGameObjectWithTag("Player").transform);
        if (!canAttack)
        {
            Debug.Log("Roaming");
            StartCoroutine(Roam());

        }
        else
            enemyManager.Attack();


        // Ce code commenté est une propriété exclusive du dungeon ci-dessous et protégé par les loi des gilets jaunes
        // il ne peut dans aucun cas être touché.

            //Debug.Log(enemVis.orientation);
            //enemVis.playerOrientation();
            //RaycastHit hit;

            //Debug.Log("cv"+enemVis.CheckVisibilty());
            //inRangeforAttack = Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, enemVis.attackDistance, 1);


            /*if (enemVis.CheckVisibilty() && !dejaVu) //
            {

                //Debug.Log("i see you ");
                enemVis.playerOrientation();
                dejaVu = true;
                if (!inRangeforAttack && !boxesCalculated) //
                {

                    int boxes = countBoxes(); //- enemVis.attackDistanceByCase;
                    boxesCalculated = true;
                    StartCoroutine(move(boxes));   
                }
                else
                    Attack();
            }*/

    }

    IEnumerator Roam()
    {
        //Debug.Log(boxes);
        if (isMove)
        {
            isMove = false;
            int direction = (int) UnityEngine.Random.Range(0,4);
            switch (direction)
            {
                case ((int)Orientation.North):
                    wallCheck.CheckMovement("Forward");
                    if (!wallCheck.wForward) {
                        isMove = true;
                        break;
                    }
                    for (int g = 0; g < 20; g++)
                    {
                        transform.position += Vector3.right * 0.2f;
                        yield return new WaitForSeconds(0.01f);
                    }
                    isMove = true;
                    //Debug.Log("moving");
                    break;

                case ((int)Orientation.South):
                    wallCheck.CheckMovement("Backward");
                    if (!wallCheck.wBackward) {
                        isMove = true;
                        break;
                    }
                    for (int g = 0; g < 20; g++)
                    {
                        transform.position -= Vector3.right * 0.2f;
                        yield return new WaitForSeconds(0.01f);
                    }
                    isMove = true;
                    break;

                case ((int)Orientation.East):
                    wallCheck.CheckMovement("Left");
                    if (!wallCheck.wLeft) {
                        isMove = true;
                        break;
                    }
                    for (int g = 0; g < 20; g++)
                    {
                        transform.position += Vector3.forward * 0.2f;
                        yield return new WaitForSeconds(0.01f);
                    }
                    isMove = true;
                    break;

                case ((int)Orientation.West):
                    wallCheck.CheckMovement("Right");
                    if (!wallCheck.wRight) {
                        isMove = true;
                        break;
                    }
                    for (int g = 0; g < 20; g++)
                    {
                        transform.position -= Vector3.forward * 0.2f;
                        yield return new WaitForSeconds(0.01f);
                    }
                    isMove = true;
                    break;
            }

            yield return new WaitForSeconds(0.5f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            canAttack = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            canAttack = false;
        }
    }











    /// <summary>
    /// !                                               !!!! WARNING !!!!
    ///  Le code ci-dessous est dédié pour créer une IA bien meilleure et plus intelligente, les fonctions ne sont pas encore implémentés
    ///  tu risques dans un dangeon et je suis que tu pourra pas sortir!!, nah vas y check le code.
    /// </summary>
    /// <returns></returns>
    int countBoxes()
    {
        switch (enemVis.orientation)
        {
            // 4 est le step size
            case (Orientation.North):
                //Debug.Log("forward");
                return Mathf.Abs((int)(((int)transform.position.x - (int)enemVis.playerLastPosition.x) / stepSize));
            case (Orientation.South):
               // Debug.Log("backward");
                return Mathf.Abs((int)(((int)transform.position.x - (int)enemVis.playerLastPosition.x) / stepSize));
            case (Orientation.East):
                //Debug.Log("right");
                return Mathf.Abs((int)(((int)transform.position.z - (int)enemVis.playerLastPosition.z) / stepSize));
            case (Orientation.West):
                //Debug.Log("left");
                return Mathf.Abs((int)(((int)transform.position.z - (int)enemVis.playerLastPosition.z) / stepSize));

        }
        return -1;
    }



    IEnumerator move(int boxes)
    {
        //Debug.Log(boxes);
        int i = -1;
        for (i = 0; i < boxes; i++) {
            RaycastHit hit;
            inRangeforAttack = Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, enemVis.attackDistance, 1);
            if (inRangeforAttack)
                break;
            switch (enemVis.orientation)
            {
                case (Orientation.North):
                    for (int g = 0; g < 20; g++)
                    {
                        transform.position += Vector3.right * 0.2f;
                        yield return new WaitForSeconds(0.01f);
                    }
                    //Debug.Log("moving");
                    break;
                case (Orientation.South):
                    for (int g = 0; g < 20; g++)
                    {
                        transform.position -= Vector3.right * 0.2f;
                        yield return new WaitForSeconds(0.01f);
                    }
                    break;
                case (Orientation.East):
                    for (int g = 0; g < 20; g++)
                    {
                        transform.position += Vector3.forward * 0.2f;
                        yield return new WaitForSeconds(0.01f);
                    }
                    break;
                case (Orientation.West):
                    for (int g = 0; g < 20; g++)
                    {
                        transform.position -= Vector3.forward * 0.2f;
                        yield return new WaitForSeconds(0.01f);
                    }
                    break;
            }
            
            yield return new WaitForSeconds(0.5f);
        }
        //if (i == 0) i = -1;
        if(i == boxes) { 
            rotateEnemy();
            
        }
        isMove = false;
    }

    IEnumerator RotateRight()
    {
        Debug.Log("Rotating Right");
        for (int g = 0; g < iRotateInterval; g++)
        {
            //transform.position = new Vector3(Mathf.Round(transform.position.x), fYLockPosition, Mathf.Round(transform.position.z));
            transform.localEulerAngles += new Vector3(0, fRotateIncrement, 0);
            yield return new WaitForSeconds(0.02f);
        }
        yield return new WaitForSeconds(0.5f);
        // reset key boolean for recalculation, c'est ça le problème avec les coroutines en fait si tu les reset pas ici
        // ça executera en parallèle et ça sera le bordel, alors il faut faire gaffe
        enemVis.calculateRotation();
        dejaVu = false;
        boxesCalculated = false;
        //var wait = new WaitForSeconds(0.5f);

    }

    IEnumerator RotateLeft()
    {
        Debug.Log("Rotating Left");
        for (int g = 0; g < iRotateInterval; g++)
        {
            //transform.position = new Vector3(Mathf.Round(transform.position.x), fYLockPosition, Mathf.Round(transform.position.z));
            transform.localEulerAngles += new Vector3(0, -fRotateIncrement, 0);
            yield return new WaitForSeconds(0.02f);
        }
        yield return new WaitForSeconds(0.5f);
        // reset key boolean for recalculation, c'est ça le problème avec les coroutines en fait si tu les reset pas ici 
        // ça executera en parallèle et ça sera le bordel, alors il faut faire gaffe
        enemVis.calculateRotation();
        dejaVu = false;
        boxesCalculated = false;
        //var wait = new WaitForSeconds(0.5f);

    }

    private void rotateEnemy()
    {


        enemVis.playerOrientation();
        if (enemVis.plOrientation == playerLastOrientation.Left)
        {
            StartCoroutine(RotateLeft());
        }
        else if (enemVis.plOrientation == playerLastOrientation.Right)
        {
            StartCoroutine(RotateRight());
        }
    }

}