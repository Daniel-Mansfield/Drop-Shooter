using UnityEngine;

public class Gun : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;
    public float impactForce = 30f;
    public float fireRate = 10f;

    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;

    public void Shoot()
    {
        muzzleFlash.Play();

        GetComponent<Animator>().Play("Recoil");

        RaycastHit hit;
        if (Physics.Raycast(muzzleFlash.transform.position, muzzleFlash.transform.forward, out hit, range))
        {
            Target target = hit.transform.GetComponent<Target>();
            if (target != null)
            {
                target.TakeDamage(damage);
            }

            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * impactForce);
            }

            GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactGO, 1f);
        }
    }

    public void Drop()
    {
        transform.SetParent(null);
    }
}
