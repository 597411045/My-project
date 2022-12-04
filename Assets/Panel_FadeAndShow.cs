using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Panel_FadeAndShow : MonoBehaviour
{
    public AnimationCurve animationCurve;
    public float timer;
    float a;
    GameObject panel2;


    private void Awake()
    {
        panel2 = GameObject.Find("Panel2");
        if (this.gameObject.name == "Panel2")
        {
            this.transform.localScale = Vector3.zero;
        }
        if (this.gameObject.name == "Panel1")
        {
            panel2.SetActive(false);
            this.enabled = false;
        }
    }

    void Start()
    {

    }

    void Update()
    {
            timer += Time.deltaTime;
            a = animationCurve.Evaluate(timer);
            this.transform.localScale = new Vector3(a, a, a);
            if (timer > 1)
            {
                this.enabled = false;
                panel2.SetActive(true);
            }
    }
}
