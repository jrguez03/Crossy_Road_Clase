using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinBehaviour : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI c_CoinText;

    public int c_CoinCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        c_CoinCount = PlayerPrefs.GetInt("Coin", 0);

        UpdateCoinText();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerPrefs.SetInt("Coins", c_CoinCount);
        PlayerPrefs.Save();

        UpdateCoinText();
    }

    private void UpdateCoinText()
    {
        c_CoinText.text = "Coins: " + c_CoinCount.ToString();
    }
}
