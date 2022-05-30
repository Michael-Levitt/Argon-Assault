using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Getting Hit")]
    [SerializeField] GameObject enemyExplosion;
    [SerializeField] int hitPoints = 4;
    [SerializeField] GameObject hitExplosion;
    GameObject parentGameObject;

    [Header("Scoreboard")]
    [SerializeField] int scoreIncrease = 15;
    Scoreboard scoreboard;

    void Start()
    {
        scoreboard = FindObjectOfType<Scoreboard>();
        parentGameObject = GameObject.FindWithTag("SpawnAtRuntime");
        AddRigidbody();
    }

    void OnParticleCollision(GameObject other)
    {
        ProcessHit();

        //Kill enemy when it reaches 0 hp
        if(hitPoints < 1)
        {
            KillEnemy();
        }
    }

    void AddRigidbody()
    {
        Rigidbody rb = gameObject.AddComponent<Rigidbody>();
        rb.useGravity = false;
    }

    void ProcessHit()
    {
        PlayExplosionVFX(hitExplosion);        
        hitPoints--;
    }

    void KillEnemy()
    {
        scoreboard.IncreaseScore(scoreIncrease); // Increase score on enemy kill
        PlayExplosionVFX(enemyExplosion);
        Destroy(gameObject);
    }

    void PlayExplosionVFX(GameObject explosionOnHit)
    {
        GameObject explosion = Instantiate(explosionOnHit, transform.position, Quaternion.identity); // Explosion (v & s)fx
        explosion.transform.parent = parentGameObject.transform; //Attach to parent for clarity
    }
}
