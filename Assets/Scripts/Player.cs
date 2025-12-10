using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(CharacterController))]

public class Player : MonoBehaviour
{
    public GameObject NEEDREST;

    private bool canUseHeadBob = true;
    public float walkingSpeed = 7.5f;
    public float runningSpeed = 11.5f;
    public float jumpHeight = 2.0f;
    public float gravity = -9.81f;
    public float acceleration = 20f;
    public float deacceleration = 20f;
    public Transform playerCamera;
    public float cameraSensitivity = 2.0f;
    public float maxCameraAngle = 90f;
    public float maxStamina = 100f;
    public float staminaDecreaseRate = 20f;
    public float staminaIncreaseRate = 10f;
    public Slider staminaBar;
    public ChristmasDudeAI christmasDude;

    CharacterController characterController;
    Vector3 moveDirection = Vector3.zero;
    public bool isRunning = false;
    public bool isMoving = false;
    bool isGrounded = false;
    float verticalVelocity = 0;
    float cameraRotationX = 0;
    public float stamina;
    public bool gameOver;
    public Canvas Screen;
    private float endTimer = 5f;
    private float timer;
    private bool timerStarted = false;

    [Header("headbob")]
    [SerializeField] private float walkBobSpeed = 14f;
    [SerializeField] private float walkBobAmount = 0.01f;
    [SerializeField] private float sprintBobSpeed = 18f;
    [SerializeField] private float sprintBobAmount = 0.03f;
    private float defaultYPos = 0;
    private float timer2;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        stamina = maxStamina;
        UpdateStaminaBar();
        defaultYPos = playerCamera.transform.localPosition.y;
    }

    void Update()
    {
            // Check if the player is grounded
    isGrounded = characterController.isGrounded;

    float targetSpeed = isRunning ? runningSpeed : walkingSpeed;
    float curSpeedX = targetSpeed * Input.GetAxisRaw("Vertical");
    float curSpeedY = targetSpeed * Input.GetAxisRaw("Horizontal");

    // Apply gravity
    if (isGrounded)
    {
        verticalVelocity = 0;
    }
    else
    {
        verticalVelocity += gravity * Time.deltaTime;
    }

    moveDirection.x = curSpeedY;
    moveDirection.z = curSpeedX;
    moveDirection.y = verticalVelocity;

    // Check if the player is moving
    isMoving = moveDirection.magnitude > 0.01f;

    // Move the controller
    characterController.Move(transform.TransformDirection(moveDirection) * Time.deltaTime * 1f);

        // Toggle running and update stamina
if (Input.GetKeyDown(KeyCode.LeftShift) && stamina > 0 && isMoving)
{
    isRunning = true;
}
if (Input.GetKeyUp(KeyCode.LeftShift) || stamina <= 0 || !isMoving)
{
    isRunning = false;
}
if (stamina == 0)
{
    NEEDREST.SetActive(true);
}
if (stamina >= 1)
{
    NEEDREST.SetActive(false);
}

// Update stamina
if (isRunning && isMoving)
{
    stamina -= staminaDecreaseRate * Time.deltaTime;
}
else if (!isRunning && !isMoving && stamina < maxStamina)
{
    stamina += staminaIncreaseRate * Time.deltaTime;
}

        stamina = Mathf.Clamp(stamina, 0, maxStamina);
        UpdateStaminaBar();

        // Rotate the player based on mouse input (horizontal rotation)
        transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * cameraSensitivity, 0);

        // Rotate the camera based on mouse input (vertical rotation)
        cameraRotationX -= Input.GetAxis("Mouse Y") * cameraSensitivity;
        cameraRotationX = Mathf.Clamp(cameraRotationX, -maxCameraAngle, maxCameraAngle);
        playerCamera.localEulerAngles = new Vector3(cameraRotationX, 0, 0);

        if (timerStarted)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                timerStarted = false; // Stop the timer
                SceneManager.LoadScene("NoWin");
            }
        }
        if (canUseHeadBob)
        {
            HandleHeadBob();
        }
    }

    private void HandleHeadBob()
    {
        if(!characterController.isGrounded) return;

        if(Mathf.Abs(moveDirection.x) > 0.1f || Mathf.Abs(moveDirection.z) > 0.1f)
        {
            timer2 += Time.deltaTime * (isRunning ? sprintBobSpeed : walkBobSpeed);
            playerCamera.transform.localPosition = new Vector3(playerCamera.transform.localPosition.x, defaultYPos + Mathf.Sin(timer2) * (isRunning ? sprintBobAmount : walkBobAmount), playerCamera.transform.localPosition.z);
        }
    }

    // Function to update the stamina bar
    void UpdateStaminaBar()
    {
        staminaBar.value = stamina;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ChristmasDudeAI")) // die
        {
            this.gameOver = true;
            base.StartCoroutine(this.KeepTheHudOff()); //Hides the Hud
        }
    }

    // Function to recharge stamina
    public void RechargeStamina(float amount)
    {
        stamina = Mathf.Clamp(stamina + amount, 0, maxStamina);
        UpdateStaminaBar();
    }
    public IEnumerator KeepTheHudOff()
    {
        for (int i = 0; i < 500; i++)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            this.Screen.enabled = false;
            walkingSpeed = 0;
            runningSpeed = 0;
            christmasDude.Jumpscared();
            RenderSettings.ambientLight = new Color((UnityEngine.Random.Range(0f, 1f)), (UnityEngine.Random.Range(0f, 1f)), (UnityEngine.Random.Range(0f, 1f)));
            StartTimer();
            yield return new WaitForEndOfFrame();
        }
        SceneManager.LoadSceneAsync("Title");
        yield break;
    }
    private void StartTimer()
    {
        timer = 5f; // Set the timer to 5 seconds
        timerStarted = true;
    }
}
