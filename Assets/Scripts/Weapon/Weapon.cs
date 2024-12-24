using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public float fireRate = 0.25f;

    public AudioClip shotSound;

    void Start(){
        Reload();
    }

    public abstract void Fire();

    public abstract void Reload();

}