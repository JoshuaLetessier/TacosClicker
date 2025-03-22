using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ActionButton : MonoBehaviour
{
    private string startSceneName = "StartScene"; // Nom de la scène de démarrage
    private string gameSceneName = "GameScene";   // Nom de la scène de jeu

    public void ResetGame()
    {
        string currentScene = SceneManager.GetActiveScene().name;

        if (currentScene == gameSceneName)
        {
            Debug.Log("Reset depuis la scène de jeu : retour à la scène de démarrage.");
            SceneManager.LoadScene(startSceneName); // Charger la scène de démarrage
        }
        else if (currentScene == startSceneName)
        {
            Debug.Log("Reset depuis la scène de démarrage.");
            // Réinitialiser des données si nécessaire
            SceneManager.LoadScene(startSceneName); // Recharge la scène de démarrage (optionnel)
        }
        else
        {
            Debug.LogWarning("Scène inconnue, aucune action.");
        }
    }

    public void OnQuit()
    {
        // Quit the game
        Application.Quit();
    }
}
