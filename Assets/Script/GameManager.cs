using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    [SerializeField] GameObject moneyText;
    [SerializeField] GameObject tacosCountText;

    [SerializeField] GameData gameData;

    public float money = 0;
    public int tacosCount = 0;

    private TextMeshProUGUI TextMeshProUGUI_Money;
    private TextMeshProUGUI TextMeshProUGUI_TacosCount;


    private float defaultTimeScale = 1f;


    public float speedTimeMultiplier = 1f;
    public bool resetSpeed = false;

    // Start is called before the first frame update
    void Start()
    {
        money = gameData.money;
        tacosCount = gameData.tacosCount;

        TextMeshProUGUI_Money = moneyText.GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI_TacosCount = tacosCountText.GetComponent<TextMeshProUGUI>();

        TextMeshProUGUI_Money.text = money.ToString() + " $";
        TextMeshProUGUI_TacosCount.text = tacosCount.ToString() + " Tacos";

        AutoIncrementMoney();
    }

    void Update()
    {
        TextMeshProUGUI_Money.text = money.ToString() + " $";
        if (Time.frameCount % 60 == 0)
            TextMeshProUGUI_TacosCount.text = tacosCount.ToString() + " Tacos";

        if (!resetSpeed)
            SetGameSpeed(speedTimeMultiplier);
        else
            ResetGameSpeed();
    }

    //Auto incrrement money every 10 seconds
    public void AutoIncrementMoney()
    {
        StartCoroutine(AutoIncrementMoneyCoroutine());
    }

    private IEnumerator AutoIncrementMoneyCoroutine()
    {
        while (true)
        {
            money += 1;
            yield return new WaitForSeconds(10);
        }
    }

    public void SetGameSpeed(float speedMultiplier)
    {
        Time.timeScale = Mathf.Clamp(speedMultiplier, 0.1f, 10f); // Limite la vitesse à des valeurs raisonnables
    }

    public void ResetGameSpeed()
    {
        Time.timeScale = defaultTimeScale;
    }


}
