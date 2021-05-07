using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SMG : Weapon
{
    void Start() {
        LoadSMG();
        fWeaponFireRate = 0.2f;
        //fDamage = 17f;
    }


    void Update() {
        if (Input.GetKey(KeyCode.F) && weaponManager.go_CurrentWeapon.name == this.name && blackBoard.fPlayerHealth > 0) {
            if (Time.time > fWeaponFireRate + fLastShot) {
                InstantiateBullet();
                fLastShot = Time.time;
            }
        }
    }

    public void SaveSMG() {
        SaveSystem.SaveSMG(this);
        Debug.Log("Saving");
	}

    void LoadSMG() {
        SMGData data = SaveSystem.LoadSMG();
        fDamage = data.fSMGDamage;
	}
}
