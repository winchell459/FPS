using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieScript : MonoBehaviour
{
    private bool isDead = false;
    private bool hasDied = false;
    private bool isAttacking = false;

    private Vector3 Destination;
    private float height;

    private Transform player;
    public CapsuleCollider ZombieCollider;

    public Vector2 ZombieRange = new Vector2(20,20);
    // Start is called before the first frame update
    void Start()
    {
        height = transform.position.y;
        Destination = transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        if (!player) player = GameObject.FindGameObjectWithTag("Player").transform;
        if (isDead && hasDied == false)
        {
            GetComponent<Animation>().Play("back_fall");

            GetComponent<NavMeshAgent>().SetDestination(transform.position);
            hasDied = true;
        }else if(!isDead && !isAttacking)
        {
            GetComponent<Animation>().Play("walk");
            if (Vector3.Distance(transform.position, Destination) < 1)
            {
                float randX = Random.Range(-ZombieRange.x, ZombieRange.x);
                float randZ = Random.Range(-ZombieRange.y, ZombieRange.y);
                Destination = new Vector3(randX, height, randZ);
                GetComponent<NavMeshAgent>().SetDestination(Destination);
            }
        }else if(isAttacking){
            GetComponent<Animation>().Play("attack");
            GetComponent<NavMeshAgent>().SetDestination(player.position);
        }
    }

    public void TakeDamage()
    {
        if (!isDead)
        {
            isDead = true;
            isAttacking = false;
            ZombieCollider.enabled = false;
            FindObjectOfType<LevelHandler>().ZombieKilled();
        }

    }

	private void OnTriggerStay(Collider other)
	{
        if(other.CompareTag("Player") && !isDead){
            isAttacking = true;
            player.GetComponent<PlayerHealth>().TakeDamage(0.1f);
        }
	}
	private void OnTriggerExit(Collider other)
	{
        if (other.CompareTag("Player") )
        {
            isAttacking = false;
        }
	}

}
