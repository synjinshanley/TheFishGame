using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    
    public void StartGame()
    {
        SceneManager.LoadScene("DifficultyScreen");
    }

    public void Credits()
    {
        SceneManager.LoadScene("CreditsScreen");
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("TitleScreen");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
