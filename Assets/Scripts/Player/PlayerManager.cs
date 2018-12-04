using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour {

    float sizeBox = 4;
    public float health = 100f;
    public bool coolDown = false;
    public float coolDownTime = 2f;
    private float time;
    Animator animator;
    public Slider CoolDownSlider;
    PlayerMovement PlayerMovement;
    GameObject weaponStash;
    [HideInInspector]
    public Weapon weapon;
    Toolbox toolbox;

    //UI
    public Slider healthBar;

    // Use this for initialization
    void Start() {
        animator = GetComponent<Animator>();
        PlayerMovement = GetComponent<PlayerMovement>();
        //weapon = GetComponentInChildren<Weapon>();
        toolbox = new Toolbox();
        weaponStash = transform.Find("Weapon").gameObject;

        
        Debug.Log(coolDownTime);

    }

    // Update is called once per frame
    void Update() {
        PlayerMovement.playerMovement();
        Attack();

    }

    void GetActiveWeapon()
    {
        // we get the current weapon and its parameters to associate them with the player.
        for (int i = 0; i < weaponStash.transform.childCount; i++)
        {
            if (weaponStash.transform.GetChild(i).gameObject.activeSelf == true)
            {
                weapon = weaponStash.transform.GetChild(i).GetComponent<Weapon>();

            }
        }
        coolDownTime = weapon.weaponCoolDown;
    }

    /// <summary>
    /// Manage the player attack
    /// </summary>
    public void Attack() {

        // search the current active weapon
        GetActiveWeapon();

        // 80 est la taille par défaut de notre slider, 2 est le coolDown par défaut.
        CoolDownSlider.GetComponent<RectTransform>().sizeDelta= new Vector2(80 * coolDownTime / 2, 20);
        RaycastHit hit;
        int dmg;
        // here the player is in cooldown mode and can't attack
        if (coolDown) {
            CoolDownSlider.gameObject.SetActive(true);
            // this is to count time
            time -= Time.deltaTime;
            Debug.Log(time);
            CoolDownSlider.value = time * CoolDownSlider.maxValue / coolDownTime;

            if (time < 0) {
                coolDown = false;
                time = coolDownTime;
                CoolDownSlider.gameObject.SetActive(false);
            }
        }
        else {
            if (Input.GetMouseButtonDown(0)) {

                time = coolDownTime;
                coolDown = true;
                animator.SetTrigger("Attack");


                if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, (sizeBox * weapon.attackDistance))) {
                    if (hit.collider.tag == "Enemy") {
                        dmg = toolbox.randomValue(weapon.damage, 3);
                        EnemyManager enemy = hit.collider.gameObject.GetComponent<EnemyManager>();
                        enemy.TakeDamage(dmg);
                    }
                    Debug.DrawRay(transform.position, this.transform.forward * (sizeBox * weapon.attackDistance), Color.black);
                }
            }
        }
    }

    /// <summary>
    /// Control how many damage the player take
    /// </summary>
    /// <param name="damage"></param>
    public void TakeDamage(int damage) {

        health -= damage;
        UIGestion();
        Debug.Log(health);

    }

    public void UIGestion() {
        healthBar.value = health;
        healthBar.transform.Find("HealthText").GetComponent<Text>().text = health.ToString();
    }



}
