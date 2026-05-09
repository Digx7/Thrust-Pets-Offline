using UnityEngine;
using UnityEngine.Events;
using Digx7.Zygote;

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

    public BooleanEvent OnGroundedEvent;

    public int currentLane = 1; // 0: left, 1: middle, 2: right
    Vector3 targetPosition;
    bool isChangingLane = false;
    Vector3 velocity;
    bool _isGrounded;
    public bool IsGrounded
    {
        get
        {
            return _isGrounded;
        }
        set
        {
            if(value is bool)
            {
                _isGrounded = value;
                OnGroundedEvent?.Invoke(_isGrounded);
            }
        }
    }
    float lastJumpTime = -1f;

    void Start()
    {
        targetPosition = transform.position;
    }

    void Update()
    {
        // if (hasAuthority == false) return;

        IsGrounded = Physics.Raycast(transform.position, Vector3.down, groundCheckDistance, groundMask);

        if (IsGrounded && velocity.y < 0)
        {
            velocity.y = -2f; 
        }

        Vector3 moveVector = Vector3.forward * forwardSpeed * Time.deltaTime;

        // if (!isChangingLane)
        // {
        //     if (Input.GetKeyDown(KeyCode.A))
        //     {
        //         if (currentLane > 0)
        //         {
        //             currentLane--;
        //             isChangingLane = true;
        //             targetPosition.x = (currentLane - 1) * laneDistance;
        //         }
        //     }
        //     else if (Input.GetKeyDown(KeyCode.D)) 
        //     {
        //         if (currentLane < 2)
        //         {
        //             currentLane++;
        //             isChangingLane = true;
        //             targetPosition.x = (currentLane - 1) * laneDistance;
        //         }
        //     }
        // }

        // if (Input.GetKeyDown(KeyCode.Q))
        // {
        //     forwardSpeed = Mathf.Max(minSpeed, forwardSpeed - speedChangeAmount);
        // }
        // else if (Input.GetKeyDown(KeyCode.E))
        // {
        //     forwardSpeed = Mathf.Min(maxSpeed, forwardSpeed + speedChangeAmount);
        // }

        // if (Input.GetKeyDown(KeyCode.Space) && isGrounded && Time.time > lastJumpTime + jumpCooldown)
        // {
        //     velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity); // Physics-based jump
        //     lastJumpTime = Time.time; // Record jump time
        // }

        // if (Input.GetKeyDown(KeyCode.Space))
        // {
        //     TryJump();
        // }

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

    public bool TryChangeLanes(float moveDirection)
    {
        if (!isChangingLane)
        {
            if (moveDirection < -0.1)
            {
                if (currentLane > 0)
                {
                    currentLane--;
                    isChangingLane = true;
                    targetPosition.x = (currentLane - 1) * laneDistance;
                    return true;
                }
            }
            else if (moveDirection > 0.1) 
            {
                if (currentLane < 2)
                {
                    currentLane++;
                    isChangingLane = true;
                    targetPosition.x = (currentLane - 1) * laneDistance;
                    return true;
                }
            }
        }

        return false;
    }

    public void TryJump()
    {
        IsGrounded = Physics.Raycast(transform.position, Vector3.down, groundCheckDistance, groundMask);

        if(IsGrounded && Time.time > lastJumpTime + jumpCooldown)
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