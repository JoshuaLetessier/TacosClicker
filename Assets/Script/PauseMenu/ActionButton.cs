using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ActionButton : MonoBehaviour
{
    private string startSceneName = "StartScene"; // Nom de la sc�ne de d�marrage
    private string gameSceneName = "GameScene";   // Nom de la sc�ne de jeu

    public void ResetGame()
    {
        string currentScene = SceneManager.GetActiveScene().name;

        if (currentScene == gameSceneName)
        {
            Debug.Log("Reset depuis la sc�ne de jeu : retour � la sc�ne de d�marrage.");
            SceneManager.LoadScene(startSceneName); // Charger la sc�ne de d�marrage
        }
        else if (currentScene == startSceneName)
        {
            Debug.Log("Reset depuis la sc�ne de d�marrage.");
            // R�initialiser des donn�es si n�cessaire
            SceneManager.LoadScene(startSceneName); // Recharge la sc�ne de d�marrage (optionnel)
        }
        else
        {
            Debug.LogWarning("Sc�ne inconnue, aucune action.");
        }
    }

    public void OnQuit()
    {
        // Quit the game
        Application.Quit();
    }
}
