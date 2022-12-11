using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FXAutoScript : MonoBehaviour
{

    ParticleSystem ps;
    float timer;
    private void Awake()
    {
        ps = this.GetComponent<ParticleSystem>();
    }

    private void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > ps.duration)
        {
            timer = 0;
            this.gameObject.SetActive(false);
        }
    }
}
