using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float jumpForce = 6f;
    public LayerMask groundLayer;
    public float raycastDistance;
    public float runningSpeed;
    public Transform rayOrigin;
    public Transform startPosition;

    const string JUMPING = "jumping";

    private Rigidbody2D _rigidBody;
    private AudioSource _audioMoneda;
    Animator _animator;
    
    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _audioMoneda = GetComponent<AudioSource>();
    }

    void Update()
    {
        Debug.DrawRay(rayOrigin.position, Vector2.down * raycastDistance, Color.red);
        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }

        if (isOnGround())
        {
            _animator.SetBool(JUMPING, false);
        }
        else
        {
            _animator.SetBool(JUMPING, true);
        }
        
    }
    private void FixedUpdate()
    {
        if(GameManager.sharedInstance.currentGameState == GameState.inGame)
        {
            if (_rigidBody.velocity.x < runningSpeed)
            {
                _rigidBody.velocity = new Vector2(runningSpeed, _rigidBody.velocity.y);
            }
        } else
        {
            _rigidBody.Sleep();
        }
    }

    public void StartGame()
    {
        this.transform.position = startPosition.position;
        this._rigidBody.velocity = Vector2.zero;
    }
    
    void Jump()
    {
        if(isOnGround())
        {
            _rigidBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }
    public void Die()
    {
        GameManager.sharedInstance.GameOver();
    }

    bool isOnGround()
    {
        return Physics2D.Raycast(rayOrigin.position, Vector2.down, raycastDistance, groundLayer);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Moneda"))
        {
            GameManager.sharedInstance.AumentarMonedas();
            _audioMoneda.Play();
            Destroy(other.gameObject);
        }
    }
}
