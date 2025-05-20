using System;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public float Movement { get; private set; }


    private void Update()
    {
        Movement = Input.GetAxisRaw("Horizontal");
    }
}
