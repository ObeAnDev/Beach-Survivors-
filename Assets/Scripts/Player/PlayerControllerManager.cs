using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerControllerManager : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] public float Speed = 7f;

    [Header("Constraints")]
    [SerializeField] private float minZ = -7.5f;
    [SerializeField] private float maxZ = 7.5f;

    [Header("References")]
    [SerializeField] private Transform playerModel;

    private Rigidbody rb;
    private Vector3 moveInput;
    private Camera mainCamera;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        mainCamera = Camera.main;


        rb.freezeRotation = true;
    }

    void Update()
    {
        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        float moveVertical = Input.GetAxisRaw("Vertical");

        moveInput = new Vector3(moveHorizontal, 0f, moveVertical).normalized;
    }

    void FixedUpdate()
    {
        MovePlayer();

        LookAtMouse();
    }

    void MovePlayer()
    {
        Vector3 velocity = new Vector3(moveInput.x * Speed, rb.velocity.y, moveInput.z * Speed);
        rb.velocity = velocity;

        if (transform.position.z < minZ || transform.position.z > maxZ)
        {
            float clampedZ = Mathf.Clamp(transform.position.z, minZ, maxZ);
            rb.position = new Vector3(transform.position.x, transform.position.y, clampedZ);
        }
    }

    void LookAtMouse()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        Plane groundPlane = new Plane(Vector3.up, transform.position);

        float rayDistance;

        if (groundPlane.Raycast(ray, out rayDistance))
        {
            Vector3 lookPoint = ray.GetPoint(rayDistance);

            Vector3 lookDirection = new Vector3(lookPoint.x, transform.position.y, lookPoint.z) - transform.position;

            if (lookDirection != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(lookDirection);

                transform.rotation = targetRotation;
            }
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