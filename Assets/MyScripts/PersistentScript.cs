using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentScript : MonoBehaviour
{
    // Start is called before the first frame update
    private void Awake()
    {
        //GameObject[] persistentObjects;
        GameObject music = this.gameObject;

        //DontDestroyOnLoad(music);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
