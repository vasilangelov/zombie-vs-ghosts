using Unity.VisualScripting;

using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class GhostAI : MonoBehaviour
{
    public float m_wanderingRadius = 10f;
    public float m_wanderingSpeed = 1f;
    public float m_attackSpeed = 3f;
    public float m_attackRange = 10f;
    public float m_attackThreshold = 2f;
    public float m_distanceToTargetPointThreshold = 1f;
    public LayerMask m_playerLayer;
    public LayerMask m_groundLayer;

    private NavMeshAgent m_navMeshAgent;
    private GameObject m_player;
    private Vector3? m_targetPoint;
    private GameManager m_gameManager;

    private void Awake()
    {
        m_player = GameObject.FindGameObjectWithTag(Tags.Player);
        m_navMeshAgent = GetComponent<NavMeshAgent>();
        m_gameManager = GameObject.FindGameObjectWithTag(Tags.GameManager).GetComponent<GameManager>();
        m_navMeshAgent.speed = m_wanderingSpeed;
    }

    private void Update()
    {
        bool isPlayerInAttackRange = Physics.CheckSphere(transform.position, m_attackRange, m_playerLayer);

        if (isPlayerInAttackRange)
        {
            Attack();
        }
        else
        {
            Wander();
        }
    }

    private void Wander()
    {
        m_navMeshAgent.speed = m_wanderingSpeed;

        if (m_targetPoint == null)
        {
            float randomX = Random.Range(-m_wanderingRadius, m_wanderingRadius);
            float randomZ = Random.Range(-m_wanderingRadius, m_wanderingRadius);

            m_targetPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

            if (!Physics.Raycast(m_targetPoint.Value, -transform.up, 2f, m_groundLayer))
            {
                m_targetPoint = null;
            }
        }

        if (m_targetPoint.HasValue)
        {
            m_navMeshAgent.SetDestination(m_targetPoint.Value);

            Vector3 distanceToTargetPoint = transform.position - m_targetPoint.Value;

            if (distanceToTargetPoint.magnitude < m_distanceToTargetPointThreshold)
            {
                m_targetPoint = null;
            }
        }
    }

    private void Attack()
    {
        m_targetPoint = null;
        m_navMeshAgent.speed = m_attackSpeed;

        m_navMeshAgent.SetDestination(m_player.transform.position);

        Vector3 distance = transform.position - m_player.transform.position;

        if (distance.magnitude < m_attackThreshold)
        {
            m_gameManager.KillPlayer();
        }
    }
}
