using TMPro;
using UnityEngine;

public class SecondSceneHandler : MonoBehaviour
{
    public TMP_Text factoryNameTMP;

    void Start()
    {
        if (factoryNameTMP != null)
        {
            factoryNameTMP.text = StartButtonHandler.sharedText;
        }
    }
}
