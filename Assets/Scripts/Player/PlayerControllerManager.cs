 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class PlayerControllerManager : MonoBehaviour
{
    [SerializeField] float speed;
    public float Speed { get { return speed; } set { speed = value; } }

    [SerializeField] float rotationSpeed;
    public float RotationSpeed => rotationSpeed;

    Rigidbody rb;

    public Transform playerModel;


    public float moveSpeed = 7f;

    public float minZ = -7.5f;
    public float maxZ = 7.5f;

    private Vector3 moveDirection;


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
        MovePlayer();
    }
    void MovePlayer()
    {
        Vector3 targetPosition = transform.position + moveDirection * moveSpeed * Time.deltaTime;

        targetPosition.z = Mathf.Clamp(targetPosition.z, minZ, maxZ);

        transform.position = targetPosition;

        if (moveDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 15f * Time.deltaTime);
        }
    }
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<ItemLoot>(out ItemLoot item))
        {
            item.OnPickUp(gameObject);
        }
    }
}