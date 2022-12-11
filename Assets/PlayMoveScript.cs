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
    public GameObject BigExplosion;

    Vector3 moveVector3;
    private void Awake()
    {
        animator = this.gameObject.GetComponent<Animator>();
        navMeshAgent = this.gameObject.GetComponent<NavMeshAgent>();
        //rigidbody = this.gameObject.GetComponent<Rigidbody>();
        secondCamera = GameObject.Find("SecondCamera").GetComponent<Camera>();
        CFL = GameObject.Find("CM FreeLook1").GetComponent<CinemachineFreeLook>();
        BigExplosion = MyUtil.FindOneInChildren(this.transform, "BigExplosion").gameObject;
        BigExplosion.SetActive(false);
    }

    void Start()
    {
        //navMeshAgent.updatePosition = false;
        navMeshAgent.updateRotation = false;
        GameManagerInVillage.timers.Add(new NewPlayerMove(navMeshAgent, secondCamera, this.transform));
        GameManagerInVillage.timers.Add(new PlayerAroundDected(this));
        GameManagerInVillage.timers.Add(new PlayerPressKeyAroundNPC(this));
        GameManagerInVillage.timers.Add(new PlayerTriggerCinemachine(CFL));
        GameManagerInVillage.timers.Add(new PlayerCheckStatus(this));
        GameManagerInVillage.timers.Add(new AnimatorObverser(this));
    }

    //private void OnAnimatorMove()
    //{
    //    //Vector3 v = ani.rootPosition;
    //    //v.y = nav.nextPosition.y;
    //    //transform.position = v;
    //    navMeshAgent.nextPosition = transform.position;
    //    animator.ApplyBuiltinRootMotion();
    //    //transform.position = navMeshAgent.nextPosition;
    //}
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
                colliders = Physics.OverlapSphere(p.transform.position, 3);
                if (colliders.Length == 0) PanelManagerInVillage.Instance?.ChangePanel(NameMap.PanelInteractive, null);
                for (int i = 0; i < colliders.Length; i++)
                {
                    if (colliders[i].name.Contains("NPC"))
                    {
                        PanelManagerInVillage.Instance.ChangePanel(null, NameMap.PanelInteractive, () =>
                        {
                            p.NearestNPC = colliders[i].GetComponent<NPCScript>();
                            PanelManagerInVillage.Instance.Panels[NameMap.PanelInteractive].SetPlayer(p.NearestNPC.view);
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

public class PlayerPressKeyAroundNPC : BaseUpdateAction
{
    PlayMoveScript p;
    //Transform thistransform;
    //NPCScript NearestNPC;
    public PlayerPressKeyAroundNPC(PlayMoveScript p) : base(1)
    {
        this.p = p;
        action += doAction;
    }

    public void doAction()
    {
        if (isStoped == false)
        {
            if ((PanelManagerInVillage.Instance.Panels[NameMap.PanelInteractive].state & CustomPanelState.ifOpen) == CustomPanelState.ifOpen
                )
            {
                if (Input.GetKeyDown(KeyCode.Q))
                {
                    NPCScript.NPCQuestUI.ClearQuest();
                    foreach (var i in p.NearestNPC.module.Quests)
                    {
                        NPCScript.NPCQuestUI.TryAddQuest(i.Value);
                    }
                    PanelManagerInVillage.Instance.ChangePanel(null, NameMap.PanelNPCQuest);
                }
                if (Input.GetKeyDown(KeyCode.E))
                {
                    PanelManagerInVillage.Instance.ChangePanel(null, NameMap.PanelDialogue, () =>
                    {
                        p.NearestNPC.view.c.module.SetDialogueIndex(p.NearestNPC.view.c.module.dialogueIndex);
                    });
                }
            }
        }
    }
}

public class PlayerCheckStatus : BaseUpdateAction
{
    PlayMoveScript p;
    Dictionary<int, Module_Effect> tmpDic;
    List<int> objectNeedToBeRemoved = new List<int>();
    public PlayerCheckStatus(PlayMoveScript p) : base(1)
    {
        this.p = p;
        action += doAction;

        tmpDic = GameManagerInVillage.PlayerControl.module.effectList;
    }

    public void doAction()
    {
        if (isStoped == false)
        {
            if (tmpDic.Count > 0)
            {
                foreach (var i in tmpDic)
                {
                    i.Value.Timer += Time.deltaTime;
                    i.Value.TotalTimer += Time.deltaTime;
                    if (i.Value.Timer > i.Value.Inverval)
                    {
                        i.Value.Affect(GameManagerInVillage.PlayerControl.module);
                        i.Value.Timer = 0;
                    }
                    if (i.Value.TotalTimer > i.Value.Duration)
                    {
                        objectNeedToBeRemoved.Add(i.Key);
                    }
                }
                foreach (var i in objectNeedToBeRemoved)
                {
                    tmpDic.Remove(i);
                }
            }
        }
    }
}

public class NewPlayerMove : BaseUpdateAction
{
    NavMeshAgent navMeshAgent;
    Camera secondCamera;
    Vector3 BackToPlayer;
    bool isAutoNav;

    Vector3 moveVector3;
    Transform thistransform;
    AnimationClip[] clips;

    public NewPlayerMove(NavMeshAgent navMeshAgent, Camera secondCamera, Transform thistransform) : base(1)
    {
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
            if (isAutoNav == false)
            {

                //thistransform.Rotate(new Vector3(0, Input.GetAxis("Horizontal"), 0));
                if (Input.GetAxis("Vertical") != 0)
                {
                    navMeshAgent.velocity = thistransform.rotation * new Vector3(0, 0, Mathf.Abs(Input.GetAxis("Vertical")) * 2);
                    thistransform.rotation = Quaternion.Lerp(thistransform.rotation, Quaternion.LookRotation((BackToPlayer - thistransform.position) * -Input.GetAxis("Vertical")), 0.1f);
                }
                if (Input.GetAxis("Horizontal") != 0)
                {
                    navMeshAgent.velocity = thistransform.rotation * new Vector3(0, 0, Mathf.Abs(Input.GetAxis("Horizontal")) * 2);
                    thistransform.rotation = Quaternion.Lerp(thistransform.rotation, Quaternion.LookRotation(Quaternion.AngleAxis(90, Vector3.up) * (BackToPlayer - thistransform.position) * -Input.GetAxis("Horizontal")), 0.1f);
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

public class AnimatorObverser : BaseUpdateAction
{
    PlayMoveScript p;
    AnimationClip[] clips;
    bool ReverseAni;
    float timer;

    public AnimatorObverser(PlayMoveScript p) : base(1)
    {
        this.p = p;
        clips = p.animator.runtimeAnimatorController.animationClips;
        action += doAction;
    }

    public void doAction()
    {
        if (isStoped == false)
        {
            p.animator.SetFloat("Speed", Mathf.Max(Mathf.Abs(Input.GetAxis("Vertical")), Mathf.Abs(Input.GetAxis("Horizontal"))) * 2);
            //p.animator.SetFloat("Rotate", Input.GetAxis("Horizontal"));
            if (Input.GetKeyDown(KeyCode.H))
            {
                p.animator.SetTrigger("Attack");
            }
            if (Input.GetKeyDown(KeyCode.J))
            {
                p.animator.SetTrigger("Skill");
            }
            if (Input.GetKeyDown(KeyCode.K))
            {
                p.animator.SetTrigger("Restore");
            }
            if (Input.GetKeyDown(KeyCode.Y))
            {
                //p.navMeshAgent.updatePosition = false;
                //p.animator.SetTrigger("Jump");
                //ReverseAni = true;
                //timer = 1;
                //p.animator.speed = 0;
                p.animator.Play("ReAttack", 1, 0);
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                p.animator.SetTrigger("Jump");
            }

            for (int i = 1; i < 3; i++)
            {
                AnimatorStateInfo s = p.animator.GetCurrentAnimatorStateInfo(i);
                if (s.IsName("Jump"))
                {
                    foreach (var j in p.animator.GetCurrentAnimatorClipInfo(i))
                    {
                        //Debug.Log($"{j.clip.name}+{j.clip.frameRate}+{j.clip.length}+{j.clip.length * j.clip.frameRate}+" +
                        //    $"{j.clip.length * j.clip.frameRate * s.normalizedTime}");
                        if (j.clip.length * j.clip.frameRate * s.normalizedTime < 38 || j.clip.length * j.clip.frameRate * s.normalizedTime > 56)
                        {
                            p.navMeshAgent.velocity = Vector3.zero;
                        }
                        if (j.clip.length * j.clip.frameRate * s.normalizedTime > 56 && j.clip.length * j.clip.frameRate * s.normalizedTime < 57)
                        {
                            p.BigExplosion.SetActive(true);
                            p.BigExplosion.GetComponent<ParticleSystem>().Play();
                        }
                    }
                }
            }
            if (ReverseAni && timer > 0)
            {
                p.animator.Play("Attack", 1, timer);
                timer -= Time.deltaTime / 3;
            }
            else
            {
                p.animator.speed = 1;
                ReverseAni = false;
            }
        }
    }
}