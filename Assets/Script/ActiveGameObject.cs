using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveGameObject : MonoBehaviour
{
    [SerializeField] private GameObject activableObject;
    [SerializeField] private List<GameObject> unactiveObject;
    [SerializeField] private AudioSource assignedAudioSource;
    [SerializeField] private AudioClip activationSound;

    public void activeObject()
    {
        activableObject.SetActive(!activableObject.activeSelf);

        unactiveObject.ForEach(obj => obj.SetActive(false));

        if (assignedAudioSource != null && activationSound != null)
        {
            assignedAudioSource.PlayOneShot(activationSound);
        }
    }
}
