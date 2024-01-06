using UnityEngine;
using UnityEngine.SceneManagement;

public class Finish : MonoBehaviour
{
    public float m_colisionThreshold = 1f;
    public LayerMask m_checkLayer;

    private Transform m_playerFeet;

    private void Awake()
    {
        m_playerFeet = GameObject.FindGameObjectWithTag(Tags.PlayerFeet).transform;
    }

    private void Update()
    {
        bool playerStepped = Physics.CheckSphere(m_playerFeet.position, m_colisionThreshold, m_checkLayer);

        if (playerStepped)
        {
            SceneManager.LoadScene(Scenes.Finish);
            SceneManager.UnloadScene(Scenes.Game);
        }
    }
}
