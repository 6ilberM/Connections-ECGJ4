using UnityEngine;

public class sc_Vine : MonoBehaviour
{
    bool entry;
    ContactFilter2D cf;
    public Collider2D myCollider;
    playerController woop;
    private void Awake()
    {
        myCollider = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            woop = other.gameObject.GetComponent<playerController>();
            woop.otherRigid = gameObject.GetComponent<Rigidbody2D>();
            woop.within = true;
        }
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            woop.within = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            woop.within = true;
        }
    }
}