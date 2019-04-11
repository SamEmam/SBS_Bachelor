using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrencyEarnedDisplay : MonoBehaviour
{
    public Text currencyText;
    private int currency;
    private float timeBeforeCurrencyCount = 0.7f;
    private float t = 0.0f;
    private float lerpSpeed = 0.5f;

    private void OnEnable()
    {
        currency = PlayerPrefs.GetInt("Currency", 0);
        currencyText.text = currency.ToString();
    }

    private void Update()
    {
        if (timeBeforeCurrencyCount > 0)
        {
            timeBeforeCurrencyCount -= Time.deltaTime;
            return;
        }
        if (t < 1.0f)
        {
            t += lerpSpeed * Time.deltaTime;
            currencyText.text =  Mathf.Lerp(currency, PlayerPrefs.GetInt("Currency", 0), t).ToString("0.");
        }
    }
}
