using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public float m_speed = 5f;
    public float m_jumpSpeed = 5f;
    public float m_rotateSpeed = 3f;
    public float m_groundCheckRadius = .2f;

    public Animator m_animator = null;
    public Rigidbody m_rigidbody = null;
    public Transform m_feetHitboxTransform = null;
    public LayerMask m_layerMask = default;

    private void Start()
    {
        if (m_animator == null) m_animator = GetComponent<Animator>();
        if (m_rigidbody == null) m_rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (GameManager.IsDead)
        {
            return;
        }

        float verticalAxis = Input.GetAxis(Controls.Vertical);
        Vector3 velocity = transform.forward * 100 * m_speed * verticalAxis * Time.deltaTime;
        velocity.y = m_rigidbody.velocity.y;

        float horizontalAxis = Input.GetAxis(Controls.Horizontal);
        transform.Rotate(new Vector3(0, horizontalAxis * 100, 0) * m_rotateSpeed * Time.deltaTime);

        if (Input.GetButtonDown(Controls.Jump) && IsGrounded)
        {
            velocity.y = m_jumpSpeed;
        }

        m_rigidbody.velocity = velocity;
        m_animator.SetFloat("MoveSpeed", (m_rigidbody.velocity.x + m_rigidbody.velocity.z) * 10);
    }

    private bool IsGrounded =>
        m_feetHitboxTransform != null
        && Physics.CheckSphere(m_feetHitboxTransform.position, m_groundCheckRadius, m_layerMask);
}
