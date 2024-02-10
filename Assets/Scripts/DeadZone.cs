using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DeadZone : MonoBehaviour
{
    public static UnityAction DeadZoneAction;
    public GameObject startPoint;
    public GameObject Player;
    public PlayerMove player;
    // Start is called before the first frame update
    void OnEnable()
    {
       player = Player.GetComponent<PlayerMove>(); 
        
    }

    // Update is called once per frame
    void Update()
    {
    
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            DeadZoneAction?.Invoke();
            
            Invoke("ResetPlayer", 2f);
            
        }
    }

    private void ResetPlayer()
    {
        Player.transform.position = startPoint.transform.position;
        player.AlivePlayer();

    }
}
