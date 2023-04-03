using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private GameObject player;
    public GameObject bulletPrefab;
    public GameObject powerUpPrefab;
    public GameObject deathEffect;

    Vector3 playerPos;

    private AudioSource playerAudio;
    public AudioClip bulletTing;
    public AudioClip explosionSound;

    public ParticleSystem explosionEffect;

    private SpawnManager gameManager;

    public int health = 1;

    public float horizonalSpeed = 15.0f;
    public float bulletSpeed = 5.0f;
    private float fireRate = 0.750f;
    private float fireNext = 0.5f;
    private float sprayAngle = 15.0f;

    //Drop chance of power up (99 max)
    private int dropChance = 10;

    private float initXPos;


    // Start is called before the first frame update
    void Awake()
    {
        //Find player
        player = GameObject.Find("Player");
        playerAudio = player.GetComponent<AudioSource>();
        bulletPrefab.GetComponent<Rigidbody>();
        //Get initial position so you can direct flight towards center of screen
        initXPos = transform.position.x;

        //Get Game Manager reference to update score with each enemy blown up
        gameManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();

    }

    // Update is called once per frame
    void Update()
    {
        Shoot();
        LateralMove();
        playerPos = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z);
    }
    void Shoot()
    {
        if (Time.time > fireNext)
        {
            //Get player position
            

            //Each enemy has its own shooting pattern
            fireNext = Time.time + fireRate;
            if (gameObject.name == "EnemyPlane(Clone)"  && !isBehindPlayer())
            {
                fireRate = 0.5f;
                Instantiate(bulletPrefab, transform.position, bulletPrefab.transform.rotation);
            }
            else if (gameObject.name == "EnemySinePlane(Clone)" && !isBehindPlayer())
            {
                fireRate = 0.75f;
                Instantiate(bulletPrefab, transform.position, bulletPrefab.transform.rotation);
                bulletPrefab.GetComponent<Rigidbody>().velocity = (player.transform.position - bulletPrefab.transform.position) * bulletSpeed;
            }
            else if (gameObject.name == "EnemyTank(Clone)")
            {
                fireRate = 1.0f;
                Instantiate(bulletPrefab, transform.position, bulletPrefab.transform.rotation = Quaternion.Euler(playerPos));
                Instantiate(bulletPrefab, transform.position, bulletPrefab.transform.rotation = Quaternion.Euler(playerPos.x, playerPos.y + sprayAngle, playerPos.z));
                Instantiate(bulletPrefab, transform.position, bulletPrefab.transform.rotation = Quaternion.Euler(playerPos.x, playerPos.y - sprayAngle, playerPos.z));
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Wall"))
        {
            horizonalSpeed = horizonalSpeed * -1;
        }

        if (other.CompareTag("Bullet"))
        {
            if(health > 0)
            {
                playerAudio.PlayOneShot(bulletTing, 1.0f);

                health--;
            }else if (health <= 0 && gameObject != null)
            {
                gameManager.UpdateScore(1);
                playerAudio.PlayOneShot(explosionSound, 1.0f);


                Destroy(gameObject);
                GameObject deathEffectClone = Instantiate(deathEffect, transform.position, transform.rotation);
                Destroy(deathEffectClone, 2);


                //Chance to drop powerup
                if (Random.Range(0, 100) > (99 - dropChance))
                {
                    Instantiate(powerUpPrefab, new Vector3 (transform.position.x, 2.0f, transform.position.z), powerUpPrefab.transform.rotation);
                }
            }
        }
    }

    void LateralMove()
    {
        if (initXPos < 0)
        {
            transform.Translate(Vector3.right * horizonalSpeed * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector3.right * -horizonalSpeed * Time.deltaTime);
        }

    }

    bool isBehindPlayer()
    {
        if (player.transform.position.z > transform.position.z)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
