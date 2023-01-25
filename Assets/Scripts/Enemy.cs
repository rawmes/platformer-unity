using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Enemy : MonoBehaviour
{

   
    
    public int maxHealth = 100;
    int currentHealth;
    private OssilateBetweenTwoEmpties killComp;
    private Animator animator;

    

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();

    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;


        if (currentHealth <= 0)
        {
            
            Die();

        }
    }

    void Die()
    {
        Debug.Log("will die here");
        killComp= GetComponent<OssilateBetweenTwoEmpties>();
        killComp.enabled = false;
        animator.SetBool("isDead", true);
        
        //gameObject.SetActive(false);
        Destroy(gameObject,1f);
    }
}
