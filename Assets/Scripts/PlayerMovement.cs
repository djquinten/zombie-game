using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 6.0f;
    public float gravity = -9.81f;
    public float mouseSensitivity = 100.0f;
    public Transform playerCameraHolder;
    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;

    public Transform slide;
    public float slideBackDistance = -0.1f;
    public float slideSpeed = 0.1f;

    private CharacterController controller;
    private Vector3 velocity;
    private float xRotation = 0f;
    private bool isShooting = false;

    private Vector3 slideStartPosition;
    private float slideTime;

    private GameManager gameManager;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        slideStartPosition = slide.localPosition;
    }

    void Update()
    {
        if (!gameManager.isGameStarted)
        {
            return;
        }

        // Movement
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);

        // Gravity
        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        // Mouse look
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        playerCameraHolder.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);

        // Shooting
        if (Input.GetButtonDown("Fire1") && !isShooting)
        {
            Shoot();
        }

        // Slide the weapon back and forth when shooting
        if (isShooting)
        {
            slideTime += Time.deltaTime;

            // Move the slide back and forth
            if (slideTime < slideSpeed)
            {
                slide.localPosition = Vector3.Lerp(slideStartPosition, slideStartPosition - new Vector3(0, 0, slideBackDistance), slideTime / slideSpeed);
            }
            else if (slideTime < slideSpeed * 2)
            {
                slide.localPosition = Vector3.Lerp(slideStartPosition - new Vector3(0, 0, slideBackDistance), slideStartPosition, (slideTime - slideSpeed) / slideSpeed);
            }
            else
            {
                slide.localPosition = slideStartPosition;
                isShooting = false;
            }
        }
    }

    void Shoot()
    {
        isShooting = true;
        slideTime = 0;

        Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
    }
}
