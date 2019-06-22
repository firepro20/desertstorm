using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellController : MonoBehaviour
{

    public GameObject player;
    public GameObject shellExplosion;
    public AudioClip shellExplosionSound;
    public float shellSpeed = 5f;
    public int shellDamage = 25;
    public float shellTTL = 5f;

    private Rigidbody shellRigidbody;
    

    private MeshRenderer[] playerRenders;
    private MeshRenderer turretRenderer;

    // Start is called before the first frame update
    void Start()
    {
        shellRigidbody = GetComponent<Rigidbody>();
        shellRigidbody.velocity = transform.forward * shellSpeed;
        playerRenders = player.GetComponentsInChildren<MeshRenderer>();
        foreach (MeshRenderer playerRenderers in playerRenders)
        {
            if(playerRenderers.gameObject.name == "TankTurret") // Why? ToString() should be giving the same result... or giving the name of the MeshRenderer itself. <- The latter is probably the case
            {
                // Set this renderer to be my turretRenderer
                turretRenderer = playerRenderers;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("ShellController - " + turretRenderer.gameObject.name);
        
        //Destroy(gameObject, shellTTL); // guarantees destruction of object after its lifetime expires
    }

    private void FixedUpdate()
    {
        // This is not a permanent solution as there may be times this will not work. To improve later
        if (!OnScreenCheck()) // we put this here as there was a bug were this was destroying object before instantiating, because
        {
            Debug.Log("I am not on screen anymore!");
            DestroyShell();
            //Destroy(gameObject, shellTTL);
        }   // render on camera is off before initiating.
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("I am " + collision.gameObject.name);
        DestroyShell();       
    }

    private bool OnScreenCheck()
    {
        // checks if visible by any camera, even Editor.
        if (!gameObject.GetComponent<MeshRenderer>().isVisible)
        {
            Destroy(gameObject);
            return false;
        }
        else
        {
            return true;
        }
    }

    public void DestroyShell()
    {
        // Destroy the bullet, instantiate bulletExplosion prefab, before destroying the component
        GameObject explosion = Instantiate(shellExplosion, transform.position, transform.rotation);
        explosion.GetComponent<ParticleSystem>().Play();
        AudioController.Instance.PlayShellExplosion();
        float totalPlayback = explosion.GetComponent<ParticleSystem>().main.duration + explosion.GetComponent<ParticleSystem>().main.startLifetime.constant;
        Destroy(explosion, totalPlayback);
        Destroy(gameObject);
        //Debug.Log("Destroy shell called");
    }
}
