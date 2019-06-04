
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D))]
public class playerController : MonoBehaviour
{
    //References
    Rigidbody2D m_rb;
    // Animator m_anim;

    SpringJoint2D m_springJoint;

    public Rigidbody2D rb_vine;
    public inputManager m_mgr;

    //Ref Variables
    public bool iswithin;

    //Custom Ref For Damping
    private Vector3 m_Velocity = Vector3.zero;
    private float m_float;

    //Customizeable
    [SerializeField] float m_jumpHeight = 5;

    //Scalar for movement
    [Range(1, 10)] [SerializeField] float m_speed = 3.72f;

    //Scalar For Movement Smoothing
    [Range(0, .2f)] [SerializeField] float m_ScSmooth = .05f;

    //Variables
    private bool m_FacingRight;

    //Stuff
    [Header("Events")]
    [Space]

    public UnityEvent OnLandEvent;
    private bool b_isGrounded;

    private void Awake()
    {
        m_rb = GetComponent<Rigidbody2D>();
        m_springJoint = GetComponent<SpringJoint2D>();
        // m_anim = GetComponent<Animator>();
        m_springJoint.enableCollision = false;
        m_springJoint.autoConfigureDistance = false;
        m_springJoint.autoConfigureConnectedAnchor = false;
        m_springJoint.connectedAnchor = Vector2.zero;
        m_springJoint.anchor = Vector2.zero;
        m_springJoint.distance = 0.5f;
        m_springJoint.dampingRatio = .05f;
        m_springJoint.frequency = 3;

        m_springJoint.enabled = false;

        if (OnLandEvent == null)
        {
            OnLandEvent = new UnityEvent();
        }
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O) && m_mgr != null)
        {
            Destroy(gameObject);
        }
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


        if (_jump && true == b_isGrounded)
        {
            if (m_rb.velocity.y < 0)
            {
                m_rb.velocity = new Vector2(m_rb.velocity.x, 0);

            }
            float mygrav = m_rb.gravityScale * Physics2D.gravity.y;
            var _jumpVelocity = (Mathf.Sqrt(Mathf.Abs(mygrav) * m_jumpHeight * 2.0f));
            m_rb.AddForce(m_rb.transform.up * _jumpVelocity * m_rb.mass, ForceMode2D.Impulse);
            b_isGrounded = false;

        }
    }
    public void Grab(bool _z)
    {

        if (_z && !m_springJoint.enabled && rb_vine != null)
        {
            if (iswithin)
            {
                m_springJoint.enabled = true;
                m_springJoint.connectedBody = rb_vine.GetComponent<Rigidbody2D>();
            }
        }
        else if (_z)
        {
            m_springJoint.enabled = false;
        }

        else
        {
            // do nothing
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
            // m_rb.velocity += new Vector2(_hSpeed * m_speed * 100, m_rb.velocity.y);
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

    private void OnDestroy()
    {
        if (m_mgr != null)
        {
            m_mgr.m_Repopulate = true;
        }
    }

    void groundRayCheck()
    {
        bool wasgrounded = b_isGrounded;
        //Landed
        if ((Physics2D.Raycast(transform.position, Vector2.down,
        GetComponent<CapsuleCollider2D>().bounds.extents.y + 0.1f, LayerMask.GetMask("Platform"))) && (m_rb.velocity.normalized.y <= 0))
        {
            b_isGrounded = true;
            if (!wasgrounded)
            {
                OnLandEvent.Invoke();
            }
        }
    }
    private void FixedUpdate()
    {
        groundRayCheck();
    }
}
