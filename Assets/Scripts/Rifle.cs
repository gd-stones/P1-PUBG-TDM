using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rifle : MonoBehaviour
{
    [Header("Rifle")]
    public Camera cam;
    public float giveDamage = 10f;
    public float shootingRange = 100f;
    public float fireCharge = 15f;
    public PlayerMovement player;

    [Header("Rifle ammunition & shooting")]
    private float nextTimeToShoot = 0f;
    private int maximumAmmunition = 20;
    int mag = 15;
    int presentAmmunition;
    public float reloadingTime = 1.3f;
    bool setReloading = false;

    //[Header("Rifle effects")]
    //public ParticleSystem muzzleSpark;

    private void Awake()
    {
        presentAmmunition = maximumAmmunition;
    }

    private void Update()
    {
        if (setReloading)
            return;

        if (presentAmmunition <= 0)
        {
            StartCoroutine(Reload());
            return;
        }

        if (Input.GetButtonDown("Fire1") && Time.time >= nextTimeToShoot)
        {
            nextTimeToShoot = Time.time + 1f / fireCharge;
            Shoot();
        }
    }

    void Shoot()
    {
        if (mag == 0)
        {
            // show ammo out text
        }

        presentAmmunition--;
        if (presentAmmunition == 0)
        {
            mag--; 
        }

        //muzzleSpark.Play();
        RaycastHit hitInfo;

        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hitInfo, shootingRange))
        {
            Debug.Log(hitInfo.transform.name);

            Objects objects = hitInfo.transform.GetComponent<Objects>();

            if (objects != null)
            {
                objects.ObjectHitDamage(giveDamage);
            }
        }
    }

    IEnumerator Reload()
    {
        player.playerSpeed = 0f;
        player.playerSprint = 0f;
        setReloading = true;
        Debug.Log("Reloading...");
        // animation & audio
        yield return new WaitForSeconds(reloadingTime);
        // animations
        presentAmmunition = maximumAmmunition;
        player.playerSpeed = 1.9f;
        player.playerSprint = 3f;
        setReloading = false;
    }
}
