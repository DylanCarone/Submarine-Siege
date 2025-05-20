using System;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public float Movement { get; private set; }
    public KeyCode leftFireButton;
    public KeyCode rightFireButton;


    private void Update()
    {
        Movement = Input.GetAxisRaw("Horizontal");
    }
}
