using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public int playerNumber;

    public float movementValue;
    public float rotationValue;
   
    public string movementInputName = "Vertical";
    public string rotationInputName = "Horizontal";

   float speed = 6f;
   float rotSpeed = 180f;

    public Rigidbody rb;
   

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        if(gameObject.tag == "Player1")
        {
            playerNumber = 1;
            movementInputName = movementInputName + playerNumber.ToString();
            rotationInputName = rotationInputName + playerNumber.ToString();
        }
        else
        {
            playerNumber = 2;
            movementInputName = movementInputName + playerNumber.ToString();
            rotationInputName = rotationInputName + playerNumber.ToString();
        }
    }
    private void Update()
    {
        movementValue = Input.GetAxis(movementInputName);
        rotationValue = Input.GetAxis(rotationInputName);
    }

    void FixedUpdate()
    {
        Move();
        Turn();
    }

    void Move()
    {
        Vector3 movement = transform.forward * movementValue * speed * Time.deltaTime;
        rb.MovePosition(rb.position + movement);
    }
    void Turn()
    {
        float angle = rotationValue * rotSpeed * Time.deltaTime;
        Quaternion rotationEuler = Quaternion.Euler(0f, angle, 0f);
        rb.MoveRotation(rb.rotation * rotationEuler);
    }
}
