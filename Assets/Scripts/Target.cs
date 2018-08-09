using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public float health = 50f;

    float emission;
    HashSet<Material> materials;

    void Start()
    {
        materials = new HashSet<Material>();

        foreach (Renderer ren in GetComponentsInChildren<Renderer>())
        {
            materials.Add(ren.material);
            ren.material.EnableKeyword("_EMISSION");
        }
    }

    void Update()
    {
        emission = Mathf.Lerp(emission, 0, Time.deltaTime * 5);
        Color finalColor = Color.red * Mathf.LinearToGammaSpace(emission);

        foreach (Material mat in materials)
        {
            mat.SetColor("_EmissionColor", finalColor);
        }
    }

    public void TakeDamage (float amount)
    {
        health -= amount;
        emission = 1;
        if (health <= 0f)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }

}
