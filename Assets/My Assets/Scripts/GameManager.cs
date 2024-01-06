using TMPro;

using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static bool IsDead { get; private set; }

    public sbyte m_initialLives = 3;
    public float m_lowestActivePoint = -30f;

    public GameObject m_player = null;
    public TMP_Text m_livesCounter = null;
    public GameObject m_gameOverPanel = null;

    public Vector3 m_startPosition = Vector3.zero;
    public Vector3 m_startForwardPosition = Vector3.zero;

    private sbyte m_lives = 0;

    private void Awake()
    {
        m_lives = m_initialLives;
        UpdateLives();
        ResetPosition();
    }

    private void Update()
    {
        if (m_player.transform.position.y >= m_lowestActivePoint)
        {
            return;
        }

        KillPlayer();
    }

    public void KillPlayer()
    {
        ResetPosition();

        if (m_lives > 1)
        {
            m_lives--;
        }
        else
        {
            IsDead = true;
            m_lives = 0;

            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;

            m_gameOverPanel.SetActive(true);
        }

        UpdateLives();
    }

    private void UpdateLives()
    {
        m_livesCounter.text = $"x{m_lives}";
    }

    private void ResetPosition()
    {
        m_player.transform.position = m_startPosition;
        m_player.transform.forward = m_startForwardPosition;
    }

    public void OnRespawnClick()
    {
        IsDead = false;

        ResetPosition();

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        m_gameOverPanel.SetActive(false);
        m_lives = m_initialLives;

        UpdateLives();
    }
}
