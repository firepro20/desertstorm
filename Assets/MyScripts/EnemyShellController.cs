using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShellController : MonoBehaviour
{

    public GameObject player;
    
    public GameObject shellExplosion;
    public float shellSpeed = 5f;
    public int shellDamage = 25;
    private Rigidbody shellRigidbody;
    public float shellTTL = 5f;

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
        OnScreenCheck();
        Destroy(gameObject, shellTTL); // guarantees destruction of object after its lifetime expires
    }

    private void OnCollisionEnter(Collision collision)
    {
        DestroyShell();       
    }

    private void OnScreenCheck()
    {
        // checks if visible by any camera, even Editor.
        if (!gameObject.GetComponent<MeshRenderer>().isVisible)
        {
            DestroyShell();
        }
    }

    public void DestroyShell()
    {
        // Destroy the bullet, instantiate bulletExplosion prefab, before destroying the component
        GameObject explosion = Instantiate(shellExplosion, transform.position, transform.rotation);
        explosion.GetComponent<ParticleSystem>().Play();
        float totalPlayback = explosion.GetComponent<ParticleSystem>().main.duration + explosion.GetComponent<ParticleSystem>().main.startLifetime.constant;
        Destroy(explosion, totalPlayback);
        Destroy(gameObject);
    }
}
