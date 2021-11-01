using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIShotManger : MonoBehaviour
{
    public float shotTime;
    public Rigidbody rb;
    public float speed;
    private void Update()
    {
        shotTime -= Time.deltaTime;
        if(shotTime <= 0)
        {
            Destroy(gameObject);
        }
        rb.velocity = transform.forward * speed;
    }

    private void OnTriggerEnter(Collider other)
    {
       if(other.tag == "Player")
       {
            PlayerStats.HP -= 1;
            PlayerStatsMono.updateHealthText();
            Destroy(gameObject);
       }
    }
}
