using UnityEngine;
using UnityEngine.InputSystem;



public class Player : MonoBehaviour
{
    
    Rigidbody2D myRigidbody2D;
    Animator myAnimator;
    BoxCollider2D myBoxCollider2d;
    PolygonCollider2D myPolygonCollider2d;

    [SerializeField]
    float runspeed = 2f;
    [SerializeField]
    float jumpspeed = 2f;
    // Start is called before the first frame update
    void Start()
    {
        
        myRigidbody2D= GetComponent<Rigidbody2D>();
        myAnimator= GetComponent<Animator>();
        myBoxCollider2d= GetComponent<BoxCollider2D>();
        myPolygonCollider2d= GetComponent<PolygonCollider2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Run();
        
    }

    public void Run()
    {
       
        float horizontalInput = Input.GetAxis("Horizontal");

        // Apply the input to the player's velocity.
        myRigidbody2D.velocity = new Vector2(horizontalInput * runspeed, myRigidbody2D.velocity.y);
        FlipSprite();
        ChangeRunAnimation();
        ChangeJumpAnimation();
       
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (!myPolygonCollider2d.IsTouchingLayers(LayerMask.GetMask("Ground"))) { return; }
       
            myRigidbody2D.velocity=new Vector2(myRigidbody2D.velocity.x,jumpspeed);
        
    }

    private void ChangeRunAnimation()
    {
        bool runningHorizontally = Mathf.Abs(myRigidbody2D.velocity.x) > Mathf.Epsilon;
        myAnimator.SetBool("Running", runningHorizontally);
    }

    private void ChangeJumpAnimation()
    {
        bool jumping=Mathf.Abs(myRigidbody2D.velocity.y) > Mathf.Epsilon;
        myAnimator.SetBool("Climbing", jumping);
    }

    private void FlipSprite()
    {
        bool runningHorizontally = Mathf.Abs(myRigidbody2D.velocity.x) > Mathf.Epsilon;
        if (runningHorizontally)
        {
            transform.localScale = new Vector2(Mathf.Sign(myRigidbody2D.velocity.x),1f);
        }
    }
}
