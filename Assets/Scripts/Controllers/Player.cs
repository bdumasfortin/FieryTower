using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    private static Player _instance = null;
    public static Player Instance => _instance;

    [Header("Events")]
    public UnityEvent OnDied;

    [Header("Layer masks")]
    [SerializeField] private LayerMask _wallLayers;
    [SerializeField] private LayerMask _platformLayers;
    [SerializeField] private LayerMask _harmfulLayers;

    [Header("Colliders")]
    [SerializeField] private Collider2D _bottomCollider;
    [SerializeField] private Collider2D _topCollider;
    [SerializeField] private Collider2D _leftCollider;
    [SerializeField] private Collider2D _rightCollider;

    [Header("Movement options")]
    [SerializeField] private float _jumpHeight = 18.0f;
    [SerializeField] private float _moveForce = 50.0f;
    [SerializeField] private float _maxMoveSpeed = 10.0f;
    [SerializeField] private Vector2 _wallJumpModifier = new Vector2(0.8f, 1.4f);
    [SerializeField] private Vector2 _comboJumpModifier = new Vector2(0f, 1.4f);

    [Header("Modifiers")]
    [SerializeField] private bool _isSlippery = false;

    private bool IsGrounded => _bottomCollider.IsTouchingLayers(_platformLayers);
    private bool IsTouchingWall => LeftTouchingWall || RightTouchingWall;
    private bool LeftTouchingWall => _leftCollider.IsTouchingLayers(_wallLayers);
    private bool RightTouchingWall => _rightCollider.IsTouchingLayers(_wallLayers);

    private Rigidbody2D _rb;

    private void Awake()
    {
        _instance = this;
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        HandleJump();
    }

    private void FixedUpdate()
    {
        HandleMovement();
    }

    private void HandleJump()
    {
        if (Input.GetButtonDown("Jump") && CanJump())
            Jump();
    }

    private void HandleMovement()
    {
        if (Mathf.Abs(_rb.velocity.x) < _maxMoveSpeed)
            _rb.AddForce(new Vector2(GetHorizontalInput() * _moveForce, 0), ForceMode2D.Force);
    }

    private float GetHorizontalInput()
    {
        var input = Input.GetAxisRaw("Horizontal");
        return _isSlippery && (input < 0 && LeftTouchingWall) || (input > 0 && RightTouchingWall) ? 0 : input;
    }

    private bool CanJump()
    {
        return IsGrounded || IsTouchingWall && !_isSlippery;
    }

    private void Jump()
    {
        if (LeftTouchingWall || RightTouchingWall)
        {
            _rb.AddForce(new Vector2((LeftTouchingWall ? 1 : -1) * _jumpHeight * _wallJumpModifier.x, _jumpHeight * _wallJumpModifier.y), ForceMode2D.Impulse);
            ComboHandler.IncrementStreak();
        }
        else if (IsGrounded && ComboHandler.IsComboActive)
            _rb.AddForce(new Vector2(1 * _comboJumpModifier.x, _jumpHeight * _comboJumpModifier.y), ForceMode2D.Impulse);
        else if (IsGrounded)
            _rb.AddForce(new Vector2(0, _jumpHeight), ForceMode2D.Impulse);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // If player touches a harmful object, he dies
        if (_harmfulLayers == (_harmfulLayers | (1 << collision.gameObject.layer)))
            Kill();
    }

    private void Kill()
    {
        OnDied?.Invoke();
    }
}
