using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerManager : MonoBehaviour {

    float sizeBox = 4;
    public float health = 100f;
    public bool coolDown = false;
    public float coolDownTime = 2f;
    private float time ;
    Animator animator;
    public Slider CoolDownSlider;
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
        time = coolDownTime;
    }

    // Update is called once per frame
    void Update() {
        PlayerMovement.playerMovement();
        Attack();

    }

    /// <summary>
    /// Manage the player attack
    /// </summary>
    public void Attack() {

        
        RaycastHit hit;
        int dmg;
        // here the player is in cooldown mode and can't attack
        if (coolDown)
        {
            CoolDownSlider.gameObject.SetActive(true);
            // this is to count time
            time -= Time.deltaTime;
             CoolDownSlider.value = time * CoolDownSlider.maxValue / coolDownTime;
            
            if (time < 0)
            {
                coolDown = false;
                time = coolDownTime;
                CoolDownSlider.gameObject.SetActive(false);
            }
        }
        else
        {
            if (Input.GetMouseButtonDown(0)) {

                time = coolDownTime;
                coolDown = true;
                animator.SetTrigger("Attack");


                if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, (sizeBox * weapon.attackDistance))) {
                    if (hit.collider.tag == "Enemy") {
                        dmg = toolbox.randomDamage(weapon.damage, 3);
                        EnemyManager enemy = hit.collider.gameObject.GetComponent<EnemyManager>();
                        enemy.takeDamage(dmg);
                    }
                }
                Debug.DrawRay(transform.position, this.transform.forward * (sizeBox * weapon.attackDistance), Color.black);
                StartCoroutine(coolDownTimer(coolDownTime));
            }
        }
    }

    /// <summary>
    /// Control how many damage the player take
    /// </summary>
    /// <param name="damage"></param>
    public void takeDamage(int damage) {

        health -= damage;
        Debug.Log(health);

    }

    IEnumerator coolDownTimer(float time)
    {
        yield return new WaitForSeconds(time);
        coolDown = false;
    }

}
