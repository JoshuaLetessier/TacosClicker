using System.Collections;
using UnityEngine;

public class MovePanel : MonoBehaviour
{
    [SerializeField] private RectTransform panel; // Panel UI
    [SerializeField] private Vector2 targetPosition; // Position cible en UI local
    private Vector2 initialPosition; // Position initiale du panel

    private Coroutine currentCoroutine; // Pour éviter plusieurs coroutines en même temps

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

        // Vérifie si le panel est à la position initiale ou cible
        if (Mathf.Approximately(panel.anchoredPosition.x, initialPosition.x))
        {
            // Déplacer vers la position cible
            currentCoroutine = StartCoroutine(MovePanelCoroutine(targetPosition));
        }
        else
        {
            // Revenir à la position initiale
            currentCoroutine = StartCoroutine(MovePanelCoroutine(initialPosition));
        }
    }

    private IEnumerator MovePanelCoroutine(Vector2 target)
    {
        while (Mathf.Abs(panel.anchoredPosition.x - target.x) > 0.01f)
        {
            // Récupère la position actuelle
            Vector2 newPosition = panel.anchoredPosition;

            // Interpole uniquement sur X
            newPosition.x = Mathf.Lerp(newPosition.x, target.x, Time.deltaTime * 10f);

            // Applique la nouvelle position avec Y inchangé
            panel.anchoredPosition = newPosition;

            yield return null;
        }

        // S'assurer que le panel atteint exactement la position cible
        panel.anchoredPosition = new Vector2(target.x, panel.anchoredPosition.y);
    }

}
