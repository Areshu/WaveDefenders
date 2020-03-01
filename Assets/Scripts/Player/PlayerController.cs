using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(PlayerInput))]
public class PlayerController : MonoBehaviour
{
    [Tooltip("Degrees per second")]
    [SerializeField] private float rotVelocity;

    private PlayerInput playerInput;
    private float horizontal;
    private Rigidbody rigidBody;
    private Vector3 angularVelocity;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        playerInput = GetComponent<PlayerInput>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = playerInput.horizontalAxis;
    }

    private void FixedUpdate()
    {
        if (horizontal != 0)
        {
            // TODO - hacer que el valor de horizontal sea siempre -1, 0 o 1

            //Make angular velocity
            angularVelocity = Vector3.up * rotVelocity * Mathf.Deg2Rad * horizontal;

            rigidBody.angularVelocity = angularVelocity;
        }
        else
        {
            rigidBody.angularVelocity = Vector3.zero;
        }
    }
}
