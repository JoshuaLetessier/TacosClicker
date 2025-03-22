using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FactoryManager : MonoBehaviour
{
    [SerializeField] GameObject RawMaterialPrice;
    [SerializeField] GameManager gameManager;
    [SerializeField] GameObject TacosAllTime;
    [SerializeField] GameObject RawMaterialCount;
    [SerializeField] GameObject TacosPerSecond;
    [SerializeField] GameObject NBAutoCliker;

    [SerializeField] GameData gameData;

    public int tacosCountAllTime = 0;


    public int tacosPerSecond = 0;
    private int tacosPerClick = 1;

    public int countAutoClicker = 0;

    private bool isRunning = false;


    public int stockRessource = 0;
    public float stockRessourcePrice = 10;

    private TextMeshProUGUI textRawMaterialPrice;
    private TextMeshProUGUI textTacosAllTime;
    private TextMeshProUGUI textRawMaterialCount;
    private TextMeshProUGUI textTacosPerSecond;
    private TextMeshProUGUI textNBAutoCliker;


    private void Start()
    {
        tacosCountAllTime = gameData.tacosCountAllTime;
        countAutoClicker = gameData.countAutoClicker;
        stockRessource = gameData.stockRessource;
        stockRessourcePrice = gameData.stockRessourcePrice;

        textRawMaterialPrice = RawMaterialPrice.GetComponent<TextMeshProUGUI>();
        textTacosAllTime = TacosAllTime.GetComponent<TextMeshProUGUI>();
        textRawMaterialCount = RawMaterialCount.GetComponent<TextMeshProUGUI>();
        textTacosPerSecond = TacosPerSecond.GetComponent<TextMeshProUGUI>();
        textNBAutoCliker = NBAutoCliker.GetComponent<TextMeshProUGUI>();

        textRawMaterialPrice.text = stockRessourcePrice.ToString();
        textTacosAllTime.text = tacosCountAllTime.ToString();
        textRawMaterialCount.text = stockRessource.ToString();
        textTacosPerSecond.text = tacosPerSecond.ToString();
        textNBAutoCliker.text = countAutoClicker.ToString();

        StartCoroutine(RandomMaterialPriceCoroutine());
    }

    private void Update()
    {            
        // Update tacos per second
        tacosPerSecond = countAutoClicker;
        AddTacosPerAutoClicker();
        textTacosAllTime.text = tacosCountAllTime.ToString();
        textRawMaterialCount.text = stockRessource.ToString();
        textTacosPerSecond.text = tacosPerSecond.ToString();
        textNBAutoCliker.text = countAutoClicker.ToString();
    }

    // Add tacos per auto clicker
    public void AddTacosPerAutoClicker()
    {
        if (countAutoClicker > 0 && stockRessource > 0 && !isRunning)
        {
            StartCoroutine(AddTacosPerSecond());
            isRunning = true;
        }
    }

    // Coroutine to add tacos per second
    private IEnumerator AddTacosPerSecond()
    {
        while (countAutoClicker > 0 && stockRessource > 0)
        {
            if(stockRessource < countAutoClicker)
            {
                gameManager.tacosCount += stockRessource;
                tacosCountAllTime += stockRessource;
                stockRessource -= stockRessource;
            }
            gameManager.tacosCount += countAutoClicker;
            tacosCountAllTime += countAutoClicker;
            stockRessource -= countAutoClicker;

            yield return new WaitForSeconds(1);
        }

        isRunning = false;
    }

    // Add tacos per click
    public void AddTacosPerClick()
    {
        if (stockRessource > 0)
        {
            gameManager.tacosCount += tacosPerClick;
            tacosCountAllTime += tacosPerClick;
            stockRessource -= tacosPerClick;
        }
    }

    public void ByFactory()
    {
        if (gameManager.money >= 100)
        {
            gameManager.money -= 100;
            countAutoClicker++;
        }
    }

    public float RandomMaterialPrice()
    {
        return Random.Range(5, 20);
    }

    private IEnumerator RandomMaterialPriceCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(5);
            stockRessourcePrice = RandomMaterialPrice();
            textRawMaterialPrice.text = stockRessourcePrice.ToString();
        }
    }

    public void ByMaterial()
    {
        if (gameManager.money >= 10)
        {
            gameManager.money -= 10;
            stockRessource += 50;
        }
    }




}
