using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;


[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{

    Rigidbody2D rb;
    Animator animator;

    public float walkSpeed = 5f;
    public float runSpeed = 35f;
    Vector2 moveInput;

    
    public bool _isFacingRight = true;

    public bool IsFacingRight { get {
        return _isFacingRight;
    } private set {
        _isFacingRight = value;
        transform.localScale = new Vector2(value ? 9 : -9, 10);
    } }

    private void setDirection(Vector2 direction) {
        if (direction.x > 0 && !IsFacingRight) {
            IsFacingRight = true;
        } else if (direction.x < 0 && IsFacingRight) {
            IsFacingRight = false;
        }
    }

    public float CurrentSpeed { get {if (IsMoving) {
        return IsRunning ? runSpeed : walkSpeed;
    } else {
        return 0;
    }} private set {
        if (IsMoving) {
            if (IsRunning) {
                runSpeed = value;
            } else {
                walkSpeed = value;
            }
        }
    } }

    [SerializeField]
    private bool _isMoving = false;

    public bool IsMoving { get 
        {
            return _isMoving;
        } 
        private set 
        {
            _isMoving = value;
            animator.SetBool(AnimationStrings.isMoving, value);
        } 
    }
 
    [SerializeField]
    private bool _isRunning = false;

    public bool IsRunning { get 
        {
            return _isRunning;
        } 
        private set 
        {
            _isRunning = value;
            animator.SetBool(AnimationStrings.isRunning, value);
        } 
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(moveInput.x * CurrentSpeed, rb.velocity.y);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        IsMoving = moveInput != Vector2.zero;

        setDirection(moveInput);
    }

    public void OnRun(InputAction.CallbackContext context)
    {
        if (context.started) {
            IsRunning = true;
        } else if (context.canceled) {
            IsRunning = false;
        }
    }
}
