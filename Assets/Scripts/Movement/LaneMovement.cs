using UnityEngine;
using UnityEngine.Events;
using Digx7.Zygote;
using System.Collections;

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
    public float slideTime = 0.5f;
    public float slideCooldown = 0.5f;
    public float groundCheckDistance = 0.2f;
    public float fastFallSpeed = 5f;
    public LayerMask groundMask;

    public BooleanEvent OnGroundedEvent;
    public UnityEvent OnLandEvent;
    public UnityEvent OnFastFallEvent;
    public BooleanEvent OnSlideEvent;
    public UnityEvent OnSlideStartEvent;
    public UnityEvent OnSlideEndEvent;

    public BooleanEvent OnJumpEvent;

    public int currentLane = 1; // 0: left, 1: middle, 2: right
    Vector3 targetPosition;
    bool isChangingLane = false;
    bool isAirborne = false;
    Vector3 velocity;
    public bool IsGrounded()
    {
        

        bool output = Physics.Raycast(transform.position, Vector3.down, groundCheckDistance, groundMask);
        OnGroundedEvent?.Invoke(output);

        if(output && isAirborne)
        {
            OnLandEvent?.Invoke();
        }

        isAirborne = !output;
        return output;
    }
    float lastJumpTime = -1f;
    float lastSlideTime = -1f;
    bool _isSliding = false;
    bool _isFastFalling = false;
    public float GetGravity()
    {
        if(_isFastFalling)
        {
            return fastFallSpeed * gravity * Time.deltaTime;
        }
        else
        {
            return gravity * Time.deltaTime;
        }
    }
    public bool IsSliding
    {
        get
        {
            return _isSliding;
        }
        private set
        {
            if(value is bool)
            {
                _isSliding = value;
                OnSlideEvent?.Invoke(_isSliding);
                if(_isSliding)
                {
                    OnSlideStartEvent?.Invoke();
                }
                else
                {
                    OnSlideEndEvent?.Invoke();
                }
            }
        }
    }

    void Start()
    {
        targetPosition = transform.position;
    }

    void Update()
    {


        if (IsGrounded() && velocity.y < 0)
        {
            velocity.y = -2f; 
        }

        Vector3 moveVector = Vector3.forward * forwardSpeed * Time.deltaTime;

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

        // velocity.y += gravity * Time.deltaTime;
        velocity.y += GetGravity();
        moveVector.y = velocity.y * Time.deltaTime;

        controller.Move(moveVector);

        if (_isFastFalling && IsGrounded())
        {
            // Cleans up any fast fall
            _isFastFalling = false;
        }
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

        if(IsGrounded() && Time.time > lastJumpTime + jumpCooldown)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity); // Physics-based jump
            lastJumpTime = Time.time; // Record jump time
            OnJumpEvent?.Invoke(true);
        }
    }

    public void TrySlide()
    {
        if(IsGrounded())
        {
            // Slide
            if(Time.time > lastSlideTime + slideCooldown)
            {
                StartCoroutine(Slide());
            }
        }
        else
        {
            // TODO Fast fall
            velocity.y = 0; // Cancle out any upward velocity
            _isFastFalling = true;
            OnFastFallEvent?.Invoke();
        }
    }

    protected IEnumerator Slide()
    {
        IsSliding = true;
        yield return new WaitForSeconds(slideTime);
        IsSliding = false;
    }

    public void IncreaseSpeed() 
    {
        forwardSpeed = Mathf.Min(maxSpeed, forwardSpeed + 1);
    }
}