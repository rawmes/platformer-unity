using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CoinsHandler : MonoBehaviour
{  

    public int coins;
    public static event Action OnCoinCollected;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Coin"))
        {
            coins = coins+1;
            //write a better way to kill sprite here.. deadline so no time for looking up 2d fade anim
            other.gameObject.SetActive(false); 
            Destroy(other.gameObject);
            OnCoinCollected?.Invoke();

        }
    }
}
