using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StoreDirector : MonoBehaviour
{
    public TextMeshProUGUI CoinCount;
    int coin = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CoinCount.text = coin.ToString();
    }

    public void OnTestCoinButton()
    {
        coin += 100;
    }
}
