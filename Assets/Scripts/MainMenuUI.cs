using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    public string gameSceneName = "Game"; // Byt till namnet på er spelscen

    // Metod som startar spelet när "Let's Play"-knappen trycks
    public void PlayGame()
    {
        SceneManager.LoadScene(gameSceneName);
    }
}
