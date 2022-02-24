using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody _rb;
    private Vector3 _velocity;
    private bool _hasJumped;
    private bool _hitWall;

    public float RunSpeed;
    public float MoveSpeed;
    public float JumpForce;
    public int SlowGround;
    public float TimerWall;
    
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        _velocity = new Vector3(0, _rb.velocity.y, RunSpeed);
        
        // Moving X
        if (Input.GetAxis("Vertical") > 0.1f || Input.GetAxis("Vertical") < 0.1f)
            _velocity.x = MoveSpeed * Input.GetAxis("Vertical");
        
        // Jump
        if (Input.GetKeyDown(KeyCode.Space) && !_hasJumped)
        {
            _rb.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
            _hasJumped = true;
        }

        // Wall
        if (_hitWall)
        {
            _velocity.z /= 3;
        }

        CheckGround();
    }
    
    public IEnumerator HitWall()
    {
        // Set Anim walk true
        _hitWall = true;
        yield return new WaitForSeconds(TimerWall);
        _hitWall = false;
        // Set Anim walk false
    }

    private void CheckGround()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down * 0.1f, out hit))
        {
            _hasJumped = false;

            if (hit.collider.gameObject.layer == SlowGround)
            {
                _velocity.z /= 2;
            }
        }
    }

    private void FixedUpdate()
    {
        _rb.velocity = _velocity;
    }
}
