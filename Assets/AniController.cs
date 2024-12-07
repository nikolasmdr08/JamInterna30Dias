using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AniController : MonoBehaviour
{

    public Rigidbody rb;
    public bool isWalking;
    public bool isDead;
    private Animator _animator;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponentInParent<Rigidbody>();
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (rb.velocity.magnitude > 0)
        {
            isWalking = true;
        }
        else 
        {
            isWalking = false;
        }

        _animator.SetBool("isWalking", isWalking);

        if (isDead == true) 
        {
            _animator.SetBool("isDead", true);
        }

    }
}
