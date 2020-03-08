﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(PlayerInput))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [Tooltip("Degrees per second")]
    [SerializeField] private float rotVelocity;
    [Header("Shoot Settings")]
    [SerializeField] private float fireRate;
    [SerializeField] private SpawnProjectiles projectileSpawner;
    [Header("Others")]
    [SerializeField] private GameSceneController sceneController;

    private PlayerInput playerInput;
    private float horizontal;
    private bool isShooting;
    private bool reload, isReloading;
    private Rigidbody rigidBody;
    private Vector3 angularVelocity;
    private float timeToFire = 0;
    private int ammo = 30;
    private int life = 100;

    private const int MAX_AMMO = 30;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        playerInput = GetComponent<PlayerInput>();

        sceneController.UpdateAmmo(ammo);
        sceneController.UpdateLife(life);
    }

    // Update is called once per frame
    void Update()
    {
        if (life > 0)
        {
            horizontal = playerInput.horizontalAxis;
            isShooting = playerInput.fire;
            reload = playerInput.reload;

            if (isShooting && ammo > 0 && Time.time >= timeToFire)
            {
                //Shoots per second
                timeToFire = Time.time + 1 / fireRate;
                projectileSpawner.SpawnVFX();

                --ammo;

                sceneController.UpdateAmmo(ammo);
            }
            else if (isShooting && ammo <= 0)
            {
                Debug.Log("Out of Ammo.");
            }

            if (!isReloading && ammo < MAX_AMMO && reload)
            {
                isReloading = true;
                StartCoroutine(LetsReload());
            }
        }
    }

    private void FixedUpdate()
    {
        if (horizontal != 0)
        {
            //Make angular velocity
            angularVelocity = Vector3.up * rotVelocity * Mathf.Deg2Rad * horizontal;

            rigidBody.angularVelocity = angularVelocity;
        }
        else
        {
            rigidBody.angularVelocity = Vector3.zero;
        }
    }

    public void SetDamage (int damage)
    {
        if (life > 0)
        {
            life -= damage;
            sceneController.UpdateLife(life);
        }
    }

    IEnumerator LetsReload()
    {
        Debug.Log("RELOADING...");

        yield return new WaitForSeconds(3f);

        ammo = MAX_AMMO;
        Debug.Log("RELOADED");

        isReloading = false;

        sceneController.UpdateAmmo(ammo);
    }
}
