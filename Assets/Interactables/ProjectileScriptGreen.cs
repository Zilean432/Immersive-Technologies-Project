using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScriptGreen : MonoBehaviour
{
    public float speed = 50f;
    public float lifeTimer = 3f;

    public float damage = 10f;

    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.forward * speed;
        Destroy(gameObject, lifeTimer);
    }

    void Update()
    {

    }

    // Update is called once per frame
    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
        {
            if (collision.gameObject.CompareTag("GreenTarget"))
            {
                HealthScript healthScript = collision.gameObject.GetComponent<HealthScript>();
                if (healthScript != null)
                {
                    healthScript.TakeDamage(damage);
                }
            }
            Destroy(gameObject);
        }
    }
}
