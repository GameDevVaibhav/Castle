using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    Animator myAnimator;
    [SerializeField] float radius = 3f;
    [SerializeField] Vector2 explosionForce=new Vector2(200f,200f);

    [SerializeField]
    AudioClip explodingSFX, burningSFX;

    AudioSource myAudioScource;
    // Start is called before the first frame update
    void Start()
    {
        myAnimator= GetComponent<Animator>();
        myAudioScource= GetComponent<AudioSource>();
    }

    void ExplodeBomb()
    {
        Collider2D playerCollider = Physics2D.OverlapCircle(transform.position, radius, LayerMask.GetMask("Player"));

        myAudioScource.PlayOneShot(explodingSFX);
        if (playerCollider)
        {
            playerCollider.GetComponent<Rigidbody2D>().AddForce(explosionForce);
            playerCollider.GetComponent<Player>().PlayerHit();
        }
    }

   

  
    private void OnTriggerEnter2D(Collider2D collision)
    {
        myAnimator.SetTrigger("Burn");
        myAudioScource.PlayOneShot(burningSFX);
    }

    void DestroyBomb()
    {
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
