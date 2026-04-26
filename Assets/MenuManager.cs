using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    
    public void StartGame()
    {
        SceneManager.LoadScene("DifficultyScreen");
    }

    public void LoadEasy()
    {
        GameSettings.CurrentDifficulty = GameSettings.Difficulty.Easy;
        SceneManager.LoadScene("MainScene");
    }

    public void LoadMedium()
    {
        GameSettings.CurrentDifficulty = GameSettings.Difficulty.Medium;
        SceneManager.LoadScene("MainScene");
    }

    public void LoadHard()
    {
        GameSettings.CurrentDifficulty = GameSettings.Difficulty.Hard;
        SceneManager.LoadScene("MainScene");
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
