using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class sc_Node : MonoBehaviour
{
    public GameObject myobj;
    GameObject m_ode;
    Rigidbody2D rb;
    LineRenderer lineRenderer;
    private void Awake()
    {
        if (rb == null)
        {
            rb = GetComponent<Rigidbody2D>();
        }

        if (lineRenderer == null)
        {
            lineRenderer = GetComponent<LineRenderer>();
        }
    }

    float f_ThresholdDistance = .519f;

    private void Start()
    {
        // Vector3 desiredpos;
        GameObject prev = null;

        for (int i = 0; i < 6; i++)
        {
            m_ode = Instantiate(myobj, transform.position, Quaternion.identity);

            var hinge = m_ode.AddComponent<HingeJoint2D>();
            m_ode.GetComponent<Rigidbody2D>().gravityScale = 1.7f;
            m_ode.GetComponent<Rigidbody2D>().mass = .2f;

            if (prev == null)
            {
                hinge.connectedBody = this.rb;
            }
            else
            {
                hinge.connectedBody = prev.GetComponent<Rigidbody2D>();
            }
            hinge.autoConfigureConnectedAnchor = false;
            hinge.enableCollision = false;
            hinge.breakForce = 1.19f * 50 * 9.81f * 9.81f;
            hinge.anchor = new Vector2(0, 0.45f);
            hinge.connectedAnchor = new Vector2(0, -0.39f);

            prev = hinge.gameObject;
        }
    }


}

