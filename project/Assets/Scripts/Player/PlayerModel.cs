using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerModel : MonoBehaviour
{
    [Header("Movement")]
    public float sprintSpeed;
    public float walkSpeed;
    public float rotationSpeed;
    public float jumpForce;

    [Header("Keys")]
    public KeyCode walkKey;
}
