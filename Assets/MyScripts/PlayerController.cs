using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Variables and Initiators
    public float playerThrust = 1f;
    public float playerRotationSpeed = 1f;
    private Rigidbody playerRigidbody;
    private AudioSource audioSource;
    public AudioClip engineIdleClip;
    public AudioClip engineDrivingClip;

    public RectTransform crosshair;

    public Transform playerTurret;
    public GameObject shell;
    public float rateOfFire = 2f;
    private float nextFire = 0f;
    private GameObject weaponProjectile;
    //private bool playerIdle = true;
    //private bool playedOnce = false;

    // Other Variables and Initiators
    

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        playerRigidbody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        //screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        //gives x and y values that will be half the screen value. Screen coordinate system is reversed, so these will be negative values
        //playerWidth = transform.GetComponent<BoxCollider>().bounds.size.x / 2;
        //playerDepth = transform.GetComponent<BoxCollider>().bounds.size.z / 2;
        // we need half the size as we already are at the center of the object, using clamp
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            playerRigidbody.velocity = transform.forward * playerThrust;
        }
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            playerRigidbody.velocity = transform.forward * -(playerThrust);
        }
        */
       
            // instead of velocity we use addforce which give a more cartoony feeling to the game.
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            {
                playerRigidbody.AddForce(transform.forward * playerThrust * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) //add logic with player and camera position, create method
            {
                playerRigidbody.AddForce(transform.forward * -(playerThrust) * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                //Debug.Log("Turn right");
                transform.Rotate(0, playerRotationSpeed * Time.deltaTime, 0);
            }
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                //Debug.Log("Turn left");
                transform.Rotate(0, Time.deltaTime * -(playerRotationSpeed), 0);
            }
            if (Input.anyKey == false)
            {
                playerRigidbody.AddForce(Vector3.zero);
            }
            // Possibility of including engine sounds, at the moment issue with changing back to idle
            // Eventually this should be included in AudioMixer
            /* 
            if (((playerIdle) && playerRigidbody.GetPointVelocity(transform.position).magnitude < 1f))
            {
                audioSource.clip = engineIdleClip;
                audioSource.loop = true;
                audioSource.Play();
                playerIdle = !playerIdle;
                playedOnce = false;
            }
            else if (!playerIdle && playerRigidbody.GetPointVelocity(transform.position).magnitude > 1f && !playedOnce)
            {
                audioSource.clip = engineDrivingClip;
                audioSource.loop = true;
                audioSource.Play();
                playedOnce = true;
            }
            */
        

            MoveCrosshair();
            Fire();
        
    }

    private void LateUpdate()
    {
        WithinScreenBounds();
    }

    private void WithinScreenBounds()
    {
        // check if player does not exceed camera view, else stop all movement.
        //Vector3 viewPos = transform.position;
        //viewPos.x = Mathf.Clamp(viewPos.x, screenBounds.x + playerWidth, screenBounds.x * -1 - playerWidth);
        //viewPos.z = Mathf.Clamp(viewPos.z, screenBounds.y + playerDepth, screenBounds.y * -1 - playerDepth);
        //transform.position = viewPos;
        // the above will work with the center of the tank. We need to include the tank renderer
    }

    private void MoveCrosshair()
    {

        crosshair.transform.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        // Calculate difference between mouse and turret
        //Vector3 crossHairWorldPoint = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));
        //Vector3 difference = crossHairWorldPoint - playerTurret.transform.position;
        //float rotationY = Mathf.Atan2(difference.x, difference.z) * Mathf.Rad2Deg;

        // Turret rotates towards mouse pointer
        playerTurret.transform.rotation = Quaternion.Euler(0f, GetTurretRotation(), 0f);
        
    }

    private void Fire()
    {
        float offset = 2f;
        if (Input.GetMouseButton(0)) // no, this is incorrect, it should be onpress
        {
            if (Time.time > nextFire && Time.timeScale != 0)
            {
                AudioController.Instance.PlayCharge();
                // further steps need to be added when weapon powerup is picked, switch \ if clauses
                weaponProjectile = Instantiate(shell, playerTurret.transform.position + (offset * playerTurret.transform.forward), playerTurret.transform.rotation);
                // We can now use GetTurretRotation() for the y value, the new angle. However we will need to specify the angle our selves.
                nextFire = Time.time + 1/rateOfFire;
                AudioController.Instance.PlayShot();
            }
        }
    }

    private float GetTurretRotation()
    {
        // Calculate difference between mouse and turret
        Vector3 crossHairWorldPoint = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));
        Vector3 difference = crossHairWorldPoint - playerTurret.transform.position;
        float rotationY = Mathf.Atan2(difference.x, difference.z) * Mathf.Rad2Deg;
        return rotationY;
        // Returns the angle of rotation for playerTurret
    }

    /*
    public void ReceiveDamage(int damage)
    {
        if ()
    }
    */

    private void OnCollisionEnter(Collision collision)
    {
        for (int i = 0; i < collision.contactCount; i++)
        {
            ContactPoint cp = collision.contacts[i];
            // Enum from Collectable Script
            GameObject tempObject = cp.otherCollider.gameObject;
            if (tempObject.GetComponent<APCController>()) // better improved with enum, as getcontroller or tags is not scalable
            {
                GameController.Instance.UpdateHealth(-20);
                AudioController.Instance.PlayPlayerHit();
                break;
            }
            if (tempObject.GetComponent<CollectableScript>())
            {
                if (tempObject.name.Contains("CircleOfDeath"))
                {
                    Debug.Log("Matching CoD");
                    Destroy(tempObject);
                    cp.otherCollider.enabled = false;
                    CircleOfDeath();
                    GameController.Instance.UpdateScore(100);
                    AudioController.Instance.PlayPickUpOffensive();
                }
                else if (tempObject.name.Contains("HealthPickup"))
                {
                    AudioController.Instance.PlayHealthPickup();
                    Debug.Log("Matching Health");
                    if (GameController.Instance.GetHealth() >= 100)
                    {
                        GameController.Instance.UpdateScore(100);
                    }
                    else
                    {
                        GameController.Instance.UpdateHealth(20);
                    }
                }

                break;
            }
        }
    }

    private void CircleOfDeath()
    {
        
        float offset = 4f;
        int noOfBullets = 8;
        float angle = 360f / (float)noOfBullets;
        for (int i = 0; i < noOfBullets; i++)
        {
            Quaternion rotation = Quaternion.AngleAxis(i * angle, transform.up);
            Vector3 direction = rotation * transform.forward;
            Vector3 position = playerTurret.transform.position + (direction * offset);
            //weaponProjectile = Instantiate(shell, playerTurret.transform.position + (offset * playerTurret.transform.forward), playerTurret.transform.rotation);
            weaponProjectile = Instantiate(shell, position, rotation);
            Debug.Log("This is number " + i + " with position " + position);
        }
        AudioController.Instance.PlayShot();
        
    }
}