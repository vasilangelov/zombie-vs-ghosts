using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenuManager : MonoBehaviour
{
    private void Awake()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void OnNewGameClick()
    {
        SceneManager.LoadScene(Scenes.Game);
        SceneManager.UnloadScene(Scenes.Finish);
    }
}
