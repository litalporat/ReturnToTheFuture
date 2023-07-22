using UnityEngine;
using UnityEngine.SceneManagement;

public class SequencerScene : MonoBehaviour
{
    public void Reload()
    {
        SceneManager.LoadScene(1);
        Time.timeScale = 1;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
