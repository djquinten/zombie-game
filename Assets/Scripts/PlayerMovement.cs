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

    private CharacterController _controller;
    private Vector3 _velocity;
    private float _xRotation = 0f;
    private bool _isShooting = false;

    private Vector3 _slideStartPosition;
    private float _slideTime;

    private GameManager _gameManager;

    private void Start()
    {
        _controller = GetComponent<CharacterController>();
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        _slideStartPosition = slide.localPosition;
    }

    private void Update()
    {
        if (!_gameManager.isGameStarted)
        {
            return;
        }

        // Movement
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        _controller.Move(move * speed * Time.deltaTime);

        // Gravity
        if (_controller.isGrounded && _velocity.y < 0)
        {
            _velocity.y = -2f;
        }
        _velocity.y += gravity * Time.deltaTime;
        _controller.Move(_velocity * Time.deltaTime);

        // Mouse look
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        _xRotation -= mouseY;
        _xRotation = Mathf.Clamp(_xRotation, -90f, 90f); // Limit vertical rotation to prevent flipping

        playerCameraHolder.localRotation = Quaternion.Euler(_xRotation, 0f, 0f); // Move camera on X axis
        transform.Rotate(Vector3.up * mouseX); // Rotate player on Y axis

        // Shooting
        if (Input.GetButtonDown("Fire1") && !_isShooting)
        {
            Shoot();
        }

        // Handle weapon slide back and forth animation when shooting
        if (_isShooting)
        {
            _slideTime += Time.deltaTime;

            // Move the slide back
            if (_slideTime < slideSpeed)
            {
                slide.localPosition = Vector3.Lerp(_slideStartPosition, _slideStartPosition - new Vector3(0, 0, slideBackDistance), _slideTime / slideSpeed);
            }
            // Move the slide forward
            else if (_slideTime < slideSpeed * 2)
            {
                slide.localPosition = Vector3.Lerp(_slideStartPosition - new Vector3(0, 0, slideBackDistance), _slideStartPosition, (_slideTime - slideSpeed) / slideSpeed);
            }
            // Reset the slide position
            else
            {
                slide.localPosition = _slideStartPosition;
                _isShooting = false;
            }
        }
    }

    private void Shoot()
    {
        _isShooting = true;
        _slideTime = 0;

        Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
    }
}
