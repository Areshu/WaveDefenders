using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnProjectiles : MonoBehaviour
{
    public void SpawnVFX()
    {
        GameObject vfx;

        vfx = ObjectPooler.Instance.GetPooledObject("PlayerBullet");
        vfx.transform.position = transform.position;
        vfx.transform.rotation = transform.rotation;
        vfx.SetActive(true);
        vfx.GetComponent<ProjectileMove>().InitProjectile();
    }
}
