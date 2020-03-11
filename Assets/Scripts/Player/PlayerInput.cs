using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [HideInInspector] public float horizontalAxis;
    [HideInInspector] public bool reload;
    [HideInInspector] public bool fire;

    // Update is called once per frame
    void Update()
    {
        fire = Input.GetButton("Fire");
        reload = Input.GetButtonDown("Reload");

        horizontalAxis = Input.GetAxis("Horizontal");
    }
}
