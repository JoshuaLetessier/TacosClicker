using UnityEngine;
using UnityEngine.UIElements;

public class MusicPlaylist : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip[] musicClips;
    private int currentClipIndex = 0;

    private Label musicTitleLabel;
    private Button nextButton;

    void OnEnable()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;

        // Trouve le Label pour afficher le titre de la musique
        musicTitleLabel = root.Q<Label>("MusicTitleLabel");
        if (musicTitleLabel == null)
        {
            Debug.LogError("MusicTitleLabel not found in the UI.");
            return;
        }

        // Trouve le bouton "Next"
        nextButton = root.Q<Button>("NextButton");
        if (nextButton == null)
        {
            Debug.LogError("NextButton not found in the UI.");
            return;
        }

        // Ajoute un gestionnaire d'événement pour le clic sur le bouton
        nextButton.clicked += OnNextButtonClicked;

        // Joue la première musique
        if (musicClips.Length > 0)
        {
            PlayMusic(currentClipIndex);
        }
    }

    void Update()
    {
        if (!audioSource.isPlaying && musicClips.Length > 0)
        {
            // Passer automatiquement au morceau suivant lorsque le morceau se termine
            PlayNextTrack();
        }
    }

    private void OnNextButtonClicked()
    {
        PlayNextTrack();
    }

    private void PlayNextTrack()
    {
        // Passe au morceau suivant
        currentClipIndex = (currentClipIndex + 1) % musicClips.Length;
        PlayMusic(currentClipIndex);
    }

    private void PlayMusic(int index)
    {
        if (index < 0 || index >= musicClips.Length)
            return;

        audioSource.clip = musicClips[index];
        audioSource.Play();

        if (musicTitleLabel != null)
        {
            string musicTitle = System.IO.Path.GetFileNameWithoutExtension(musicClips[index].name);
            musicTitleLabel.text = $"Playing: {musicTitle}";
        }
    }
}
