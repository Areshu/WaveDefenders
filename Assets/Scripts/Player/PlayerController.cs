using System.Collections;
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
    private const float RELOAD_TIME = 3f;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        playerInput = GetComponent<PlayerInput>();

        sceneController.UpdateAmmo(ammo);
        sceneController.UpdateLife(life);
    }

    void Update()
    {
        if (life > 0 && !sceneController.IsGameOver())
        {
            horizontal = playerInput.horizontalAxis;
            isShooting = playerInput.fire;
            reload = playerInput.reload;

            if (!isReloading)
            {
                if (isShooting && ammo > 0 && Time.time >= timeToFire)
                {
                    //Disparos por segundo
                    timeToFire = Time.time + 1 / fireRate;
                    projectileSpawner.SpawnVFX();

                    --ammo;

                    sceneController.UpdateAmmo(ammo);
                }

                if (reload && ammo < MAX_AMMO)
                {
                    isReloading = true;
                    StartCoroutine(LetsReload());
                }
            }
        }
    }

    private void FixedUpdate()
    {
        if (horizontal != 0)
        {
            //Velocidad Angular
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
        sceneController.Reloading();

        yield return new WaitForSeconds(RELOAD_TIME);

        ammo = MAX_AMMO;

        isReloading = false;

        sceneController.UpdateAmmo(ammo);
    }
}
