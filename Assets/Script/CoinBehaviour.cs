using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinBehaviour : MonoBehaviour
{
    public TerrainBehaviour c_TerrainBehaviour;

    [SerializeField] TextMeshProUGUI c_CoinText;
    [SerializeField] TextMeshProUGUI c_FinalCoin;
    [SerializeField] GameObject c_Player;

    public int c_CoinCount = 0;
    public float c_rotationspeed = 20f;

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
        MuereCoin();

        transform.Rotate(Vector3.forward * c_rotationspeed * Time.deltaTime);
    }

    private void UpdateCoinText()
    {
        c_CoinText.text = "Coins: " + c_CoinCount.ToString();
    }

    public void MuereCoin()
    {
        if (c_Player)
        {
            c_FinalCoin.text = "Coins: " + c_CoinCount.ToString();
        }
    }
}
