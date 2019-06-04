using UnityEngine;

public class sc_Vine : MonoBehaviour
{
    playerController Player;

    private void Start()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Player = other.gameObject.GetComponent<playerController>();

            Player.rb_vine = gameObject.GetComponent<Rigidbody2D>();

            Player.iswithin = true;
        }
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {

            Player.iswithin = true;

        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Player.iswithin = false;
        }
    }
}