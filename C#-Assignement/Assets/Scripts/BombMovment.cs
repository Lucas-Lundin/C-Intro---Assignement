using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombMovment : MonoBehaviour
{
    [SerializeField] private GameObject colliderCheckerPrefab;

    [SerializeField] private int MaxHits = 25;
    [SerializeField] private float Radius = 10f;
    [SerializeField] private float ExplosiveForce = 1000f;
    [SerializeField] private float ExplosivveUpForce = 0.001f;
    [SerializeField] private LayerMask HitLayer;
    [SerializeField] private LayerMask BlockExplosionLayer;
    private int MaxDamage = 10;
    private int MinDamage = 1;

    private Collider[] Hits;
    private void Awake()
    {
        Hits = new Collider[MaxHits];
    }


    void OnCollisionEnter(Collision collisionInfo)
    {   
        // Spawn a collider marker:
        GameObject spawnedCheckerPrefab;
        spawnedCheckerPrefab = Instantiate(colliderCheckerPrefab, transform.position, transform.rotation);

        //Instantiate(ParticleSystemPrefab, transform.position, Quaternion.identity);
        int hits = Physics.OverlapSphereNonAlloc(transform.position, Radius, Hits, HitLayer);

        for (int i = 0; i < hits; i++)
        {
            if (Hits[i].TryGetComponent<Rigidbody>(out Rigidbody rigidbody))
            {
                float distance = Vector3.Distance(transform.position, Hits[i].transform.position);

                if (!Physics.Raycast(transform.position, (Hits[i].transform.position - transform.position).normalized, distance, BlockExplosionLayer.value))
                {
                    rigidbody.AddExplosionForce(ExplosiveForce, transform.position, Radius, ExplosivveUpForce);
                    Debug.Log($"Would hit {rigidbody.name} for {Mathf.FloorToInt(Mathf.Lerp(MaxDamage, MinDamage, distance / Radius))}");
                    // Check players only
                    if (rigidbody.name == "Player Two")
                    {
                        rigidbody.GetComponent<PlayerHealth>().ModifyHP(Mathf.FloorToInt(Mathf.Lerp(MaxDamage, MinDamage, distance / Radius)));
                        Debug.Log("P2");
                    }

                }
            }
        }
        Destroy(gameObject);
    }

}

   


