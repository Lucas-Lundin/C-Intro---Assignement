using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] private GameObject colliderCheckerPrefab;

    private int maxHits = 45;
    private float collisionRadius = 12f;
    private float explosiveForce = 1500f;
    private float explosivveUpForce = 0.001f;
    [SerializeField] private LayerMask hitLayer;
    [SerializeField] private LayerMask blockExplosionLayer;
    private int explosionDamage = -1;

    private float bombTimerStart = 1.5f;
    private float bombTimerCurrent;
 
 

    private Collider[] Hits;
    private void Awake()
    {
        Hits = new Collider[maxHits];
    }

    private void Start()
    {
        bombTimerCurrent = bombTimerStart;
    }


    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            

        }
        bombTimerCurrent -= Time.deltaTime;
        if (bombTimerCurrent <= 0)
        {
            Explode();
        }
    }

    


    void OnCollisionEnter(Collision collisionInfo)
    {
        
    }

    private void Explode()
    {
        // Spawn a collider marker:
        GameObject spawnedCheckerPrefab;
        spawnedCheckerPrefab = Instantiate(colliderCheckerPrefab, transform.position, transform.rotation);

        //Instantiate(ParticleSystemPrefab, transform.position, Quaternion.identity);
        int hits = Physics.OverlapSphereNonAlloc(transform.position, collisionRadius, Hits, hitLayer);

        for (int i = 0; i < hits; i++)
        {
            if (Hits[i].TryGetComponent<Rigidbody>(out Rigidbody rigidbody))
            {
                float distance = Vector3.Distance(transform.position, Hits[i].transform.position);

                if (!Physics.Raycast(transform.position, (Hits[i].transform.position - transform.position).normalized, distance, blockExplosionLayer.value))
                {
                    rigidbody.AddExplosionForce(explosiveForce, transform.position, collisionRadius, explosivveUpForce);
                    //Debug.Log($"Would hit {rigidbody.name} for {Mathf.FloorToInt(Mathf.Lerp(MaxDamage, MinDamage, distance / collisionRadius))}");

                    // Damage to players:
                    if (rigidbody.gameObject == TurnManager.GetInstance().GetTurnObjectByIndex(1) || rigidbody.gameObject == TurnManager.GetInstance().GetTurnObjectByIndex(2))
                    {
                        rigidbody.GetComponent<PlayerHealth>().ModifyHP(explosionDamage);
                        Debug.Log("" + rigidbody.name + "ModifyHP " + explosionDamage);
                    }

                }
            }
        }
        Destroy(gameObject);
    }

}

   


