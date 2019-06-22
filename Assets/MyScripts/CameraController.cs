using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player;
    private MeshRenderer playerChassisMesh;
    private Vector3 offset;
    
    

    void Start()
    {
        offset = transform.position - player.transform.position;
        playerChassisMesh = player.GetComponentInChildren<MeshRenderer>();
        //Debug.Log("The selected mesh is " + playerChassisMesh.ToString() + " Size X " + playerChassisMesh.bounds.size.x + " Size Z " + playerChassisMesh.bounds.size.z);
        //Debug.Log("Bounds value of Tank - " + playerChassisMesh.bounds.size);
    }

    // LateUpdate is called once per frame, after all Update code has executed
    void Update()
    {
        //transform.position = player.transform.position + offset;
        //transform.position = new Vector3(transform.position.x, transform.position.y, Vector3.Magnitude(player.transform.position + offset)); // follows only on z-axis 
        // Bug - not countering for - offset
        // Bug - Going down still increases the magnitude, but translation is in the +ve, forward direction. Test Case - go down, left or right.
        if (player.transform.position.z > transform.position.z)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, player.transform.position.z); // -> This solve all problems
        }

        if (-(playerChassisMesh.bounds.size.z / 2f) + (player.transform.position.z) <= (transform.position.z - Camera.main.orthographicSize)) // I need to calculate the bounds.size which is a Vector3 and get its magnitude as read only value.
        {
            player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, (transform.position.z - Camera.main.orthographicSize) + (playerChassisMesh.bounds.size.z / 2f)); // add player renderer size offset
            // + (playerChassisMesh.bounds.size.z/2f) --> Add this to above if condition player.transform.position.z
            Debug.Log("Transform Player < Camera's end");
        }
    }
    // this needs to be updated to follow player only on z - axis AND not go down.
    private void LateUpdate()
    {
        
    }
}
