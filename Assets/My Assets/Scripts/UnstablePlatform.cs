using System;

using UnityEngine;

public class UnstablePlatform : MonoBehaviour
{
    public float m_colisionThreshold = 1f;
    public float m_stableDurationSeconds = 3;
    public float m_regenIntervalSeconds = 3;
    public LayerMask m_playerLayer;

    private Rigidbody m_rigidbody;
    private DateTime? m_stepTime;
    private DateTime? m_destructionTime;
    private Vector3 m_initialPosition;

    private void Awake()
    {
        m_initialPosition = transform.position;
    }

    private void Update()
    {
        if (m_stepTime.HasValue)
        {
            TimeSpan difference = DateTime.UtcNow - m_stepTime.Value;

            if (difference.Seconds >= m_stableDurationSeconds)
            {
                m_stepTime = null;
                m_destructionTime = DateTime.UtcNow;
                m_rigidbody = gameObject.AddComponent<Rigidbody>();
                m_rigidbody.useGravity = true;
            }

            return;
        }

        if (m_destructionTime.HasValue)
        {
            TimeSpan difference = DateTime.UtcNow - m_destructionTime.Value;

            if (difference.Seconds >= m_regenIntervalSeconds)
            {
                Destroy(m_rigidbody);
                m_rigidbody = null;
                transform.position = m_initialPosition;
                m_destructionTime = null;
            }

            return;
        }

        bool hasPlayerStepped = Physics.CheckSphere(transform.position, m_colisionThreshold, m_playerLayer);

        if (hasPlayerStepped)
        {
            m_stepTime = DateTime.UtcNow;
        }
    }
}
