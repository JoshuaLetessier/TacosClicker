using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewPlayerData", menuName = "GameData/PlayerData")]

public class GameData : ScriptableObject
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
    public System.DateTime time;

    public void ResetData()
    {
        money = 0;
        tacosCount = 0;
        tacosPrice = 10;
        marketLevel = 1;
        marketPrice = 200;
        stockRessourcePrice = 10;
        stockRessource = 50;
        countAutoClicker = 0;
        tacosCountAllTime = 0;
        demand = 50;
        time = System.DateTime.Now;
    }
}
