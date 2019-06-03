using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inputManager : MonoBehaviour
{
    public playerController controller;

    public Animator animator;

    [SerializeField] KeyCode btn_Grab_Conf = KeyCode.Z;
    [SerializeField] KeyCode btn_Shoot_Back = KeyCode.X;

    public bool m_Repopulate;

    float f_force_Hrzt = 0f;

    bool b_jump;
    public bool m_jumpEnabled = true;

    public bool b_isMenu;
    void Awake()
    {
        if (controller == null)
        {
            controller = GameObject.FindGameObjectWithTag("Player").GetComponent<playerController>();
            animator = controller.GetComponent<Animator>();
            controller.m_mgr = this;
        }

    }


    private bool m_isAxisInUse = false;
    // Update is called once per frame
    void Update()
    {
        if (b_isMenu)
        {
            if (Input.GetKeyDown(btn_Grab_Conf))
            {
                //Confirm
            }
            if (Input.GetKeyDown(btn_Shoot_Back))
            {
                //Go Back
            }

            if (Input.GetAxisRaw("Horizontal") != 0 && Input.GetAxisRaw("Vertical") != 0)
            {
                if (m_isAxisInUse == false)
                {
                    if (Input.GetAxisRaw("Horizontal") == -1 || Input.GetAxisRaw("Vertical") == -1)
                    {
                        // Izquierda
                    }
                    else if (Input.GetAxisRaw("Horizontal") == 1 || Input.GetAxisRaw("Vertical") == 1)
                    {
                        // Derecha
                    }

                    m_isAxisInUse = true;
                }
            }
            if (Input.GetAxisRaw("Horizontal") == 0 && Input.GetAxisRaw("Vertical") == 0)
            {
                m_isAxisInUse = false;
            }
        }

        else if (m_Repopulate == true)
        {
            if (GameObject.FindGameObjectWithTag("Player").GetComponent<playerController>() != null)
            {
                controller = GameObject.FindGameObjectWithTag("Player").GetComponent<playerController>();
                animator = controller.GetComponent<Animator>();
                controller.m_mgr = this;
                m_Repopulate = false;
            }
        }

        else
        {
            //getaxisRaw if you want -1 0 1 get axis 0.0f 0.5f 1.0f and so on
            f_force_Hrzt = Input.GetAxisRaw("Horizontal");

            animator.SetFloat("Speed", Mathf.Abs(f_force_Hrzt));

            if (Input.GetKeyDown(btn_Grab_Conf))
            {
                //Grab
                controller.Grab(true);
            }

            if (Input.GetKeyDown(btn_Shoot_Back))
            {
                //Throw
            }

            if (Input.GetButtonDown("Jump") && m_jumpEnabled)
            {
                b_jump = true;
                // animator.SetBool("IsJumping", true);
            }
        }
    }

    void FixedUpdate()
    {
        // Move our character
        if (controller != null)
        {
            controller.Move(f_force_Hrzt * Time.fixedDeltaTime, b_jump);
            b_jump = false;
        }

    }

}
