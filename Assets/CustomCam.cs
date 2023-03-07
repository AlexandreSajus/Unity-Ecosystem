using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomCam : MonoBehaviour
{

    public GameObject target;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Look at the target
        transform.LookAt(target.transform);
    }
}
