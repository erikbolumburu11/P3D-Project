using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileWeapon : Weapon
{
    [SerializeField] bool projectileVisibleWhenLoaded;
    [SerializeField] Transform projectileRestTransform;

    [SerializeField] GameObject projectileRestPrefab;
    [SerializeField] GameObject projectileFiredPrefab;

    GameObject loadedProjectile;

    public override void Fire(){
        if(projectileVisibleWhenLoaded){
            Destroy(loadedProjectile);
        }

        //Transform cameraTransform = FindObject.FindFPSCamera().transform;
        Transform cameraTransform = Camera.main.transform;
        GameObject projectile = Instantiate(
            projectileFiredPrefab,
            cameraTransform.position + cameraTransform.forward * 2,
            cameraTransform.rotation
        );

        SoundManager.instance.PlaySFX(shotSound);
    }


    public override void Reload()
    {
        if(projectileVisibleWhenLoaded){
            loadedProjectile = Instantiate(projectileRestPrefab, projectileRestTransform);
        }
    }
}
