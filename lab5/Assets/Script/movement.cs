using UnityEngine.AI;
using UnityEngine;


public class movement : MonoBehaviour
{
    public NavMeshAgent agent;

    public Transform player;

    public LayerMask whatIsGround, whatIsPlayer;

    public float health =5;

    
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;


    public float timeBetweenAttacks;
    bool alreadyAttacked;

    private Animator anim;

    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;
    public GameObject drop;
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Awake()
    {
        player = GameObject.Find("Third Person Player").transform;
        agent = GetComponent<NavMeshAgent>();
        

    }

    private void Update()
    {

        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerInSightRange && !playerInAttackRange) Patroling();
        if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        if (playerInAttackRange && playerInSightRange) AttackPlayer();
    }

    private void Patroling()
    {
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
            agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        
        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;

        anim.SetBool("Aware", true);
    }
    private void SearchWalkPoint()
    {

        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);
        anim.SetBool("Aware", true);
        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            walkPointSet = true;
    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
        anim.SetBool("Aware", true);
    }

    private void AttackPlayer()
    {

        agent.SetDestination(transform.position);

        transform.LookAt(player);
        anim.SetBool("Attack", true);
        anim.SetBool("Aware", false);
        if (!alreadyAttacked)
        {

          

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }
    private void ResetAttack()
    {
        alreadyAttacked = false;
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            health--;
            Destroy(other.gameObject);
            if (health <= 0)
            {
                anim.SetTrigger("Death");
                Destroy(gameObject, 5);
            }
        }
    }

    private void OnDestroy()
    {
        Instantiate(drop, transform.position, drop.transform.rotation);
    }

}

