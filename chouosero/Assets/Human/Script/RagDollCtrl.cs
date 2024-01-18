using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagDollCtrl : MonoBehaviour
{
    Animator animator;
    Rigidbody rb;
    public bool RagDoll = false;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(RagDoll==rb.isKinematic)
        {
            rb.isKinematic = !RagDoll;
            animator.enabled = !RagDoll;
        }
    }
   
}
