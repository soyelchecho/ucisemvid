using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfigRotationScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var rotationVector = transform.rotation.eulerAngles;
        rotationVector.z = 0;
        rotationVector.x = 0;
        rotationVector.y = 0;
        transform.rotation = Quaternion.Euler(rotationVector);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
