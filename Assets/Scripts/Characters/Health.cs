using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;

    [SerializeField] bool isPlayerHealth;
    [SerializeField] Healthbar HUDHealthbar;
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

    public void Die(){
        if(gameObject.CompareTag("Player")){
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        } 
        else{
            ScoreManager.instance.score += 100;
            Destroy(gameObject);
        }
    }

    void UpdateHealthbar(){
        if(isPlayerHealth){
            if(HUDHealthbar == null) return;
            HUDHealthbar.SetValue(currentHealth/maxHealth);
        }
        else{
            Healthbar healthbar = GetComponentInChildren<Healthbar>();
            if(healthbar == null) return;
            healthbar.SetValue(currentHealth/maxHealth);
        }
    }
}