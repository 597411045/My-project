using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayMoveScript : MonoBehaviour
{
    public Animator animator;
    public NavMeshAgent navMeshAgent;
    public Camera secondCamera;
    public Vector3 BackToPlayer;
    public bool isAutoNav;
    public CinemachineFreeLook CFL;
    public NPCScript NearestNPC;

    Vector3 moveVector3;
    private void Awake()
    {
        animator = this.gameObject.GetComponent<Animator>();
        navMeshAgent = this.gameObject.GetComponent<NavMeshAgent>();
        //rigidbody = this.gameObject.GetComponent<Rigidbody>();
        secondCamera = GameObject.Find("SecondCamera").GetComponent<Camera>();
        CFL = GameObject.Find("CM FreeLook1").GetComponent<CinemachineFreeLook>();
    }

    void Start()
    {
        //navMeshAgent.updatePosition = false;
        //navMeshAgent.updateRotation = false;
        GameManagerInVillage.timers.Add(new PlayerMove(animator, navMeshAgent, secondCamera, this.transform));
        GameManagerInVillage.timers.Add(new PlayerAroundDected(this));
        GameManagerInVillage.timers.Add(new PlayerPressQAroundNPC(this));
        GameManagerInVillage.timers.Add(new PlayerTriggerCinemachine(CFL));
    }

    //void Update()
    //{
    //BackToPlayer = new Vector3(secondCamera.transform.position.x, this.transform.position.y, secondCamera.transform.position.z);
    //moveVector3 = Vector3.zero;
    //if (isAutoNav == false)
    //{
    //    animator.SetFloat("Speed", Mathf.Max(Mathf.Abs(Input.GetAxis("Vertical")), Mathf.Abs(Input.GetAxis("Horizontal"))) * 5);
    //    if (Input.GetAxis("Vertical") != 0)
    //    {
    //        moveVector3 = ((BackToPlayer - this.transform.position) * -Input.GetAxis("Vertical")).normalized;
    //        //navMeshAgent.velocity += ((BackToPlayer - this.transform.position) * -Input.GetAxis("Vertical")).normalized;
    //        this.transform.rotation = Quaternion.Lerp(this.transform.rotation, Quaternion.LookRotation((BackToPlayer - this.transform.position) * -Input.GetAxis("Vertical")), 0.1f);
    //    }

    //    if (Input.GetAxis("Horizontal") != 0)
    //    {
    //        moveVector3 += (Quaternion.AngleAxis(90, Vector3.up) * (BackToPlayer - this.transform.position) * -Input.GetAxis("Horizontal")).normalized;
    //        //navMeshAgent.velocity += (Quaternion.AngleAxis(90, Vector3.up) * (BackToPlayer - this.transform.position) * -Input.GetAxis("Horizontal")).normalized;
    //        this.transform.rotation = Quaternion.Lerp(this.transform.rotation, Quaternion.LookRotation(Quaternion.AngleAxis(90, Vector3.up) * (BackToPlayer - this.transform.position) * -Input.GetAxis("Horizontal")), 0.1f);
    //    }
    //    moveVector3 *= 3;
    //    navMeshAgent.velocity = moveVector3;
    //    if (Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") == 0)
    //    {
    //        navMeshAgent.velocity = Vector3.zero;
    //    }
    //}
    //else
    //{
    //    if (Vector3.Distance(this.transform.position, navMeshAgent.destination) < 1)
    //    {
    //        isAutoNav = false;
    //    }
    //}

    //navMeshAgent.nextPosition = this.transform.position;

    //Vector3 v = transform.InverseTransformDirection(navMeshAgent.velocity);
    //animator.SetFloat("Speed", v.z);
    //animator.SetFloat("Angle", v.x);
    //ani.SetFloat("Speed", nav.velocity.magnitude);
    //}

    //private void OnAnimatorMove()
    //{
    //    //Vector3 v = ani.rootPosition;
    //    //v.y = nav.nextPosition.y;
    //    //transform.position = v;
    //    navMeshAgent.nextPosition = transform.position;
    //    animator.ApplyBuiltinRootMotion();
    //}
}

public class PlayerMove : BaseUpdateAction
{
    Animator animator;
    NavMeshAgent navMeshAgent;
    Camera secondCamera;
    Vector3 BackToPlayer;
    bool isAutoNav;

    Vector3 moveVector3;
    Transform thistransform;

    public PlayerMove(Animator animator, NavMeshAgent navMeshAgent, Camera secondCamera, Transform thistransform) : base(1)
    {
        this.animator = animator;
        this.navMeshAgent = navMeshAgent;
        this.secondCamera = secondCamera;
        this.thistransform = thistransform;
        action += doAction;
    }

    public void doAction()
    {
        if (isStoped == false)
        {
            BackToPlayer = new Vector3(secondCamera.transform.position.x, thistransform.position.y, secondCamera.transform.position.z);
            moveVector3 = Vector3.zero;
            if (isAutoNav == false)
            {
                animator.SetFloat("Speed", Mathf.Max(Mathf.Abs(Input.GetAxis("Vertical")), Mathf.Abs(Input.GetAxis("Horizontal"))) * 5);
                if (Input.GetAxis("Vertical") != 0)
                {
                    moveVector3 = ((BackToPlayer - thistransform.position) * -Input.GetAxis("Vertical")).normalized;
                    //navMeshAgent.velocity += ((BackToPlayer - this.transform.position) * -Input.GetAxis("Vertical")).normalized;
                    thistransform.rotation = Quaternion.Lerp(thistransform.rotation, Quaternion.LookRotation((BackToPlayer - thistransform.position) * -Input.GetAxis("Vertical")), 0.1f);
                }

                if (Input.GetAxis("Horizontal") != 0)
                {
                    moveVector3 += (Quaternion.AngleAxis(90, Vector3.up) * (BackToPlayer - thistransform.position) * -Input.GetAxis("Horizontal")).normalized;
                    //navMeshAgent.velocity += (Quaternion.AngleAxis(90, Vector3.up) * (BackToPlayer - this.transform.position) * -Input.GetAxis("Horizontal")).normalized;
                    thistransform.rotation = Quaternion.Lerp(thistransform.rotation, Quaternion.LookRotation(Quaternion.AngleAxis(90, Vector3.up) * (BackToPlayer - thistransform.position) * -Input.GetAxis("Horizontal")), 0.1f);
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
                if (Vector3.Distance(thistransform.position, navMeshAgent.destination) < 1)
                {
                    isAutoNav = false;
                }
            }
        }
    }
}

public class PlayerAroundDected : BaseUpdateAction
{
    PlayMoveScript p;
    //Transform thistransform;
    Collider[] colliders;
    //NPCScript NearestNPC;
    public PlayerAroundDected(PlayMoveScript p) : base(1)
    {
        this.p = p;
        //this.thistransform = thistransform;
        //this.NearestNPC = NearestNPC;
        action += doAction;
    }

    public void doAction()
    {
        if (isStoped == false)
        {
            f += Time.deltaTime;
            if (f > 1)
            {
                colliders = Physics.OverlapSphere(p.transform.position, 5);
                if (colliders.Length == 0) PanelManagerInVillage.Instance?.ChangePanel(NameMap.PanelInteractive, null);
                for (int i = 0; i < colliders.Length; i++)
                {
                    if (colliders[i].name.Contains("NPC"))
                    {
                        PanelManagerInVillage.Instance.ChangePanel(null, NameMap.PanelInteractive, () =>
                        {
                            p.NearestNPC = colliders[i].GetComponent<NPCScript>();
                            PanelManagerInVillage.Instance.Panels[NameMap.PanelInteractive].Player = p.NearestNPC.view;
                        });
                        break;
                    }
                    else
                    {
                        PanelManagerInVillage.Instance.ChangePanel(NameMap.PanelInteractive, null);
                    }
                }
                f = 0;
            }
        }
    }
}

public class PlayerTriggerCinemachine : BaseUpdateAction
{
    CinemachineFreeLook CFL;
    float oldXMAX;
    float oldYMAX;
    public PlayerTriggerCinemachine(CinemachineFreeLook CFL) : base(1)
    {
        this.CFL = CFL;
        oldXMAX = CFL.m_XAxis.m_MaxSpeed;
        oldYMAX = CFL.m_YAxis.m_MaxSpeed;
        action += doAction;
    }

    public void doAction()
    {
        if (isStoped == false)
        {
            f += Time.deltaTime;
            if (f > 1)
            {
                foreach (var i in PanelManagerInVillage.Instance.Panels)
                {
                    if ((i.Value.state & (CustomPanelState.ifOpen | CustomPanelState.ifIgnore)) == CustomPanelState.ifOpen)
                    {
                        CFL.m_XAxis.m_MaxSpeed = 0;
                        CFL.m_YAxis.m_MaxSpeed = 0;
                        break;
                    }
                    else
                    {
                        if (CFL.m_XAxis.m_MaxSpeed != oldXMAX)
                            CFL.m_XAxis.m_MaxSpeed = oldXMAX;
                        if (CFL.m_YAxis.m_MaxSpeed != oldYMAX)
                            CFL.m_YAxis.m_MaxSpeed = oldYMAX;
                    }
                }
                f = 0;
            }
        }
    }
}

public class PlayerPressQAroundNPC : BaseUpdateAction
{
    PlayMoveScript p;
    //Transform thistransform;
    //NPCScript NearestNPC;
    public PlayerPressQAroundNPC(PlayMoveScript p) : base(1)
    {
        this.p = p;
        action += doAction;
    }

    public void doAction()
    {
        if (isStoped == false)
        {
            if ((PanelManagerInVillage.Instance.Panels[NameMap.PanelInteractive].state & CustomPanelState.ifOpen) == CustomPanelState.ifOpen
                && Input.GetKeyDown(KeyCode.Q))
            {
                foreach (var i in p.NearestNPC.module.Quests)
                {
                    NPCScript.NPCQuestUI.Clear();
                    NPCScript.NPCQuestUI.TryAddQuest(i.Value);
                }

                PanelManagerInVillage.Instance.ChangePanel(null, NameMap.PanelNPCQuest);
            }
        }
    }
}