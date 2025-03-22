using System;
using UnityEngine;
using UnityEngine.UIElements;

public class StartButtonHandler : MonoBehaviour
{

    [SerializeField] private GameData gameData;
    [SerializeField] private SaveManager saveManager;

    private Button startButton;
    private Button optionsButton;
    private Button quitButton;
    private TextField inputField;

    public UIDocument optionsMenu; // Document UI pour le menu des options
    private VisualElement optionsRoot;
    private Button closeButton; // Bouton pour fermer le menu d'options

    // Variable statique pour partager le texte entre les scènes
    public static string sharedText = "";

    void OnEnable()
    {
        // Récupérer les éléments principaux
        var root = GetComponent<UIDocument>().rootVisualElement;

        startButton = root.Q<Button>("StartButton");
        optionsButton = root.Q<Button>("OptionsButton");
        quitButton = root.Q<Button>("QuitButton");
        inputField = root.Q<TextField>("InputField");

        // Initialiser le menu d'options
        InitializeOptionsMenu();

        // Bouton "Start"
        if (startButton != null)
        {
            startButton.clicked += () =>
            {
                if (inputField != null)
                {
                    sharedText = inputField.value;
                }
                LoadSave();
                LoadScene("Game");
            };
        }

        // Bouton "Options"
        if (optionsButton != null)
        {
            optionsButton.clicked += ShowOptionsMenu;
        }

        // Bouton "Quit"
        if (quitButton != null)
        {
            quitButton.clicked += QuitGame;
        }
    }

    void OnDisable()
    {
        if (startButton != null)
            startButton.clicked -= () => LoadScene("Josh");

        if (optionsButton != null)
            optionsButton.clicked -= ShowOptionsMenu;

        if (quitButton != null)
            quitButton.clicked -= QuitGame;

        if (closeButton != null)
            closeButton.clicked -= HideOptionsMenu;
    }

    private void InitializeOptionsMenu()
    {
        if (optionsMenu != null)
        {
            optionsRoot = optionsMenu.rootVisualElement;
            optionsRoot.style.display = DisplayStyle.None; // Masquer le menu au départ

            closeButton = optionsRoot.Q<Button>("CloseButton");
            if (closeButton != null)
            {
                closeButton.clicked += HideOptionsMenu;
            }
        }
        else
        {
            Debug.LogError("Menu d'options non assigné dans l'inspecteur !");
        }
    }

    private void ShowOptionsMenu()
    {
        if (optionsRoot != null)
        {
            optionsRoot.style.display = DisplayStyle.Flex; // Afficher le menu
        }
    }

    private void HideOptionsMenu()
    {
        if (optionsRoot != null)
        {
            optionsRoot.style.display = DisplayStyle.None; // Masquer le menu
        }
    }

    private void LoadScene(string sceneName)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }

    private void QuitGame()
    {
        Debug.Log("Quitting the game");
        Application.Quit();
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }

    //Load save
    public void LoadSave()
    {
        PlayerPrefs playerPrefs = saveManager.LoadGame();
        if (playerPrefs != null)
        {
            gameData.money = playerPrefs.money;
            gameData.tacosCount = playerPrefs.tacosCount;
            gameData.tacosPrice = playerPrefs.tacosPrice;
            gameData.marketLevel = playerPrefs.marketLevel;
            gameData.marketPrice = playerPrefs.marketPrice;
            gameData.stockRessourcePrice = playerPrefs.stockRessourcePrice;
            gameData.stockRessource = playerPrefs.stockRessource;
            gameData.countAutoClicker = playerPrefs.countAutoClicker;
            gameData.tacosCountAllTime = playerPrefs.tacosCountAllTime;
            gameData.demand = playerPrefs.demand;
            gameData.time = playerPrefs.time;
            UpdateDataByTime();
        }
    }

    private void UpdateDataByTime()
    {
        int tacosSoldTotal = 0;
        double moneyEarned = 0;

        // Calcul du temps écoulé depuis la dernière sauvegarde
        TimeSpan timeSpan = DateTime.Now - gameData.time;
        int secondsElapsed = Mathf.Min((int)timeSpan.TotalSeconds, 86400); // Limite à 24 heures

        Debug.Log($"Temps écoulé : {secondsElapsed} secondes");

        // Initialisation des variables
        int tacosProducedPerSecond = gameData.countAutoClicker;
        float demandMultiplier = gameData.demand / 100f;
        int tacosSoldPerSecond = Mathf.CeilToInt(demandMultiplier * (10 * gameData.marketLevel));
        tacosSoldPerSecond = Mathf.Clamp(tacosSoldPerSecond, 0, 100); // Limite max de tacos vendus/s

        // Itérer seconde par seconde
        for (int i = 0; i < secondsElapsed; i++)
        {
            // Production de tacos
            int tacosProduced = Mathf.Min(tacosProducedPerSecond, gameData.stockRessource);
            gameData.stockRessource -= tacosProduced;
            gameData.tacosCount += tacosProduced;
            gameData.tacosCountAllTime += tacosProduced;

            // Vente de tacos
            int tacosSold = Mathf.Min(tacosSoldPerSecond, gameData.tacosCount);
            gameData.tacosCount -= tacosSold;
            tacosSoldTotal += tacosSold;

            // Calcul de l'argent gagné
            moneyEarned += tacosSold * gameData.tacosPrice;

            // Si plus de ressources, arrêter la production
            if (gameData.stockRessource <= 0 && gameData.tacosCount <= 0)
            {
                Debug.Log("Production et vente arrêtées, plus de ressources ou de tacos.");
                break;
            }
        }

        // Mise à jour des données finales
        gameData.money += (float)moneyEarned;

        // Debug pour validation
        Debug.Log($"Tacos produits totaux : {gameData.tacosCountAllTime} | Tacos vendus totaux : {tacosSoldTotal} | Argent gagné : {moneyEarned:F2} | Stock restant : {gameData.stockRessource}");
    }

}
