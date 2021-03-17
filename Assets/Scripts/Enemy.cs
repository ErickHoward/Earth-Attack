using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject deathFX;
    [SerializeField] GameObject hitFX;
    [SerializeField] Transform parent;
    [SerializeField] int value;
    [SerializeField] int hitPoints = 10;


    Scoreboard scoreboard;

    private void Start() 
    {
        scoreboard = FindObjectOfType<Scoreboard>();
    }
    private void OnParticleCollision(GameObject other)
    {
        ProcessHit();
        if (hitPoints < 1)
        {            
            KillEnemy();
        }

    }
    private void ProcessHit()
    {
        GameObject vfx = Instantiate(deathFX, transform.position, Quaternion.identity);
        vfx.transform.parent = parent;
        scoreboard.UpdateScore(value);
        hitPoints--;
    }
    private void KillEnemy()
    {

        GameObject vfx = Instantiate(deathFX, transform.position, Quaternion.identity);
        vfx.transform.parent = parent;
        Destroy(gameObject);
    }


}
