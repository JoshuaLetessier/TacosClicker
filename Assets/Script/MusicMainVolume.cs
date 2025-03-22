using UnityEngine;
using UnityEngine.UIElements;

public class MusicMainVolume : MonoBehaviour
{
    public UIDocument uiDocument; // Référence au document UI à afficher
    public AudioSource musicAudioSource; // Référence à la source audio de la musique

    private VisualElement root;
    private Button closeButton;
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

        if (closeButton != null)
        {
            // Ajouter un événement pour le clic sur le bouton de fermeture
            closeButton.clicked += OnCloseButtonClicked;
        }
        else
        {
            Debug.LogError("Bouton 'CloseButton' non trouvé dans l'UI !");
        }

        musicVolumeSlider = root.Q<SliderInt>("MusicVolume");

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
}
