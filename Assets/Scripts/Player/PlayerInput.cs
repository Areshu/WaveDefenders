using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [HideInInspector] public float horizontalAxis;
    [HideInInspector] public bool reload;
    [HideInInspector] public bool fire;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        fire = Input.GetButtonDown("Fire");
        reload = Input.GetButtonDown("Reload");

        horizontalAxis = Input.GetAxis("Horizontal");
    }
}
