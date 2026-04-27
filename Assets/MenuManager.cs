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

    public void LoadCurrentDifficulty()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void LoadNextLevel()
    {
        if (GameSettings.CurrentLevel == GameSettings.Level.One)
        {
            GameSettings.CurrentLevel = GameSettings.Level.Two;
        }
        else if (GameSettings.CurrentLevel == GameSettings.Level.Two)
        {
            GameSettings.CurrentLevel = GameSettings.Level.Three;
        }
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
