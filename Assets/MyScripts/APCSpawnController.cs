using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class APCSpawnController : MonoBehaviour
{
    public int numberOfAPC = 2;
    public GameObject APCPrefab;

    public BoxCollider triggerCollider;
    public BoxCollider spawnArea;
    private bool alreadyTriggered = false;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("I should be Trigger Collider located at " + triggerCollider.bounds.size);
        Debug.Log("I should be Spawn Area non trigger collider located at " + spawnArea.bounds.size);
        // To make public to make thing easier
        /*
        foreach (Collider c in GetComponents<BoxCollider>())
        {
            if (c.enabled == false)
            {
                triggerCollider = (BoxCollider) c;
                Debug.Log("I should be Trigger Collider located at " + triggerCollider.bounds.size);
            }
            else
            {
                spawnArea = (BoxCollider) c;
                Debug.Log("I should be Spawn Area non trigger collider located at " + spawnArea.bounds.size);
            }
        }
        */
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("I have been triggered by " + other.name);
        if (alreadyTriggered == false && other.gameObject.GetComponent<PlayerController>())
        {
            Debug.Log("I have been triggered by Player");
            // avoid double trigger
            alreadyTriggered = true;
            SpawnAPC(numberOfAPC);
        }
    }

    private void SpawnAPC(int noToSpawn)
    {
        // To declare at top to avoid compile/runtime issues
        float xComponent = 0f;
        float zComponent = 0f;
        float xSpawnAreaSize = spawnArea.size.x;
        float zSpawnAreaSize = spawnArea.size.z;
        //Vector3 minPoint = spawnArea.bounds.min;
        //Vector3 maxPoint = spawnArea.bounds.max;
        //Debug.Log("" + xSpawnAreaSize + "\n" + zSpawnAreaSize + minPoint + maxPoint);
        for (int i = 0; i < noToSpawn; i++)
        {
           xComponent = Random.Range(-xSpawnAreaSize / 2, xSpawnAreaSize / 2);
           zComponent = Random.Range(-zSpawnAreaSize / 2, zSpawnAreaSize / 2);
           Vector3 randomPoint = new Vector3(xComponent, 1f, zComponent + transform.position.z); 
           //if () // I am not hitting any other collider, instantiate me.
           GameObject tempAPC = Instantiate(APCPrefab, randomPoint, Quaternion.Euler(new Vector3(0, 180, 0))); // + transform.position
            if (tempAPC!= null && tempAPC.GetComponent<APCController>().CurrentAPCOtherCollider() != null)
            {
             // ensure no two APC spawn on each other
             if (tempAPC.transform.position == tempAPC.GetComponent<APCController>().CurrentAPCOtherCollider().transform.position || tempAPC.GetComponent<APCController>().CurrentAPCOtherCollider().tag == "Environment")
              {
                  SpawnAPC(1);
              }
            }
           Debug.Log("The random spawn point for APC" + i + " is " + randomPoint);
        }
    }    
}
