using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class PlayerController : MonoBehaviour
{
    //Power up variables
    public int powerUpLevel = 0;
    private float doubleBulletOffset = 0.5f;

    //Game Over Screen
    public TextMeshProUGUI gameOverText;
    public Button restartButton;

    //Movement variables
    public float speed = 10.0f;
    public float bankSpeed = -50.0f;
    public float verticalInput;
    public float horizontalInput;
    //Bounds Variables
    private float horiBounds = 15.0f;
    private float upperVertBounds = 16.0f;
    private float lowerVertBounds = -20.0f;
    //Fire Variables
    public float fireNext = 0.0f;
    public float fireRate = 0.20f;
    public GameObject bulletPrefab;
    //Audio Variables
    public AudioClip powerUpSound;
    public AudioClip maxPowerUpSound;
    public AudioClip shootSound;
    public AudioClip gameOverExplosion;
    private AudioSource playerAudio;
    //Particle Effects
    public ParticleSystem explosionParticle;

    // Start is called before the first frame update
    void Start()
    {
        //Get Audio Component
        playerAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
        ConstrictPlayerBounds();

        if (Input.GetKey(KeyCode.Space) && Time.time > fireNext)
        {
            fireNext = Time.time + fireRate;
            Shoot();
        }

    }
    //Moves the player based on arrow key input
    void MovePlayer()
    { 
        verticalInput = Input.GetAxis("Vertical");
        horizontalInput = Input.GetAxis("Horizontal");

        transform.Translate(Vector3.forward * verticalInput * speed * Time.deltaTime, Space.World);
        transform.Rotate(Vector3.forward * horizontalInput * bankSpeed * Time.deltaTime);
        transform.Translate(Vector3.right * horizontalInput * (1.5f * speed) * Time.deltaTime, Space.World);
    }
    //Prevent player from leaving play area
    public void ConstrictPlayerBounds()
    {
        if (transform.position.x < -horiBounds)
        {
            transform.position = new Vector3(-horiBounds, transform.position.y, transform.position.z);
        }
        if (transform.position.x > horiBounds)
        {
            transform.position = new Vector3(horiBounds, transform.position.y, transform.position.z);
        }

        if (transform.position.z < lowerVertBounds)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, lowerVertBounds);
        }
        if (transform.position.z > upperVertBounds)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, upperVertBounds);
        }
    }
    //Shooting mechanic
    void Shoot()
    {
        switch (powerUpLevel)
        {
            case 0:
                Instantiate(bulletPrefab, transform.position, bulletPrefab.transform.rotation);
                break;
            //First power up - shoot 2 bullets
            case 1:
                Instantiate(bulletPrefab, new Vector3 (transform.position.x + doubleBulletOffset, transform.position.y, transform.position.z), bulletPrefab.transform.rotation);
                Instantiate(bulletPrefab, new Vector3(transform.position.x - doubleBulletOffset, transform.position.y, transform.position.z), bulletPrefab.transform.rotation);
                break;
            //Bump up fire rate
            case 2:
                Instantiate(bulletPrefab, new Vector3(transform.position.x + doubleBulletOffset, transform.position.y, transform.position.z), bulletPrefab.transform.rotation);
                Instantiate(bulletPrefab, new Vector3(transform.position.x - doubleBulletOffset, transform.position.y, transform.position.z), bulletPrefab.transform.rotation);
                fireRate = 0.15f;
                break;
            //Shoot 3 bullets
            case 3:
                Instantiate(bulletPrefab, new Vector3(transform.position.x + (1.25f * doubleBulletOffset), transform.position.y, transform.position.z), bulletPrefab.transform.rotation);
                Instantiate(bulletPrefab, new Vector3(transform.position.x, transform.position.y, transform.position.z + doubleBulletOffset), bulletPrefab.transform.rotation);
                Instantiate(bulletPrefab, new Vector3(transform.position.x - (1.25f * doubleBulletOffset), transform.position.y, transform.position.z), bulletPrefab.transform.rotation);
                break;
            //Fastest fire rate
            case 4:
                Instantiate(bulletPrefab, new Vector3(transform.position.x + (1.25f * doubleBulletOffset), transform.position.y, transform.position.z), bulletPrefab.transform.rotation);
                Instantiate(bulletPrefab, new Vector3(transform.position.x, transform.position.y, transform.position.z + doubleBulletOffset), bulletPrefab.transform.rotation);
                Instantiate(bulletPrefab, new Vector3(transform.position.x - (1.25f * doubleBulletOffset), transform.position.y, transform.position.z), bulletPrefab.transform.rotation);
                fireRate = 0.10f;
                break;
            //Final form, 4 bullets
            case 5:
                Instantiate(bulletPrefab, new Vector3(transform.position.x + 2 * doubleBulletOffset, transform.position.y, transform.position.z - doubleBulletOffset), bulletPrefab.transform.rotation);
                Instantiate(bulletPrefab, new Vector3(transform.position.x - 2 * doubleBulletOffset, transform.position.y, transform.position.z - doubleBulletOffset), bulletPrefab.transform.rotation);
                Instantiate(bulletPrefab, new Vector3(transform.position.x + doubleBulletOffset, transform.position.y, transform.position.z), bulletPrefab.transform.rotation);
                Instantiate(bulletPrefab, new Vector3(transform.position.x - doubleBulletOffset, transform.position.y, transform.position.z), bulletPrefab.transform.rotation);
                break;

        }
        playerAudio.PlayOneShot(shootSound, 0.5f);
    }

    //Power Up Control
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Power Up"))
        {
            if (powerUpLevel <= 4)
            {
                powerUpLevel++;
                playerAudio.PlayOneShot(powerUpSound);
            } else
            {
                playerAudio.PlayOneShot(maxPowerUpSound);
            }
            Destroy(other.gameObject);
        }
        if (other.CompareTag("Enemy"))
        {
            DetachParticles(explosionParticle);
            playerAudio.PlayOneShot(gameOverExplosion);
            Destroy(gameObject);
            gameOverText.gameObject.SetActive(true);
            restartButton.gameObject.SetActive(true);
        }
    }

    //Particle detachment
    public void DetachParticles(ParticleSystem particleSystem)
    {
        // This splits the particle off so it doesn't get deleted with the parent
        particleSystem.transform.parent = null;
        particleSystem.Play();
    }
}
