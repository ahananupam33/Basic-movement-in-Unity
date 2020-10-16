using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class oscilator : MonoBehaviour
{
    public int initialdirection;//for right 1 for left -1
    public int amplitude;
    public int oscilation_direction;//in x direction 1 or in y direction -1
    public float disp;
    public float delay;
    public int dealtdamage;
    [Range(0, 4)] [SerializeField] float oscillatingspeed=1f;
    Vector3 offset;
    Vector3 initialposition;
    void Start()
    {
        initialposition = transform.position;
       // InvokeRepeating("update", 0f, delay);
    }

    // Update is called once per frame
    void Update()
    {
        if (disp == 0)
        {
            if (oscilation_direction == 1)
                offset.x = initialdirection * amplitude * Mathf.Abs(Mathf.Sin(Time.time * Mathf.PI / oscillatingspeed));
            if (oscilation_direction == -1)
                offset.y = initialdirection * amplitude * Mathf.Abs(Mathf.Sin(Time.time * Mathf.PI / oscillatingspeed));
            transform.position = offset + initialposition;
        }

        else if (disp == 1)
        {
            if (oscilation_direction == 1)
                offset.x = initialdirection * amplitude * Mathf.Sin(Time.time * Mathf.PI / oscillatingspeed);
            if (oscilation_direction == -1)
                offset.y = initialdirection * amplitude * Mathf.Sin(Time.time * Mathf.PI / oscillatingspeed);
            transform.position = offset + initialposition;
        }
        else
            transform.position = initialposition;

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Checks if the devil came in contact with the oscillator
        if(other.gameObject.tag=="devil")
        {
            Vector3 hitDirection = other.transform.position - transform.position;
            hitDirection = hitDirection.normalized;                                     // if hitdirection becomes large then it is normalised
            FindObjectOfType<PlayerHealth>().TakeDamage(dealtdamage, hitDirection);
        }
    }
}
