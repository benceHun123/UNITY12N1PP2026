using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float _maxSpeed = 5;
    [SerializeField] float _jumpHeight = 500;

    [SerializeField] float _groundCheckRadius = .1f;
    
    [SerializeField] LayerMask _groundLayer;
    [SerializeField] Transform _groundCheker;

    Animator _animator;
    Rigidbody2D _rigidbody;

    bool facingRight;
    private bool isGrounded;

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
    }

    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(_groundCheker.position, _groundCheckRadius, _groundLayer);
        _animator.SetBool("isGrounded", isGrounded);
        _animator.SetFloat("verticalSpeed", _rigidbody.linearVelocity.y);


        float move = Input.GetAxis("Horizontal");

        _animator.SetFloat("horizontalSpeed",Mathf.Abs(move));

        _rigidbody.linearVelocity = new(move * _maxSpeed, _rigidbody.linearVelocity.y);

        if (move > 0 && !facingRight) FlipPlayer();
        else if (move<0 && facingRight) FlipPlayer();
    }

    private void FlipPlayer()
    {
        facingRight= !facingRight;
        Vector3 playerScale = transform.localScale;
        playerScale.x *= -1;
        transform.localScale = playerScale;
    }

    
}
