using System.Collections;
using UnityEngine;

public class SpawnProjectiles : MonoBehaviour
{
    [SerializeField] private AudioSource aSource;
    public GameObject muzzlePrefab;

    private float muzzleTime;

    public void SpawnVFX()
    {
        muzzlePrefab.SetActive(true);
        ParticleSystem muzzPartcls = muzzlePrefab.transform.GetChild(0).GetComponent<ParticleSystem>();
        muzzleTime = muzzPartcls.main.duration;

        StartCoroutine(HideMuzzle());
        

        GameObject vfx;

        vfx = ObjectPooler.Instance.GetPooledObject(GameManager.BULLET_TAG);
        vfx.transform.position = transform.position;
        vfx.transform.rotation = transform.rotation;
        vfx.SetActive(true);
        vfx.GetComponent<ProjectileMove>().InitProjectile();

        if (aSource != null)
            aSource.Play();
        else
        {
            aSource.GetComponent<AudioSource>();
            aSource.Play();
        }
    }

    IEnumerator HideMuzzle()
    {
        yield return new WaitForSeconds(muzzleTime);
            muzzlePrefab.SetActive(false);
    }
}
