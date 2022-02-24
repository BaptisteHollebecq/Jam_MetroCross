using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody _rb;
    private Animator _anim;
    private Vector3 _velocity;
    private bool _hasJumped;
    private bool _hitWall;
    private bool _slowed;

    public float RunSpeed;
    public float MoveSpeed;
    public float JumpForce;
    public int SlowGround;
    public float TimerWall;

    public bool OnSkate = false;
    public GameObject Skate;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _anim  = GetComponent<Animator>();
        
        _anim.SetBool("Go",true);
    }

    void Update()
    {
        if (!Game.Instance.Playing)
        {
            _velocity = Vector3.zero;
            return;
        }
        _velocity = new Vector3(0, _rb.velocity.y, RunSpeed);

        if(_slowed) _velocity.z /= 2;
            
        // Moving X
        if (Input.GetAxis("Vertical") > 0.1f || Input.GetAxis("Vertical") < 0.1f)
            _velocity.x = MoveSpeed * Input.GetAxis("Vertical") * -1;
        
        // Jump
        if (Input.GetKeyDown(KeyCode.Space) && !_hasJumped)
        {
            if (OnSkate)
                GetOffSkate();
            _rb.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
            _hasJumped = true;
        }

        // Wall
        if (_hitWall)
        {
            _velocity.z /= 3;
            
        }

        if (OnSkate)
            _velocity.y = 0;
    }
    
    private void FixedUpdate()
    {
        _rb.velocity = _velocity;
    }
    
    public IEnumerator HitWall()
    {
        if (OnSkate)
            GetOffSkate();
        _anim.SetFloat("Blend",1);
        _hitWall = true;
        yield return new WaitForSeconds(TimerWall);
        _hitWall = false;
        _anim.SetFloat("Blend",0);
    }

    public void GetSkate()
    {
        OnSkate = true;
        _anim.SetBool("OnSkate", true);
        Skate.SetActive(true);
    }

    private void GetOffSkate()
    {
        OnSkate = false;
        _anim.SetBool("OnSkate", false);
        Skate.SetActive(false);
    }

    public void OnFootTriggerStay(Collider ground)
    {
        _hasJumped = false;
        _velocity.y = 0;

        if (!OnSkate)
            _slowed = ground.gameObject.layer == SlowGround;
        
    }
    
}
