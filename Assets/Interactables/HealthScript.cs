using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HealthScript : MonoBehaviour
{
    public float health = 50f;
    public UnityEvent onBreak;
    public bool usesEvent = false;

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0f)
        {
            Break();
        }
    }

    void Break()
    {
        if (usesEvent)
        {
            onBreak.Invoke();
        }
        Destroy(gameObject);
    }
}
