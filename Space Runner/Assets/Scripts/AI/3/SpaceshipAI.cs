using UnityEngine.AI;
using UnityEngine;

public class SpaceshipAI : MonoBehaviour
{
    //public NavMeshAgent agent;
    float rotatinalDamp = 0.5f;

    public Transform player;
    public LayerMask whatIsPlayer;

    //Flying around
    public Vector3 flyPoint;
    bool flyPointSet;
    public float flyPointRange;
    public float moveSpeed;

    //Attacking 
    public float timeBetweenAttacks;
    bool alreadyAttacked;
    public GameObject projectile;

    //States
    public float sightRange, attackRange;
    public bool  playerInSightRange, playerInAttackRange;

    void Awake()
    {
        player = GameObject.Find("Player_Spaceship").transform;

        //agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerInSightRange && !playerInAttackRange) Patroling();
        if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        if (playerInSightRange && playerInAttackRange) AttackPlayer();

    }

    private void Patroling()
    {
        if (flyPointSet == false)
        {
            SearchFlyPoint();
        }
        if(flyPointSet == true)
        {
            //agent.SetDestination(flyPoint);
            transform.position = Vector3.MoveTowards(transform.position, flyPoint, moveSpeed * Time.deltaTime);
            Vector3 pos = flyPoint - transform.position;
            Quaternion rotation = Quaternion.LookRotation(pos);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotatinalDamp * Time.deltaTime);
        }

        Vector3 distanceToFlyPoint = transform.position - flyPoint;

        //Walkpoint reached
        if(distanceToFlyPoint.magnitude <1f)
        {
            flyPointSet = false;
        }
    }
    private void SearchFlyPoint()
    {
        float randomZ = Random.Range(-flyPointRange, flyPointRange);
        float randomX = Random.Range(-flyPointRange, flyPointRange);
        float randomY = Random.Range(-flyPointRange, flyPointRange);

        
        flyPoint = new Vector3(transform.position.x + randomX, transform.position.y + randomY, transform.position.z + randomZ);

        flyPointSet = true;
    }

    private void ChasePlayer()
    {
        //agent.SetDestination(player.position);
        transform.position = Vector3.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
        Vector3 pos = player.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(pos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotatinalDamp*2 * Time.deltaTime);
    }
    private void AttackPlayer()
    {
        //agent.SetDestination(transform.position);
        transform.position = Vector3.MoveTowards(transform.position, transform.position, moveSpeed * Time.deltaTime);
        Vector3 pos = player.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(pos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotatinalDamp*5 * Time.deltaTime);

        if (!alreadyAttacked)
        {
            //Attack code here
            Debug.Log("Shooting sheet");
            Rigidbody rb = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * 100f, ForceMode.Impulse);
            rb.AddForce(transform.up * 2f, ForceMode.Impulse);
            //

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }
    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}
