
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class CoinText : MonoBehaviour
{
    
    //public GameObject textMeshObj;
    public TextMeshProUGUI coinTextRef;
    int coinCounter;

    private void OnEnable()
    {   
        
        CoinsHandler.OnCoinCollected += IncreaseCoinCount;
    }

    private void OnDisable()
    {
        CoinsHandler.OnCoinCollected -= IncreaseCoinCount;
    }
    // Start is called before the first frame update
    void Start()

    {
        //Text = FindObjectOfType<TextMeshProUGUI>();
        coinCounter = PlayerPrefs.GetInt("coins");
        coinTextRef.text = $"x{coinCounter}";
        
    }

    // Update is called once per frame
    void Update()
    {
        
        PlayerPrefs.SetInt("coins",coinCounter);
        
    }

    public void IncreaseCoinCount()
    {
        coinCounter++;
        coinTextRef.text = $"x{coinCounter}";
        Debug.Log("trigger");
    }
}
