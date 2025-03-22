using System.IO;
using UnityEngine;


[System.Serializable]

public class PlayerPrefs
{
    public float money;
    public int tacosCount;
    public float tacosPrice;
    public int marketLevel;
    public float marketPrice;
    public float stockRessourcePrice;
    public int stockRessource;
    public int countAutoClicker;
    public int tacosCountAllTime;
    public float demand;
    public string serializedTime;

    [System.NonSerialized]
    public System.DateTime time;

    // Getter et Setter pour synchroniser DateTime avec la chaîne sérialisée
    public System.DateTime Time
    {
        get
        {
            return string.IsNullOrEmpty(serializedTime) ? System.DateTime.MinValue : System.DateTime.Parse(serializedTime);
        }
        set
        {
            serializedTime = value.ToString("o"); // Format ISO 8601
        }
    }
}


public class SaveManager : MonoBehaviour
{
    private string saveFilePath;

    private void Start()
    {
        saveFilePath = Application.persistentDataPath + "/savefile.json";
        //saveFilePath = System.IO.Path.Combine(System.Environment.CurrentDirectory, "savefile.json");
        if (!File.Exists(saveFilePath))
        {
            File.Create(saveFilePath).Close(); // Crée un fichier vide
        }
    }

    public void SaveGame(PlayerPrefs data)
    {
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(saveFilePath, json);
        Debug.Log("Game saved to " + saveFilePath);
    }

    public PlayerPrefs LoadGame()
    {
        if (File.Exists(saveFilePath))
        {
            string json = File.ReadAllText(saveFilePath);
            PlayerPrefs data = JsonUtility.FromJson<PlayerPrefs>(json);
            Debug.Log("Game loaded");
            return data;
        }
        else
        {
            Debug.LogWarning("Save file not found");
            return null;
        }
    }

    public void DeleteSave()
    {
        File.Delete(saveFilePath);
        Debug.Log("Save deleted");
    }
}


