using UnityEngine;

public class sc_Vine : MonoBehaviour
{
    playerController woop;

    private void Start()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {

            woop = other.gameObject.GetComponent<playerController>();
            // woop = other.gameObject.GetComponent<playerController>();
            
            woop.rb_vine = gameObject.GetComponent<Rigidbody2D>();
            // Debug.Log("um what");
            woop.iswithin = true;
        }
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            // Debug.Log("um que");
            woop.iswithin = true;

        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            woop.iswithin = false;
        }
    }
}