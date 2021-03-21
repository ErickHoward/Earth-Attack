using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private GameObject deathFX;
    [SerializeField] private GameObject hitVFX;
    [SerializeField] private int value;
    [SerializeField] private int hitPoints = 10;
    [SerializeField] private bool pointsOnDeath = false;


    private Scoreboard scoreboard;
    private GameObject parentGameObject;
    private AudioSource audioSource;
    private void Start()
    {
        parentGameObject = GameObject.FindWithTag("SpawnedAtRuntime");
        scoreboard = FindObjectOfType<Scoreboard>();
        AddRigidbody();
        audioSource = GetComponent<AudioSource>();

    }

    private void AddRigidbody()
    {
        Rigidbody rb = gameObject.AddComponent<Rigidbody>();
        rb.drag = 1000000f;
        rb.angularDrag = 1000000f;
        rb.useGravity = false;

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
        vfx.transform.parent = parentGameObject.transform;
        if (pointsOnDeath == false)
        {
            scoreboard.UpdateScore(value);
        }
        hitPoints--;
    }
    private void KillEnemy()
    {
        if (pointsOnDeath == true)
        {
            scoreboard.UpdateScore(value);
        }
        GameObject fx = Instantiate(deathFX, transform.position, Quaternion.identity);
        fx.transform.parent = parentGameObject.transform;
        Destroy(gameObject);
    }


}
