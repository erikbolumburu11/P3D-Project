using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerWeapons : MonoBehaviour
{
    [SerializeField] GameObject weaponHolder;
    Weapon equippedWeapon;

    [SerializeField] InputActionReference fireInput;

    bool readyToFire = true;

    void Start(){
        equippedWeapon = weaponHolder.GetComponentInChildren<Weapon>();
    }

    void Update(){
        if(fireInput.action.IsPressed()){
            Fire();
        }
    }

    void Fire(){
        if(!readyToFire) return;
        readyToFire = false;

        Invoke(nameof(ResetFire), equippedWeapon.fireRate);

        equippedWeapon.Fire();
    }

    void ResetFire(){
        readyToFire = true;
    }
}