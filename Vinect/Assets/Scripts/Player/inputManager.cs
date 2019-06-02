using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inputManager : MonoBehaviour
{
    public playerController controller;

    public Animator animator;

    float f_force_Hrzt = 0f;

    bool b_jump;
    public bool m_jumpEnabled = true;

    void Awake()
    {

        if (controller == null)
        {
            controller = GameObject.FindGameObjectWithTag("Player").GetComponent<playerController>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        //getaxisRaw if you want -1 0 1 get axis 0.0f 0.5f 1.0f and so on
        f_force_Hrzt = Input.GetAxis("Horizontal");

        animator.SetFloat("Speed", Mathf.Abs(f_force_Hrzt));

        if (Input.GetButtonDown("Jump") && m_jumpEnabled)
        {
            b_jump = true;
           // animator.SetBool("IsJumping", true);
        }
    }

    void FixedUpdate()
    {
        // Move our character
        controller.Move(f_force_Hrzt * Time.fixedDeltaTime, b_jump);
        b_jump = false;
    }

}
