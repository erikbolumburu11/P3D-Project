using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttackAnimationEvent : MonoBehaviour
{
    MeleeWeapon weapon;

    void Awake(){
        weapon = GetComponentInChildren<MeleeWeapon>();
    }

    public void OpenHitbox(){
        weapon.OpenHitbox();
    }

    public void CloseHitbox(){
        weapon.CloseHitbox();
    }

}
