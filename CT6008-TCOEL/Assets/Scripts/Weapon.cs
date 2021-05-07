using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [HideInInspector] public float fWeaponFireRate;
    public float fDamage;
    [HideInInspector] public float fLastShot = 0f;
    [SerializeField] public bool bCanFire = true;

    public Transform firePoint;
    public GameObject go_Bullet;

    public WeaponManager weaponManager;
    public GameObject go_WeaponManger;

    public BlackBoard blackBoard;

	private void Start() {

    }

	void Update()
    {
        
    }

    public void InstantiateBullet() {
        Instantiate(go_Bullet, firePoint.position, firePoint.rotation);
    }

    public void Shoot() {
        if (Time.time > fWeaponFireRate + fLastShot) {
            InstantiateBullet();
            fLastShot = Time.time;
        }
    }
}
