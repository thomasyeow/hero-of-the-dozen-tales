using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    public GameObject target;
    public bool isWalking = false;


    private Animator _animator;
    private NavMeshAgent _navComponent;
    private Vector3 startPosition;

    private int stance = 0; //0=standing, 1=walking, 2=attacking
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        _animator = gameObject.GetComponent<Animator>();
        _navComponent = gameObject.GetComponent<NavMeshAgent>();
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Move();

        _animator.SetFloat("Velocity", _navComponent.velocity.magnitude);
    }
    void Move()
    {
        if (stance == 1)//Move towards target
        {
            _navComponent.SetDestination(target.transform.position);
        }
        else if (stance == 0)//Move back to start position

        {
            _navComponent.SetDestination(startPosition);
        }
        else if (stance == 2) //load battleScene

        {

        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            stance++;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            stance--;
        }
    }
}
