using UnityEngine;

public class Gun : MonoBehaviour {

    public float damage = 10f;
    public float range = 100f;
    public float impactForce = 30f;
    public float fireRate = 10f;

    public Camera fpsCam;
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;

	// Update is called once per frame
	void Update () {
	    // checks if player has fired
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            Drop();
        }
	}

    void Shoot()
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

    void Drop()
    {
        GameObject.Find("sniperCamo").transform.SetParent(null);
    }

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.Q)) {
            GameObject.Find("sniperCamo").transform.SetParent(other.transform.GetChild(0));
            GameObject.Find("sniperCamo").transform.localPosition = new Vector3(0.125f, -0.125f, 0.5f);
            GameObject.Find("sniperCamo").transform.localEulerAngles = Vector3.zero;
        }
    }
}
