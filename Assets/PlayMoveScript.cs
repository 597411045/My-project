using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayMoveScript : MonoBehaviour
{
    Animator animator;
    NavMeshAgent navMeshAgent;
    //Rigidbody rigidbody;

    private void Awake()
    {
        animator = this.gameObject.GetComponent<Animator>();
        navMeshAgent = this.gameObject.GetComponent<NavMeshAgent>();
        //rigidbody = this.gameObject.GetComponent<Rigidbody>();
    }

    void Start()
    {
        //navMeshAgent.updatePosition = false;
        navMeshAgent.updateRotation = false;

    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("Speed", Input.GetAxis("Vertical") * 5);
        this.transform.Rotate(new Vector3(0, Input.GetAxis("Horizontal") * 2, 0));
        if (Input.GetAxis("Vertical") != 0)
        {
            navMeshAgent.velocity = this.transform.rotation * new Vector3(0, 0, Input.GetAxis("Vertical") * 5);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            navMeshAgent.velocity += new Vector3(0, 10, 0);
        }
        //navMeshAgent.nextPosition = this.transform.position;

        //Vector3 v = transform.InverseTransformDirection(navMeshAgent.velocity);
        //animator.SetFloat("Speed", v.z);
        //animator.SetFloat("Angle", v.x);
        //ani.SetFloat("Speed", nav.velocity.magnitude);


    }

    //private void OnAnimatorMove()
    //{
    //    //Vector3 v = ani.rootPosition;
    //    //v.y = nav.nextPosition.y;
    //    //transform.position = v;
    //    navMeshAgent.nextPosition = transform.position;
    //    animator.ApplyBuiltinRootMotion();
    //}
}
