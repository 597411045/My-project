using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayMoveScript : MonoBehaviour
{
    Animator animator;
    public NavMeshAgent navMeshAgent;
    Camera secondCamera;
    Vector3 BackToPlayer;
    public bool isAutoNav;

    Vector3 moveVector3;
    private void Awake()
    {
        animator = this.gameObject.GetComponent<Animator>();
        navMeshAgent = this.gameObject.GetComponent<NavMeshAgent>();
        //rigidbody = this.gameObject.GetComponent<Rigidbody>();
        secondCamera = GameObject.Find("SecondCamera").GetComponent<Camera>();
    }

    void Start()
    {
        //navMeshAgent.updatePosition = false;
        //navMeshAgent.updateRotation = false;

    }

    // Update is called once per frame
    void Update()
    {
        BackToPlayer = new Vector3(secondCamera.transform.position.x, this.transform.position.y, secondCamera.transform.position.z);
        moveVector3 = Vector3.zero;
        if (isAutoNav == false)
        {
            animator.SetFloat("Speed", Mathf.Max(Mathf.Abs(Input.GetAxis("Vertical")), Mathf.Abs(Input.GetAxis("Horizontal"))) * 5);
            if (Input.GetAxis("Vertical") != 0)
            {
                moveVector3 = ((BackToPlayer - this.transform.position) * -Input.GetAxis("Vertical")).normalized;
                //navMeshAgent.velocity += ((BackToPlayer - this.transform.position) * -Input.GetAxis("Vertical")).normalized;
                this.transform.rotation = Quaternion.Lerp(this.transform.rotation, Quaternion.LookRotation((BackToPlayer - this.transform.position) * -Input.GetAxis("Vertical")), 0.1f);
            }

            if (Input.GetAxis("Horizontal") != 0)
            {
                moveVector3 += (Quaternion.AngleAxis(90, Vector3.up) * (BackToPlayer - this.transform.position) * -Input.GetAxis("Horizontal")).normalized;
                //navMeshAgent.velocity += (Quaternion.AngleAxis(90, Vector3.up) * (BackToPlayer - this.transform.position) * -Input.GetAxis("Horizontal")).normalized;
                this.transform.rotation = Quaternion.Lerp(this.transform.rotation, Quaternion.LookRotation(Quaternion.AngleAxis(90, Vector3.up) * (BackToPlayer - this.transform.position) * -Input.GetAxis("Horizontal")), 0.1f);
            }
            moveVector3 *= 3;
            navMeshAgent.velocity = moveVector3;
            if (Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") == 0)
            {
                navMeshAgent.velocity = Vector3.zero;
            }
        }
        else
        {
            if (Vector3.Distance(this.transform.position, navMeshAgent.destination) < 1)
            {
                isAutoNav = false;
            }
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
