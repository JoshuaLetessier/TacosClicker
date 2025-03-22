using UnityEngine;
using UnityEngine.UIElements;

public class ObjectClickHandler : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private OfficeManager officeManager;
    [SerializeField] private FactoryManager factoryManager;
    [SerializeField] private SaveManager saveManager;

    [SerializeField] private GameData gameData;

    public UIDocument uiDocument; // Référence au document UI à afficher
    public AudioSource generalAudioSource; // Référence à la source audio générale
    public AudioSource musicAudioSource; // Référence à la source audio de la musique

    private VisualElement root;
    private Button closeButton;
    private Button restartButton;
    private Button quitButton;
    private SliderInt generalVolumeSlider;
    private SliderInt musicVolumeSlider;

    void Start()
    {
        if (uiDocument == null)
        {
            Debug.LogError("Le UIDocument n'a pas été assigné dans l'inspecteur.");
            return;
        }

        // Initialiser le document UI
        root = uiDocument.rootVisualElement;

        if (root == null)
        {
            Debug.LogError("Échec de l'initialisation du document UI. Assurez-vous que le UIDocument est correctement configuré.");
            return;
        }

        // Masquer l'UI au démarrage
        root.style.display = DisplayStyle.None;

        // Récupérer le bouton de fermeture
        closeButton = root.Q<Button>("CloseButton");
        restartButton = root.Q<Button>("RestartButton");
        quitButton = root.Q<Button>("QuitButton");

        if (closeButton != null)
        {
            // Ajouter un événement pour le clic sur le bouton de fermeture
            closeButton.clicked += OnCloseButtonClicked;
        }
        else
        {
            Debug.LogError("Bouton 'CloseButton' non trouvé dans l'UI !");
        }


        if (restartButton != null)
        {
            // Ajouter un événement pour le clic sur le bouton de fermeture
            restartButton.clicked += OnRestartButtonClicked;
        }
        else
        {
            Debug.LogError("Bouton 'RestartButton' non trouvé dans l'UI !");
        }

        if (quitButton != null)
        {
            quitButton.clicked += QuitGame;
        }

        else
        {
            Debug.LogError("Bouton 'QuitButton' non trouvé dans l'UI !");
        }

        // Récupérer les curseurs de volume
        generalVolumeSlider = root.Q<SliderInt>("GeneralVolume");
        musicVolumeSlider = root.Q<SliderInt>("MusicVolume");

        if (generalVolumeSlider != null)
        {
            // Ajouter un événement pour le changement de valeur du curseur de volume général
            generalVolumeSlider.RegisterValueChangedCallback(evt =>
            {
                float volume = evt.newValue / 100f;
                generalAudioSource.volume = volume;
            });
        }
        else
        {
            Debug.LogError("Curseur 'GeneralVolume' non trouvé dans l'UI !");
        }

        if (musicVolumeSlider != null)
        {
            // Ajouter un événement pour le changement de valeur du curseur de volume de la musique
            musicVolumeSlider.RegisterValueChangedCallback(evt =>
            {
                float volume = evt.newValue / 100f;
                musicAudioSource.volume = volume;
            });
        }
        else
        {
            Debug.LogError("Curseur 'MusicVolume' non trouvé dans l'UI !");
        }
    }

    void Update()
    {
        // Détecter le clic de souris gauche
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform == transform)
                {
                    root.style.display = DisplayStyle.Flex;
                }
            }
        }
    }

    void OnCloseButtonClicked()
    {
        root.style.display = DisplayStyle.None;
    }

    void OnRestartButtonClicked()
    {
        saveManager.DeleteSave();
        // Reload the scene and reset the game
        gameData.ResetData();
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }

    private void LoadScene(string sceneName)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }

    private void QuitGame()
    {
        PlayerPrefs playerPrefs = new PlayerPrefs
        {
            money = gameManager.money,
            tacosCount = gameManager.tacosCount,
            tacosPrice = officeManager._TacosPrice,
            marketLevel = officeManager._MarketLevel,
            marketPrice = officeManager._MarketPrice,
            stockRessourcePrice = factoryManager.stockRessourcePrice,
            stockRessource = factoryManager.stockRessource,
            countAutoClicker = factoryManager.countAutoClicker,
            tacosCountAllTime = factoryManager.tacosCountAllTime,
            demand = officeManager._DemandPercentage,
            time = System.DateTime.Now,
        };

        saveManager.SaveGame(playerPrefs);

        Debug.Log("Quitting the game");
        Application.Quit();
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
