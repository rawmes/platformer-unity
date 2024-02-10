
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    public GameObject walkingSound;
    public AudioSource jumpSound;
    public AudioSource deathSound;
    

    Vector3 jumpStop = new Vector3 (1f, 0f, 1f);

   

    public InputAction playerControl;
    

    public Transform attackPoint;
    public float attackRange= 0.64f;
    public int attackDamage = 100;

    public LayerMask enemyLayers;



    public float speed;


    public float jumpForce;

    public bool isJumping=false;
    public int numberOfJumps;
    private int jumpCounter;
    
    public float momentum;

    private Rigidbody2D rb;

    public static event Action OnCrouch;

    private float inputX;

    private Vector3 refVel = Vector3.zero;

    public Animator animator;

    private bool isFacingRight;
    public bool canMove=true;
    private float dampFactor;
    
    private void OnEnable()
    {
        playerControl.Enable();
        DeadZone.DeadZoneAction += KillPlayer;
        

    }

    private void OnDisable()
    {
        playerControl.Disable();
        DeadZone.DeadZoneAction -= KillPlayer;
    
    }

    // Start is called before the first frame update
    void Start()
    {
        jumpCounter = numberOfJumps;

        rb = gameObject.GetComponent<Rigidbody2D>();
        
        //DontDestroyOnLoad(animator);
            
    }

    // Update is called once per frame
    void Update()
    {
        
        
        //moving the player
        // Move = Input.GetAxis("Horizontal");
        
        
        Vector3 newVel = new Vector2(speed*inputX, rb.velocity.y);
        
        if(isJumping)
        {
             dampFactor = momentum * 7;
        }
        else
        {
             dampFactor = momentum;
        }



        if (canMove)
        {
            rb.velocity = Vector3.SmoothDamp(rb.velocity, newVel, ref refVel, dampFactor);
            if(newVel.x != 0 && !isJumping)
            {
                walkingSound.SetActive(true);
            }
            else
            {
                walkingSound.SetActive(false);
            }
            
        }
        else
        {
            rb.velocity = Vector2.zero;
           
        }
       
       // Debug.Log($"{rb.velocity}");

        
        if(Input.GetButtonDown("Fire1")){
            OnCrouch?.Invoke();
        }
    }
    
    public void Move(InputAction.CallbackContext context)
    {

        
           

        inputX = context.ReadValue<Vector2>().x;
        
        
        
             
        if(inputX > 0)
        {
            
            animator.SetBool("isRunning",true);

            isFacingRight = true;
            transform.localRotation = Quaternion.Euler(0,0,0);
            
        }else if(inputX < 0){

            
            animator.SetBool("isRunning",true);


            isFacingRight = false;
            transform.localRotation = Quaternion.Euler(0,180,0);
            

            
            
        }else{
            
            animator.SetBool("isRunning",false);
            
        }
        //Debug.Log("moving");
    }

    public void jump(InputAction.CallbackContext context)
    {
        if (!canMove) return;

        if (context.started )
        {
            animator.SetBool("isJumping", true);
            if (jumpCounter >0 || !isJumping)
            {
                rb.velocity *= jumpStop;

                rb.velocity = rb.velocity + Vector2.up * jumpForce;

                
                jumpSound.Play();
                
                if(isJumping)
                {
                    jumpCounter--;
                }
                
                ////Debug.Log($"{rb.velocity.y}");
                //if(rb.velocity.x < 0f && transform.parent)
                //{
                //    Debug.Log(transform.parent);
                //    rb.velocity = new Vector2(rb.velocity.x,jumpForce*0.8f+Time.deltaTime);
                //    jumpCounter -= 1;
                //   // Debug.Log($"jump{jumpCounter}");    

                //}else{
                //    rb.velocity = new Vector2(rb.velocity.x,rb.velocity.y+jumpForce+Time.deltaTime);
                //    jumpCounter -= 1;
                //   // Debug.Log($"jump{jumpCounter}"); 

                // }

                
                  

            
            }

        }
    }

    public void attack(InputAction.CallbackContext context)
    {
        if(context.started && !context.performed)
        {

            animator.SetBool("isAttacking",true);
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position,attackRange,enemyLayers);


            foreach(Collider2D enemy in hitEnemies)
            {

                Debug.Log("We hit " + enemy.name);
                enemy.GetComponent<Enemy>().TakeDamage(attackDamage);

                
            }



        }else{
            animator.SetBool("isAttacking",false);
        }
    }
 

    

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Ground"))
        {
            jumpCounter = numberOfJumps;

            isJumping = false;
            animator.SetBool("isJumping", false);
            //UnityEngine.Debug.Log("check");
        }
    }
    private void OnCollisionExit2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Ground"))
        {

            isJumping = true;
            
            jumpCounter--;
            
        }
    }

    private void KillPlayer()
    {
        if(animator != null)
        {
            animator.SetTrigger("PlayerDeath");
        }
        
        rb.velocity = Vector3.zero;
        playerControl.Disable();
        canMove = false;

        
        deathSound.Play();


        walkingSound.SetActive(false);
        
    }

    public void AlivePlayer()
    {
        playerControl.Enable();
        if(animator != null)
        {
            animator.SetTrigger("PlayerSpawn");
        }
        
        canMove = true;
        isJumping = false;
        jumpCounter = numberOfJumps;
        walkingSound.SetActive(true);
    }

    

    
}
