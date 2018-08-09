using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Player player;

    Animator anim;

    float time;
    Vector3 targetPos;

	void Start ()
    {
        anim = GetComponent<Animator>();
        targetPos = new Vector3(Random.Range(-20, 20), transform.position.y, Random.Range(-20, 20));
    }

	void Update ()
    {
        if (Vector3.Distance(transform.position, targetPos) < 1)
            targetPos = new Vector3(Random.Range(-20, 20), transform.position.y, Random.Range(-20, 20));

        Vector3 diff = (targetPos - transform.position).normalized * 4.5f;
        diff = transform.InverseTransformDirection(diff);

        anim.SetFloat("VelocityX", diff.x);
        anim.SetFloat("VelocityZ", diff.z);

        Vector3 toPlayer = player.transform.position - transform.position;
        toPlayer.y = 0;
        transform.rotation = Quaternion.LookRotation(toPlayer, transform.up);
    }
}
