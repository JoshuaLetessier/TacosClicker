using System.Collections;
using UnityEngine;

public class MovePanel : MonoBehaviour
{
    [SerializeField] private RectTransform panel; // Panel UI
    [SerializeField] private Vector2 targetPosition; // Position cible en UI local
    private Vector2 initialPosition; // Position initiale du panel

    private Coroutine currentCoroutine; // Pour �viter plusieurs coroutines en m�me temps

    void Start()
    {
        // Sauvegarde de la position initiale en local
        initialPosition = panel.anchoredPosition;
    }

    public void Move()
    {
        // Annuler la coroutine en cours si elle existe
        if (currentCoroutine != null)
        {
            StopCoroutine(currentCoroutine);
        }

        // V�rifie si le panel est � la position initiale ou cible
        if (Mathf.Approximately(panel.anchoredPosition.x, initialPosition.x))
        {
            // D�placer vers la position cible
            currentCoroutine = StartCoroutine(MovePanelCoroutine(targetPosition));
        }
        else
        {
            // Revenir � la position initiale
            currentCoroutine = StartCoroutine(MovePanelCoroutine(initialPosition));
        }
    }

    private IEnumerator MovePanelCoroutine(Vector2 target)
    {
        while (Mathf.Abs(panel.anchoredPosition.x - target.x) > 0.01f)
        {
            // R�cup�re la position actuelle
            Vector2 newPosition = panel.anchoredPosition;

            // Interpole uniquement sur X
            newPosition.x = Mathf.Lerp(newPosition.x, target.x, Time.deltaTime * 10f);

            // Applique la nouvelle position avec Y inchang�
            panel.anchoredPosition = newPosition;

            yield return null;
        }

        // S'assurer que le panel atteint exactement la position cible
        panel.anchoredPosition = new Vector2(target.x, panel.anchoredPosition.y);
    }

}
