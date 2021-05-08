using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SMG : Weapon
{
    void Start() {
        LoadSMG();
        fWeaponFireRate = 0.08f;
        fBulletsFired = fOverHeatRate;
        bRecharging = false;
        //fDamage = 17f;
    }


    void Update() {
        
        if(fBulletsFired > fOverHeatRate) {
            fBulletsFired = fOverHeatRate;
		}


        if (Input.GetKey(KeyCode.F) && weaponManager.go_CurrentWeapon.name == this.name && blackBoard.fPlayerHealth > 0 && fBulletsFired > 0 && bCanShoot == true) {
            fBulletsFired--;
            StartCoroutine("FireRate");
            Debug.Log(fBulletsFired);
            fLastShot = Time.time;
        } else if(!Input.GetKey(KeyCode.F) && fBulletsFired <= fOverHeatRate && bRecharging == false) {
            Debug.Log("Recharging");
            StartCoroutine("Recharge");
        } else if (fBulletsFired <= 0) {
             StartCoroutine("CoolDown");
        }
    }

    IEnumerator FireRate() {
        bCanShoot = false;
        InstantiateBullet();
        yield return new WaitForSeconds(0.14f);
        bCanShoot = true;
    }

    IEnumerator Recharge() {
        yield return new WaitForSeconds(0.6f);
        fBulletsFired = fBulletsFired + 1;
    }

    IEnumerator CoolDown() {
        bRecharging = true;
        yield return new WaitForSeconds(2.5f);
        fBulletsFired = fOverHeatRate;
        bRecharging = false;

    }

    public void SaveSMG() {
        SaveSystem.SaveSMG(this);
	}

    void LoadSMG() {
        SMGData data = SaveSystem.LoadSMG();
        fDamage = data.fSMGDamage;
	}
}
