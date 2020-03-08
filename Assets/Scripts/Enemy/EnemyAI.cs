using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    
    [SerializeField]private NavMeshAgent agent;
    [SerializeField]

    private Transform player;
    private Vector3 playerPosition;
    private PlayerController playerC;
    private bool isAlive;
    private bool isAttacking;

    public void InitAI()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
            playerC = player.gameObject.GetComponent<PlayerController>();
            playerPosition = new Vector3(player.position.x, 0f, player.position.z);
        }

        isAlive = true;
        isAttacking = false;
        
        agent.destination = playerPosition;
        agent.isStopped = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!isAttacking && agent.hasPath && !agent.isStopped && agent.remainingDistance <= 1)
        {
            agent.isStopped = true;

            //Attack
            StartCoroutine(Attack());
        }
        else if (!agent.hasPath)
        {
            InitAI();
        }
    }

    IEnumerator Attack()
    {
        while (isAlive)
        {
            yield return new WaitForSeconds(1f);

            playerC.SetDamage(Enemy.DAMAGE);
        }

        isAttacking = false;
    }

    public void Dead()
    {
        StopAllCoroutines();
    }
}
