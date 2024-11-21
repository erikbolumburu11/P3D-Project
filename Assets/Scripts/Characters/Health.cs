using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;

    void Awake(){
        currentHealth = maxHealth;
        UpdateHealthbar();
    }

    public void Hurt(float amount){
        currentHealth -= amount;
        CheckDeath();
        UpdateHealthbar();
    }

    void CheckDeath(){
        if(currentHealth <= 0) Die();
    }

    void Die(){
        Destroy(gameObject);
    }

    void UpdateHealthbar(){
        Healthbar healthbar = GetComponentInChildren<Healthbar>();
        if(healthbar == null) return;
        healthbar.SetValue(currentHealth/maxHealth);
    }
}