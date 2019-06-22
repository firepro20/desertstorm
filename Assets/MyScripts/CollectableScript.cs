using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>())
        {
            Destroy(gameObject);
        }
    }

    // create enum for better handling of multiple collectable items

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.GetComponent<PlayerController>())
    //    {
    //        Destroy(gameObject);
    //    }
    //}

    private void OnDestroy()
    {
        //play pickup sound

    }


}
