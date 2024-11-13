using System;
using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public PlayerDataWithDash Data;

    public Rigidbody2D RB { get; private set;}

    public bool IsFacingRight { get; private set; }
    public bool IsJumping { get; private set; }
    public bool IsDashing { get; private set; }

    public float LastOnGroundTime { get; private set; }

    private bool _isJumpCut;
    private bool _isJumpFalling;


    private int _dashesLeft;
    private bool _dashRefilling;
    private Vector2 _lastDashDir;
    private bool _isDashAttacking;
    private int _dashesRight;

    private Vector2 _moveInput;

    public float LastPressedJumpTime { get; private set; }
    public float LastPressedDashTime { get; private set; }
    public object StartDash { get; private set; }

    private Transform _groundCheckPoint;

    private Vector2 _groundCheckSize = new Vector2(0.49f, 0.03f);
    private Transform _frontWallCheckPoint;
    private Transform _backWallCheckPoint;
    private Vector2 _wallCheckSize = new Vector2(0.5f, 1f);
    private Vector2 _wallCheckSize2 = new Vector2(1f, 1f);


    private LayerMask _groundLayer;

    //public float gravityStrength;
    //public float gravityScale;

    //public float fallGravityMult;
    //public float maxFallSpeed;

    //public float fastFallGravityMult;
    //public float maxFastFallSpeed;

    //public float runMaxSpeed;
    //public float runAcceleration;
    //public float runAccelAmount;
    //public float runDecceleration;
    //public float runDeccelAmount;

    //public float accelInAir;
    //public float deccelInAir;
    //public bool doConserveMomentum = true;

    //public float jumpHeight;
    //public float jumpTimeToApex;
    //public float jumpForce;


    //public float jumpCutGravityMult;
    //public float jumpHangGravityMult;
    //public float jumpHangTimeThreshold;
    //public float jumpHangAccelerationMult;
    //public float jumpHangMaxSpeedMult;

    //public float coyoteTime;
    //public float jumpInputBufferTime;

    //public int dashAmount;
    //public float dashSpeed;
    //public float dashSleepTime;
    //public float dashAttackTime;
    //public float dashEndTime;
    //public Vector2 dashEndSpeed;
    //public float dashEndRunLerp;
    //public float dashRefillTime;
    //public float dashInputBufferTime;





    private void Awake()
    {
        RB = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        SetGravityScale(Data.gravityScale);
        IsFacingRight = true;
    }

    private void Update()
    {
        LastOnGroundTime -= Time.deltaTime;

        LastPressedJumpTime -= Time.deltaTime;
        LastPressedDashTime -= Time.deltaTime;



        _moveInput.x = Input.GetAxisRaw("Horizontal");
        _moveInput.y = Input.GetAxisRaw("Vertical");

        if (_moveInput.x != 0)
            CheckDirectionToFace(_moveInput.x > 0);

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.C) || Input.GetKeyDown(KeyCode.J))
        {
            OnJumpInput();
        }

        if (Input.GetKeyUp(KeyCode.Space) || Input.GetKeyUp(KeyCode.C) || Input.GetKeyUp(KeyCode.J))
        {
            OnJumpUpInput();
        }

        if (Input.GetKeyDown(KeyCode.X) || Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.K))
        {
            OnDashInput();
        }



        if (!IsDashing && !IsJumping)
        {
            if (Physics2D.OverlapBox(_groundCheckPoint.position, _groundCheckSize, 0, _groundLayer) && !IsJumping)
            {
                LastOnGroundTime = Data.coyoteTime;
            }
        }


        if (LastOnGroundTime > 0 && !IsJumping)
        {
            _isJumpCut = false;

            if (!IsJumping)
                _isJumpFalling = false;
        }


        if (!IsDashing)
        {
            if (CanJump() && LastPressedJumpTime > 0)
            {
                IsJumping = true;
                _isJumpCut = false;
                _isJumpFalling = false;
                Jump();
            }


        }

        if (!_isDashAttacking)
        {
            if (RB.velocity.y < 0 && _moveInput.y < 0)
            {
                SetGravityScale(Data.gravityScale * Data.fastFallGravityMult);
                RB.velocity = new Vector2(RB.velocity.x, Mathf.Max(RB.velocity.y, -Data.maxFastFallSpeed));
            }
            else if (_isJumpCut)
            {
                SetGravityScale(Data.gravityScale * Data.jumpCutGravityMult);
                RB.velocity = new Vector2(RB.velocity.x, Mathf.Max(RB.velocity.y, -Data.maxFallSpeed));
            }
            else if ((IsJumping || _isJumpFalling) && Mathf.Abs(RB.velocity.y) < Data.jumpHangTimeThreshold)
            {
                SetGravityScale(Data.gravityScale * Data.jumpHangGravityMult);
            }
            else if (RB.velocity.y < 0)
            {
                SetGravityScale(Data.gravityScale * Data.fallGravityMult);
                RB.velocity = new Vector2(RB.velocity.x, Mathf.Max(RB.velocity.y, -Data.maxFallSpeed));
            }
            else
            {
                SetGravityScale(Data.gravityScale);
            }
        }
        else
        {
            SetGravityScale(0);
        }

        if (CanDash() && LastPressedDashTime > 0)
        {
            Sleep(Data.dashSleepTime);

            if (_moveInput != Vector2.zero)
                _lastDashDir = _moveInput;
            else
                _lastDashDir = IsFacingRight ? Vector2.right : Vector2.left;



            IsDashing = true;
            IsJumping = false;
            _isJumpCut = false;

            StartCoroutine(nameof(StartDash), _lastDashDir);
        }



    }


    public void OnJumpInput()
    {
        LastPressedJumpTime = Data.jumpInputBufferTime;
    }

    public void OnJumpUpInput()
    {
        if (CanJumpCut())
            _isJumpCut = true;
    }

    public void OnDashInput()
    {
        LastPressedDashTime = Data.dashInputBufferTime;
    }

    private void Jump()
    {
        LastPressedJumpTime = 0;
        LastOnGroundTime = 0;

        float force = Data.jumpForce;
        if (RB.velocity.y < 0)
            force -= RB.velocity.y;

        RB.AddForce(Vector2.up * Data.jumpForce, ForceMode2D.Impulse);
    }

    public void CheckDirectionToFace(bool isMovingRight)
    {
        if (isMovingRight != IsFacingRight)
            return;
    }

    private bool CanJump()
    {
        return LastOnGroundTime > 0 && !IsJumping;
    }

    private bool CanJumpCut()
    {
        return IsJumping && RB.velocity.y > 0;
    }

    private bool CanDash()
    {
        if (!IsDashing && _dashesLeft < Data.dashAmount && LastOnGroundTime > 0 && !_dashRefilling)
        {
            StartCoroutine(nameof(RefillDash), 1);
        }

        return _dashesLeft > 0;
    }

    private void OnValidate()
    {
        Data.gravityStrength = -(2 * Data.jumpHeight) / (Data.jumpTimeToApex * Data.jumpTimeToApex);

        Data.gravityScale = Data.gravityStrength / Physics2D.gravity.y;

        Data.runAccelAmount = (50 * Data.runAcceleration) / Data.runMaxSpeed;
        Data.runDeccelAmount = (50 * Data.runDecceleration) / Data.runMaxSpeed;

        Data.jumpForce = Mathf.Abs(Data.gravityStrength) * Data.jumpTimeToApex;

        Data.runAcceleration = Mathf.Clamp(Data.runAcceleration, 0.01f, Data.runMaxSpeed);
        Data.runDecceleration = Mathf.Clamp(Data.runDecceleration, 0.01f, Data.runMaxSpeed);
    }

    public void SetGravityScale(float scale)
    {
        RB.gravityScale = scale;
    }

    private void Sleep(float duration)
    {
        StartCoroutine(nameof(PerformSleep), duration);
    }

    private IEnumerator PerformSleep(float duration)
    {
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(duration);
        Time.timeScale = 1;
    }

    private IEnumerator RefillDash(int amount)
    {
        _dashRefilling = true;
        yield return new WaitForSeconds(Data.dashRefillTime);
        _dashRefilling = false;
        _dashesLeft = Mathf.Min(Data.dashAmount, _dashesLeft + 1);
    }


}