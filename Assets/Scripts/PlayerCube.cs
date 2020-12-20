using System.Collections;
using System.Collections.Generic;
using BeardedManStudios.Forge.Networking;
using BeardedManStudios.Forge.Networking.Generated;
using BeardedManStudios.Forge.Networking.Unity;
using UnityEngine;

public class PlayerCube : PlayerCubeBehavior
{
    public float upForce = 1;
    
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

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            networkObject.SendRpc(RPC_MOVE_UP,Receivers.All,upForce);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            networkObject.SendRpc(RPC_MOVE_DOWN,Receivers.All);
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

    public override void MoveUp(RpcArgs args)
    {
        MainThreadManager.Run(() =>
        {
            float _upForce = args.GetNext<float>();
            
            transform.position += Vector3.up * _upForce;
        });
    }

    public override void MoveDown(RpcArgs args)
    {
        MainThreadManager.Run(() =>
        {
            transform.position += Vector3.down;
        });
    }
}
