using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zombieScript : MonoBehaviour {

    bool isDead = false;
    Animation zombieAnimations;
    Rigidbody rb;

    public float walkingSpeed = 30f;
    Vector3 direction;
    public float walkTime = 5f;
    float timeToChange = 0;

    float attackTime = 2f;
    float lastAttack = 0;

    //States
    public bool isWalking = false;
    public bool isAttacking = false;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        zombieAnimations = transform.GetChild(0).GetComponent<Animation>();
        direction = transform.GetChild(0).forward;
	}
	
	// Update is called once per frame
	void Update () {

        if (isWalking)
        {
            //transform.Translate(direction * walkingSpeed * Time.deltaTime);
            rb.velocity = direction * walkingSpeed * Time.deltaTime;
            if(timeToChange + walkTime < Time.fixedTime)
            {
                float newHorizontal = Random.Range(-1, 1);
                float newVertical = Random.Range(-1, 1);
                direction = new Vector3(newHorizontal,0,newVertical);
                timeToChange = Time.fixedTime;

                transform.forward = direction;
            }
            zombieAnimations.Play("walk");
        }

        if (isAttacking)
        {
            transform.forward = direction;
            zombieAnimations.Play("attack");
        }
	}

    public void MakeDamage()
    {
        if (!isDead)
        {
            isWalking = false;
            isAttacking = false;
            isDead = true;
            zombieAnimations.Play("back_fall");

            GameObject.FindGameObjectWithTag("Player").SendMessage("ZombieKill");
        }
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && !isDead)
        {
            isAttacking = true;
            isWalking = false;

            direction = other.transform.position - transform.position;
            direction = new Vector3(direction.x, 0, direction.z); 

            AttackPlayer(other.gameObject);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player" && !isDead)
        {
            isAttacking = false;
            isWalking = true;

            //lastAttack = 0;
        }
    }
    public void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player" && !isDead)
        {
            AttackPlayer(other.gameObject);
        }
    }

    void AttackPlayer(GameObject player)
    {
        if (lastAttack + attackTime < Time.fixedTime)
        {
            lastAttack = Time.fixedTime;
            player.SendMessage("ZombieAttack");
        }
    }
}
