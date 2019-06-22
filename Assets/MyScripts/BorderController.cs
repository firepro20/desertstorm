using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BorderController : MonoBehaviour
{
    private BoxCollider borderCollider;
    private float borderColliderLength;

    public GameObject player;

    

    // Start is called before the first frame update
    void Start()
    {
        borderCollider = GetComponentInChildren<BoxCollider>();
        borderColliderLength = borderCollider.size.x;
        //Debug.Log("This is the size of border in transform units " + borderColliderLength);
    }

    // Update is called once per frame
    void Update()
    {
        /*
        //cameraPosition = Camera.main.transform.position.z;
        Debug.Log("The borderColliderLength value is " + borderColliderLength);
        if (player.transform.position.z > borderColliderLength)// we do it like this cause the background is not actually moving, the player and camera is.
        {
            // reset camera position to start new count, move border
            MoveBorder();
            cameraPosition = -borderColliderLength; // reset cameraPositionValue
        }
        Debug.Log("CameraPosition value is " + cameraPosition);
        */
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.GetComponent<PlayerController>())
        {
            Debug.Log("Player has entered the zone!");
            MoveBorder();
        }
    }

    private void MoveBorder()
    {
        Vector3 offset = new Vector3(0, 0, borderColliderLength * 3f);
        transform.position = transform.position + offset;
    }

    
}
