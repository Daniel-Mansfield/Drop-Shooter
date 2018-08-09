using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Transform gunPos;

    HashSet<Gun> guns;
    Gun equipped;

	// Use this for initialization
	void Start () {
        guns = new HashSet<Gun>();

        if (GetComponentInChildren<Gun>() != null)
        {
            guns.Add(GetComponentInChildren<Gun>());
            equipped = GetComponentInChildren<Gun>();
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        foreach (Gun gun in guns)
        {
            // checks if player has fired
            if (Input.GetButtonDown("Fire1"))
            {
                gun.Shoot();
            }
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                gun.Drop();
                equipped = null;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<Gun>() == null || guns.Contains(other.GetComponent<Gun>())) { return; }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (equipped) { equipped.Drop(); }
            equipped = other.GetComponent<Gun>();

            guns.Add(equipped);

            other.transform.SetParent(gunPos);
            other.transform.localPosition = Vector3.zero;
            other.transform.localEulerAngles = Vector3.zero;
        }
    }
}
