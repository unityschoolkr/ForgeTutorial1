using System.Collections;
using System.Collections.Generic;
using BeardedManStudios.Forge.Networking.Generated;
using UnityEngine;

public class PlayerCube : PlayerCubeBehavior
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (!networkObject.IsOwner)
        {
            transform.position = networkObject.position;
            transform.rotation = networkObject.rotation;
            
            return;
        }
        
        transform.position += new Vector3(
            Input.GetAxis("Horizontal"),
            0,
            Input.GetAxis("Vertical")
        ) * Time.deltaTime * 5.0f;

        transform.Rotate(Vector3.up * Time.deltaTime * 90F);

        networkObject.position = transform.position;
        networkObject.rotation = transform.rotation;

    }
}
