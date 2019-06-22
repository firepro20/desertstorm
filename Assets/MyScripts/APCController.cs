using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class APCController : MonoBehaviour
{
    public GameObject player;
    public float hitPoints = 75.0f;
    public float moveSpeed = 2f;
    public float rotateSpeed = 2f;
    public float distanceFromPlayer = 2f;
    public GameObject tankExplosion;
    public GameObject[] pickups;

    private bool damagedPlayer = false;
    private GameObject tempObject;

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // I did this as I know I have one player with a constant tag. And also because an instance cannot be applied to a prefab.
        player = GameObject.FindGameObjectWithTag("Player");
        // If Idle, rotate turret left right
        
        // get position and lerp between up and down, IF not in camera
        // also lerp alpha of texture when hit.
        if (hitPoints > 0f)
        {
            // live and constantly look at player direction
            transform.LookAt(player.transform);
        }
        else
        {
            // die
            DestroyAPC(); 
        }

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerController>())
        {
            ChasePlayer();
        }
    }

    private void ChasePlayer()
    {
        // calculate MoveTowards
        if (Vector3.Distance(transform.position, player.transform.position) > distanceFromPlayer && !damagedPlayer)
        {
            float followSpeed = moveSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, followSpeed);
            // rotate

            Vector3 rotateDirection = player.transform.position - transform.position;
            Quaternion toRotation = Quaternion.LookRotation(transform.forward, rotateDirection);
            transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, rotateSpeed * Time.time);

            //transform.LookAt(player.transform);
        }
    }

    private void SpawnPickup() // eventually this can move to GameController, and create pickups array
    {
        int randOne = Random.Range(5, 17) % 4;
        int randTwo = Random.Range(1, 10);

        // Generates random value, based on value mod, which is 1 or 0 drops a pickup
        // the above executes only if another random value satisfies the initial condition
        if (randOne == 1)
        {
            if (randTwo > 6)
            {
                Instantiate(pickups[0], transform.position, transform.rotation);
            }
            if (randTwo < 4)
            {
                Instantiate(pickups[1], transform.position, transform.rotation);
            } 
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (hitPoints > 0.0f)
        {
            for (int i = 0; i < collision.contactCount; i++)
            {
                ContactPoint cp = collision.contacts[i];
                tempObject = cp.otherCollider.gameObject;
                if (tempObject.GetComponent<ShellController>())
                {
                    AudioController.Instance.PlayEnemyHit();
                    hitPoints = hitPoints - tempObject.GetComponent<ShellController>().shellDamage;
                    break;
                }
                if (tempObject.GetComponent<PlayerController>())
                {
                    damagedPlayer = true;
                }
            }
        }
    }

    public GameObject CurrentAPCOtherCollider()
    {
        if(tempObject != null)
        {
            return tempObject;
        }
        return null;
    }
    
    private void DestroyAPC()
    {
        GameObject explosion = Instantiate(tankExplosion, transform.position, transform.rotation);
        explosion.GetComponent<ParticleSystem>().Play();
        AudioController.Instance.PlayTankExplosion();
        AudioController.Instance.PlayScoreSound();
        GameController.Instance.UpdateScore(200);
        float totalPlayback = explosion.GetComponent<ParticleSystem>().main.duration + explosion.GetComponent<ParticleSystem>().main.startLifetime.constant;
        SpawnPickup();
        Destroy(explosion, totalPlayback);
        Destroy(gameObject);
    }
    
}
