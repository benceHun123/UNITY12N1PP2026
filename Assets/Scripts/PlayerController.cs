using System;
using Unity.VisualScripting;
using UnityEditor.Tilemaps;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float _maxSpeed = 5;
    [SerializeField] float _jumpHeight = 500;

    [SerializeField] float _groundCheckRadius = .1f;

    [SerializeField] LayerMask _groundLayer;
    [SerializeField] Transform _groundChecker;

    private Animator _animator;
    private Rigidbody2D _rigidbody;

    private bool facingRight;
    private bool isGrounded;

    //shooting variables
    [SerializeField] Transform _gunTip;
    [SerializeField] GameObject _projectilePrefab;
    [SerializeField] float _fireRate = 0.5f;

    private float _nextFireTime = 0f;


    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();

        facingRight = true;
        isGrounded = false;
    }

    private void Update()
    {
        if (isGrounded && Input.GetAxis("Jump") > 0)
        {
            _animator.SetBool("isGrounded", false);
            isGrounded = false;
            _rigidbody.AddForce(new Vector2(0, _jumpHeight));
        }

        //shooting
        if(Input.GetAxisRaw("Fire1") >0)
        {
            if(Time.time >= _nextFireTime)
            {
                _nextFireTime = Time.time + _fireRate;
                if (facingRight)
                {
                    Instantiate(_projectilePrefab, _gunTip.position, Quaternion.identity);
                }
                else
                {
                    Instantiate(_projectilePrefab, _gunTip.position, Quaternion.Euler(0, 0, 180f));
                }
            }
        }
    }

    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(
            _groundChecker.position,
            _groundCheckRadius,
            _groundLayer);
        _animator.SetBool("isGrounded", isGrounded);
        _animator.SetFloat("verticalSpeed", _rigidbody.linearVelocity.y);


        float move = Input.GetAxis("Horizontal");

        _animator.SetFloat("horizontalSpeed", Mathf.Abs(move));

        _rigidbody.linearVelocity = new(
            move * _maxSpeed,
            _rigidbody.linearVelocity.y);

        if (move > 0 && !facingRight) FlipPlayer();
        else if (move < 0 && facingRight) FlipPlayer();

}
    private void FlipPlayer()
    {
        facingRight = !facingRight;
        Vector3 playerScale = transform.localScale;
        playerScale.x *= -1;
        transform.localScale = playerScale;
    }
}
