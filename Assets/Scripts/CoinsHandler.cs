using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CoinsHandler : MonoBehaviour
{
    public GameObject smokeAnim;
    public AudioClip collectedAudio;
    public AudioSource coinAudio;

    List<GameObject> smokeList = new();

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
            GameObject smoke = Instantiate(smokeAnim);
            smoke.transform.position = other.transform.position;
            smokeList.Add(smoke);
            other.gameObject.SetActive(false); 
            Destroy(other.gameObject);
            OnCoinCollected?.Invoke();
            Invoke("DespawnSmoke", 1.5f);

            PlaySound();
            

        }
    }

    void DespawnSmoke()
    {
        if(smokeList.Count > 0)
        {
            GameObject temp = smokeList[0];
            smokeList.RemoveAt(0);
            Destroy(temp);
        }
        
    }

    void PlaySound()
    {
        coinAudio.clip = collectedAudio;
        coinAudio.Play();
    }

}
