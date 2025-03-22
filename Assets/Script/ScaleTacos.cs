using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleTacos : MonoBehaviour
{
    [SerializeField] private RectTransform tacos; // Panel UI
    [SerializeField] private FactoryManager factoryManager;
    [SerializeField] private GameObject FactoryPanel;
    [SerializeField] private GameObject OfficePanel;

    private void FixedUpdate()
    {
        // Si un panel est actif, on ne veut pas afficher les tacos
        DisableTacos();

        // Nombre de tacos pour atteindre la taille maximale
        float maxTacos = 100000f; // Ajuste selon tes besoins

        // Calcul de la nouvelle échelle avec une fonction logarithmique
        float scaleFactor = Mathf.Log10(1 + factoryManager.tacosCountAllTime) / Mathf.Log10(2 + maxTacos);

        // Clamp la valeur entre 0.1 (taille minimale) et 3 (taille maximale)
        float newScale = Mathf.Lerp(0.1f, 3f, scaleFactor);

        // Applique l'échelle au panel
        tacos.localScale = new Vector3(newScale, newScale, 1);
    }

    public void DisableTacos()
    {
        // Vérifie si un panel est actif dans la hiérarchie
        if (FactoryPanel.activeInHierarchy || OfficePanel.activeInHierarchy)
        {
            if (tacos.gameObject.activeInHierarchy) // Évite de désactiver plusieurs fois
            {
                Debug.Log("Désactivation des tacos");
                tacos.gameObject.SetActive(false);
            }
        }
        else if(!FactoryPanel.activeInHierarchy && !OfficePanel.activeInHierarchy)
        {
            if (!tacos.gameObject.activeInHierarchy) // Évite d'activer plusieurs fois
            {
                Debug.Log("Réactivation des tacos");
                tacos.gameObject.SetActive(true);
            }
        }
    }
}
