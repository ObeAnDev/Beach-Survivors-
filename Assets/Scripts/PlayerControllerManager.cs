using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class PlayerControllerManager : MonoBehaviour
{
    [SerializeField] float speed;
    public float Speed => speed;

    public float rotationSpeed;

    Rigidbody rb;

    public Transform playerModel;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        float moveVertical = Input.GetAxisRaw("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical).normalized;

        rb.velocity = new Vector3(movement.x * speed, rb.velocity.y, movement.z * speed);

        if (moveHorizontal != 0 || moveVertical != 0)
        {
            Quaternion targetRotation = Quaternion.LookRotation(movement);
            playerModel.rotation = Quaternion.RotateTowards(playerModel.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

  
    }
}
