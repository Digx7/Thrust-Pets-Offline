using UnityEngine;

public class LaneMovement : MonoBehaviour 
{
    public CharacterController controller;
    public float forwardSpeed = 10f;
    public float laneDistance = 3f; // Distance between lanes
    public float laneChangeSpeed = 10f;
    public float minSpeed = 5f;
    public float maxSpeed = 20f;
    public float speedChangeAmount = 2f;
    public float gravity = -9.81f;
    public float jumpHeight = 2f;   
    public float jumpCooldown = 0.3f; 
    public float groundCheckDistance = 0.2f;
    public LayerMask groundMask;

    public int currentLane = 1; // 0: left, 1: middle, 2: right
    Vector3 targetPosition;
    bool isChangingLane = false;
    Vector3 velocity;
    bool isGrounded;
    float lastJumpTime = -1f;

    void Start()
    {
        targetPosition = transform.position;
    }

    void Update()
    {
        // if (hasAuthority == false) return;

        isGrounded = Physics.Raycast(transform.position, Vector3.down, groundCheckDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; 
        }

        Vector3 moveVector = Vector3.forward * forwardSpeed * Time.deltaTime;

        if (!isChangingLane)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                if (currentLane > 0)
                {
                    currentLane--;
                    isChangingLane = true;
                    targetPosition.x = (currentLane - 1) * laneDistance;
                }
            }
            else if (Input.GetKeyDown(KeyCode.D)) 
            {
                if (currentLane < 2)
                {
                    currentLane++;
                    isChangingLane = true;
                    targetPosition.x = (currentLane - 1) * laneDistance;
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            forwardSpeed = Mathf.Max(minSpeed, forwardSpeed - speedChangeAmount);
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            forwardSpeed = Mathf.Min(maxSpeed, forwardSpeed + speedChangeAmount);
        }

        // if (Input.GetKeyDown(KeyCode.Space) && isGrounded && Time.time > lastJumpTime + jumpCooldown)
        // {
        //     velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity); // Physics-based jump
        //     lastJumpTime = Time.time; // Record jump time
        // }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            TryJump();
        }

        if (isChangingLane)
        {
            float newX = Mathf.Lerp(transform.position.x, targetPosition.x, laneChangeSpeed * Time.deltaTime);
            moveVector.x = newX - transform.position.x;

            // Check if lane change is complete
            if (Mathf.Abs(transform.position.x - targetPosition.x) < 0.1f)
            {
                isChangingLane = false;
                moveVector.x = 0;
                transform.position = new Vector3(targetPosition.x, transform.position.y, transform.position.z);
            }
        }

        velocity.y += gravity * Time.deltaTime;
        moveVector.y = velocity.y * Time.deltaTime;

        controller.Move(moveVector);
    }

    public void TryJump()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, groundCheckDistance, groundMask);

        if(isGrounded && Time.time > lastJumpTime + jumpCooldown)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity); // Physics-based jump
            lastJumpTime = Time.time; // Record jump time
        }
    }

    public void IncreaseSpeed() 
    {
        forwardSpeed = Mathf.Min(maxSpeed, forwardSpeed + 1);
    }
}