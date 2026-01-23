using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float _maxSpeed = 5;

    Animator _animator;
    Rigidbody2D _rigidbody;

    bool facingRight;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();

        facingRight = true;
    }

    private void FixedUpdate()
    {
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
