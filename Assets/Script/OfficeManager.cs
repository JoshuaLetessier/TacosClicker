using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class OfficeManager : MonoBehaviour
{
    [SerializeField] GameData gameData;

    [SerializeField] GameManager gameManager;
    [SerializeField] GameObject TacosPrice;
    [SerializeField] GameObject MarketPrice;
    [SerializeField] GameObject MarketLevel;
    [SerializeField] GameObject Demande;
    [SerializeField] GameObject MoneyPerSeconds;

    private TextMeshProUGUI textTacosPrice;
    private TextMeshProUGUI textMarketPrice;
    private TextMeshProUGUI textMarketLevel;
    private TextMeshProUGUI textDemande;
    private TextMeshProUGUI textMoneyPerSeconds;

    private float tacosPrice = 10;
    private int marketLevel = 1;
    private float marketPrice = 200;
    private float demandPercentage;

    private bool isSelling = false;

    public float _TacosPrice { get => tacosPrice; set => tacosPrice = value; }
    public int _MarketLevel { get => marketLevel; set => marketLevel = value; }
    public float _MarketPrice{ get => marketPrice; set => marketPrice = value; }
    public float _DemandPercentage { get => demandPercentage; set => demandPercentage = value; }


    // Start is called before the first frame update
    void Start()
    {
        tacosPrice = gameData.tacosPrice;
        marketLevel = gameData.marketLevel;
        marketPrice = gameData.marketPrice;
        demandPercentage = gameData.demand;

        textTacosPrice = TacosPrice.GetComponent<TextMeshProUGUI>();
        textMarketPrice = MarketPrice.GetComponent<TextMeshProUGUI>();
        textMarketLevel = MarketLevel.GetComponent<TextMeshProUGUI>();
        textDemande = Demande.GetComponent<TextMeshProUGUI>();
        textMoneyPerSeconds = MoneyPerSeconds.GetComponent<TextMeshProUGUI>();

        textTacosPrice.text = tacosPrice.ToString() + " $";
        textMarketPrice.text = marketPrice.ToString() + " $";
        textMarketLevel.text = "Market level " + marketLevel.ToString();
        textDemande.text = MarketDemand().ToString() + " %";
        textMoneyPerSeconds.text = "0 $";

        if (!isSelling)
        {
            StartCoroutine(SellTacos());
            isSelling = true;
        }
    }

    void Update()
    {
        textDemande.text = MarketDemand().ToString() + " %";
    }


    //Price of tacos
    public void DecreasePriceTacos()
    {
        tacosPrice -= 1;
        textTacosPrice.text = tacosPrice.ToString() + " $";
    }

    public void IncreasePriceTacos()
    {
        tacosPrice += 1;
        textTacosPrice.text = tacosPrice.ToString() + " $";
    }

    // Buy Market
    public void BuyMarket()
    {
        if (gameManager.money >= marketPrice)
        {
            gameManager.money -= marketPrice;
            marketLevel++;
            marketPrice *= 2;
            textMarketPrice.text = marketPrice.ToString() + " $";
            textMarketLevel.text = "Market level      " + marketLevel.ToString();
        }
    }

    private IEnumerator SellTacos()
    {
        while (true)
        {
            if (gameManager.tacosCount > 0)
            {
                // Récupère le pourcentage de demande actuel
                demandPercentage = MarketDemand();

                // Calcule le nombre de tacos vendus par seconde (basé sur la demande)
                int tacosSold = Mathf.CeilToInt((demandPercentage / 100f) * (10 * marketLevel)); // 10 ventes max/s par niveau


                // Applique les ventes
                int actualSales = Mathf.Min(tacosSold, gameManager.tacosCount); // Ne pas dépasser le stock
                gameManager.tacosCount -= actualSales;
                gameManager.money += actualSales * tacosPrice;

                // Affiche les ventes
                textMoneyPerSeconds.text = (actualSales * tacosPrice).ToString() + " $";

                Debug.Log($"Vendu {actualSales} tacos | Prix: {tacosPrice:F2} | Argent: {gameManager.money:F2}");

                yield return new WaitForSeconds(1); // Attendre 1 seconde
            }
            else
            {
                Debug.Log("Plus de tacos à vendre !");
                yield return null; // Arrêter temporairement si stock vide
            }
        }
    }


    private float MarketDemand()
    {
        int population = 100000;
        float minPrice = 1;
        float maxPrice = 100;

        float safePrice = Math.Clamp(tacosPrice, minPrice, maxPrice);

        float levelMultiplier = (float)Math.Pow(1.1, marketLevel);
        float priceMultiplier = Mathf.Exp(-0.06933f * safePrice);
        float populationMultiplier = 1.01f * population;
        float rawDemand = levelMultiplier * priceMultiplier;

        float demand = Mathf.Min(rawDemand * population, population);
        float demandPercentage = (demand / population) * 100f;

        return demandPercentage;
    }


}
