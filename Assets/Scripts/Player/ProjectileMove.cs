using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMove : MonoBehaviour
{
    public float speed;
    public float fireRate;
    //public GameObject muzzlePrefab;
    public GameObject HitPrefab;
    public GameObject HitEnemyPrefab;

    private int damage = 25;

    public void InitProjectile()
    {
        GetComponent<Rigidbody>().velocity = transform.forward * speed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        GetComponent<Rigidbody>().velocity = Vector3.zero;

        ContactPoint contact = collision.contacts[0];
        Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
        Vector3 pos = contact.point;



        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<Enemy>().SetDamage(damage);

            //Effects
            GameObject hitVFX = Instantiate(HitEnemyPrefab, pos, rot);
            ParticleSystem hitPartcls = hitVFX.GetComponent<ParticleSystem>();
            Destroy(hitVFX, hitPartcls.main.duration);
        }
        else
        {
            GameObject hitVFX = Instantiate(HitPrefab, pos, rot);
            ParticleSystem hitPartcls = hitVFX.transform.GetChild(0).GetComponent<ParticleSystem>();
            Destroy(hitVFX, hitPartcls.main.duration);
        }

        gameObject.SetActive(false);
    }
}
