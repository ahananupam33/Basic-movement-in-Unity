using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public HealthBar healthBar;
    public int damage = 10;
    public float firerange;
    //private GameObject ob;
    //private Transform rb;
    //private float vec;
    //private bool takedmg;
    int fireon=0;
    Vector3 fireposition;
    float timer=1f;
    float time=0;
    public devil Devil;             // reference to the devil.cs script
    
    private void Start()
    {
        currentHealth = maxHealth;
        healthBar.setMaxHealth(maxHealth);
       // ob = GameObject.FindWithTag("enemy");
        //rb = ob.GetComponent(Transform);
        

    }

    void Update()
    {
        time+=Time.deltaTime;
        if(fireon==1)
        {
            Infire();

        }
       
    }

    

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.CompareTag("fire")   )
        {
            fireon=1;
            fireposition=collider.transform.position;
            time=0;
            Infire();
            TakeDamage(damage);
        }
        if (collider.CompareTag("osc"))
        {
            TakeDamage(damage);
        }
        /*if (collider.gameObject.tag == "enemy")
        {
            takedmg = true;
            fireon = 1;
            fireposition = collider.transform.position;
            time = 0;
            Infire();
            TakeDamage(damage);
        }*/
       //else { takedmg = false; }

    }

    /*private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "enemy")
        {
            takedmg = false;
        }
    }*/
    void Infire()
    {
        float player_distance = Vector3.Distance(fireposition, transform.position);
       // Debug.Log(player_distance);
        if(player_distance<firerange /*|| takedmg==true*/ )
        {
            if(time>timer)
            {
                TakeDamage(damage);
                time=0f;
            }
        }

        else
        {
            fireon=0;

        }
    }
    

    private void TakeDamage(int damage, Vector3 Kdirection)
    {
        if(currentHealth>0)
        {
            currentHealth -= damage;
            healthBar.setHealth(currentHealth);
            Devil.KnockBack(Kdirection);                    // Knocks the player back on taking damage
        }
    }
}
