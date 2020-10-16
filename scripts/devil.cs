using UnityEngine;
using System;

public class devil : MonoBehaviour
{
    int health;
    public Animator anim;
    public float move = 10f;
    private Rigidbody2D pl;
    private Vector3 movedir;
    private SpriteRenderer sprite;
    public string playerstate;
    public string gamestate;
    public float knockbackForce;                    // force by which object gets knocked back
    public float knockbackTime;                     // amount of time the object gets knocked back
    private float knockBackTimer;                   // controls how long the object will be affected by knockback and not move/be invincible
    private void Awake()
    {
        anim = GetComponent<Animator>();
        pl = GetComponent<Rigidbody2D>();
        sprite=GetComponent<SpriteRenderer>();
    }
    private void FixedUpdate()
    {
        if (knockBackTimer <= 0)                 // Checks if player has hit an obstacle
        {
            playerstate = GameObject.FindGameObjectWithTag("playermanager").GetComponent<playermanager>().playerstate;
            gamestate = GameObject.FindGameObjectWithTag("playermanager").GetComponent<playermanager>().gamestate;
            health = GetComponent<PlayerHealth>().currentHealth;
            if (health <= 0)
            {
                GameObject.FindGameObjectWithTag("playermanager").GetComponent<playermanager>().gamestate = "GAMEOVER";
                death();
                Destroy(this.gameObject, 2f);
            }
            if (gamestate == "playing")
            {
                if (playerstate == "DEVIL")
                {
                    facemouse();
                    animation_motion();
                    pl.velocity = movedir * move;
                }
                else if (playerstate == "DOG")
                {
                    pl.velocity = new Vector2(0f, 0f);
                    anim.SetBool("ir", false);
                    anim.SetBool("il", false);
                    anim.SetBool("ib", false);
                    anim.SetBool("walkf", false);
                    anim.SetBool("walkb", false);
                    anim.SetBool("walkl", false);
                    anim.SetBool("walkr", false);
                }
            }
        }
        else
        {
            knockBackCounter -= Time.deltaTime;                 
        }
        
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.CompareTag("key"))
        {

            Destroy(collider.gameObject);
            GameObject.FindGameObjectWithTag("playermanager").GetComponent<playermanager>().gamestate="LEVELCOMPLETE";
        }
    }

    void death()
    {
        pl.velocity=new Vector2(0f,0f);
        
        anim.SetBool("walkf", false); anim.SetBool("walkl", false); anim.SetBool("walkr", false); anim.SetBool("walkb", false);
        anim.SetTrigger("dead");
    }
    
    private void animation_motion()
    {

        float moveX = 0f, moveY = 0f;
        
        if (Input.GetKey("w"))
        {
           
            anim.SetBool("walkf", false); anim.SetBool("walkl", false); anim.SetBool("walkr", false);
            anim.SetBool("walkb", true);
            moveY = 1f;
        }
        else if (Input.GetKey("s"))
        {
            
            anim.SetBool("walkl", false); anim.SetBool("walkr", false); anim.SetBool("walkb", false);
            anim.SetBool("walkf", true);
            moveY = -1f;
        }
        else if (Input.GetKey("d"))
        {
            sprite.flipX=false;
            anim.SetBool("walkf", false); anim.SetBool("walkl", false); anim.SetBool("walkb", false);
            anim.SetBool("walkr", true);
            moveX = 1f;
        }
        else if (Input.GetKey("a"))
        {
            sprite.flipX=false;
            anim.SetBool("walkf", false); anim.SetBool("walkr", false); anim.SetBool("walkb", false);
            anim.SetBool("walkl", true);
            moveX = -1f;
        }
       
        else
        {
            anim.SetBool("walkf", false); anim.SetBool("walkl", false); anim.SetBool("walkr", false); anim.SetBool("walkb", false);
        }
        movedir = new Vector3(moveX, moveY).normalized;
    }
    void facemouse()
    {
        Vector3 mouse = Input.mousePosition;
        mouse = Camera.main.ScreenToWorldPoint(mouse);
        float x, y;
        x = mouse.x - transform.position.x;
        y = mouse.y - transform.position.y;
        //Debug.Log(x);
       
        if (Input.GetMouseButton(0))
        {
            anim.SetBool("ir", false); 
            anim.SetBool("il", false);
            anim.SetBool("ib", false);
            anim.SetBool("walkf", false);
            anim.SetBool("walkb", false); 
            anim.SetBool("walkl", false); 
            anim.SetBool("walkr", false);
            anim.SetTrigger("attackl");
             if (x>=0)
                sprite.flipX=false;
            else 
                sprite.flipX=true;
            
        }
        else if (x > 20 || x<-20 )
        {
              if (x>=0)
                sprite.flipX=false;
            else 
                sprite.flipX=true;
           // Debug.Log("ExecuteInEditMode");
            anim.SetBool("ir", false); anim.SetBool("ib", false);
            anim.SetBool("il", true);
        }
        else
        {
            
            anim.SetBool("ir", false); anim.SetBool("il", false);
            anim.SetBool("ib", false);
        }
       
    }

    // function to cause knockback on hitting obstacles
    public void KnockBack(Vector3 direction)
    {
        knockBackTimer = knockbackTime;                       // Sets the timer for knockback
        movedir = direction * knockbackForce;                 // Sets the direction for knockback
        movedir.z = knockbackForce;                           // To make the devil jump up on knockback
    }
}