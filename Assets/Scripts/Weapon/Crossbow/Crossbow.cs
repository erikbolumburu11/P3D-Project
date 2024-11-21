using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crossbow : ProjectileWeapon
{
    public override void Reload()
    {
        base.Reload();
    }

    public override void Fire()
    {
        base.Fire();
        Invoke(nameof(Reload), fireRate);
    }
}
