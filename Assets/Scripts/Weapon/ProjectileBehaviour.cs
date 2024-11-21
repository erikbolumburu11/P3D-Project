using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour
{
    Rigidbody rb;
    public float shootForce;
    public float damage;

    void Awake(){
        rb = GetComponent<Rigidbody>();
    }

    void Start(){
        rb.AddForce(transform.forward * shootForce, ForceMode.Impulse);
    }

    void OnCollisionEnter(Collision col){
        if(col.gameObject.TryGetComponent(out Health health)){
            health.Hurt(damage);
        }
        Destroy(gameObject);
    }
}
