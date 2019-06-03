using UnityEngine;

public class sc_Vine : MonoBehaviour
{
    playerController woop;
    private void Start()
    {
        if (woop == null)
        {
            woop = FindObjectOfType<inputManager>().controller;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            // woop = other.gameObject.GetComponent<playerController>();
            woop.rb_vine = gameObject.GetComponent<Rigidbody2D>();
            // woop.within = true;
        }
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
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