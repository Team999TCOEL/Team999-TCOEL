using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : Weapon
{
    
    void Start()
    {
        fWeaponFireRate = 1f;
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && weaponManager.go_CurrentWeapon.name == this.name) {
            if (Time.time > fWeaponFireRate + fLastShot) {
                InstantiateBullet();
                fLastShot = Time.time;
            }
        }
    }

}
