using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : MonoBehaviour
{
    public float damage;

    Collider col;

    void Awake(){
        col = GetComponent<Collider>();
    }
    
    public void OpenHitbox(){
        col.enabled = true;
    }

    public void CloseHitbox(){
        col.enabled = false;
    }

    void OnTriggerEnter(Collider col){
        if(col.gameObject.TryGetComponent(out Health health)){
            if(col.CompareTag("Player")){
                health.Hurt(damage);
                CloseHitbox();
            }
        }
    }
}
