
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class playerController : MonoBehaviour
{
    //References
    Rigidbody2D m_rb;
    Animator m_anim;

    //Custom Ref For Damping
    private Vector3 m_Velocity = Vector3.zero;
    private float m_float;

    //Customizeable
    [SerializeField] float m_jumpHeight = 5;

    //Scalar for movement
    [Range(1, 10)] [SerializeField] float m_speed = 3.72f;

    //Scalar For Movement Smoothing
    [Range(0, .3f)] [SerializeField] float m_ScSmooth = .05f;

    //Variables
    private bool m_FacingRight;


    private void Awake()
    {
        m_rb = GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    ///Flips Character
    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        m_FacingRight = !m_FacingRight;

        // Spin Renderer 
        SpriteRenderer MyImage = gameObject.GetComponent<SpriteRenderer>();
        MyImage.flipX = !MyImage.flipX;

    }
    private void Jump(bool _jump)
    {
        if (_jump)
        {
            float mygrav = m_rb.gravityScale * Physics2D.gravity.y;
            var _jumpVelocity = (Mathf.Sqrt(Mathf.Abs(mygrav) * m_jumpHeight * 2.0f));

            m_rb.AddForce(m_rb.transform.up * _jumpVelocity * m_rb.mass, ForceMode2D.Impulse);
        }

    }


    public void Move(float _hSpeed, bool _jump)
    {
        // float f_trgS = _hSpeed * m_speed * 100;
        Vector3 v3_targetVel = new Vector2(_hSpeed * m_speed * 100, m_rb.velocity.y);

        if (_hSpeed > 0 || _hSpeed < 0)
        {
            // float targetspeed = Mathf.SmoothDamp(m_rb.velocity.x, f_trgS, ref m_float, m_ScSmooth);
            // m_rb.velocity = new Vector3(targetspeed, m_rb.velocity.y, 0);

            m_rb.velocity = Vector3.SmoothDamp(m_rb.velocity, v3_targetVel, ref m_Velocity, m_ScSmooth);
        }
        Jump(_jump);

        if (_hSpeed > 0 && m_FacingRight)
        {
            // ... flip the player.
            Flip();
        }
        // Otherwise if the input is moving the player Right and the player is facing right...
        else if (_hSpeed < 0 && !m_FacingRight)
        {
            // ... flip the player.
            Flip();
        }
    }



}
