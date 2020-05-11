    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateUp : MonoBehaviour
{
    public Animator animator;
    public Animator animator2;

    void Start()
    {
        
    }
    public void OnCollisionEnter(Collision collision)
    {
        animator.SetBool("GO", true);
        animator2.SetBool("NextStep", true);
    }
    
    void Update()
    {
        
    }
}
