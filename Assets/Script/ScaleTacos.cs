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

        // Calcul de la nouvelle �chelle avec une fonction logarithmique
        float scaleFactor = Mathf.Log10(1 + factoryManager.tacosCountAllTime) / Mathf.Log10(2 + maxTacos);

        // Clamp la valeur entre 0.1 (taille minimale) et 3 (taille maximale)
        float newScale = Mathf.Lerp(0.1f, 3f, scaleFactor);

        // Applique l'�chelle au panel
        tacos.localScale = new Vector3(newScale, newScale, 1);
    }

    public void DisableTacos()
    {
        // V�rifie si un panel est actif dans la hi�rarchie
        if (FactoryPanel.activeInHierarchy || OfficePanel.activeInHierarchy)
        {
            if (tacos.gameObject.activeInHierarchy) // �vite de d�sactiver plusieurs fois
            {
                Debug.Log("D�sactivation des tacos");
                tacos.gameObject.SetActive(false);
            }
        }
        else if(!FactoryPanel.activeInHierarchy && !OfficePanel.activeInHierarchy)
        {
            if (!tacos.gameObject.activeInHierarchy) // �vite d'activer plusieurs fois
            {
                Debug.Log("R�activation des tacos");
                tacos.gameObject.SetActive(true);
            }
        }
    }
}
