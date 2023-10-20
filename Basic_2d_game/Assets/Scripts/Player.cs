using UnityEngine;
using UnityEngine.InputSystem;



public class Player : MonoBehaviour
{
    
    Rigidbody2D myRigidbody2D;
    Animator myAnimator;
    BoxCollider2D myBoxCollider2d;
    PolygonCollider2D myPolygonCollider2d;

    float startingGravityScale;

    [SerializeField]
    float runspeed = 2f;
    [SerializeField]
    float jumpspeed = 2f;
    [SerializeField]
    float climbspeed = 8f;
    // Start is called before the first frame update
    void Start()
    {
        
        myRigidbody2D= GetComponent<Rigidbody2D>();
        myAnimator= GetComponent<Animator>();
        myBoxCollider2d= GetComponent<BoxCollider2D>();
        myPolygonCollider2d= GetComponent<PolygonCollider2D>();
        startingGravityScale=myRigidbody2D.gravityScale;
    }

    // Update is called once per frame
    void Update()
    {
       
        Run();
        Jump();
        climb();
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
  

    private void climb()
    {
        if (myBoxCollider2d.IsTouchingLayers(LayerMask.GetMask("Climbing")))
        {
            Debug.Log("istouching");
            float verticalInput = Input.GetAxis("Vertical");
            Vector2 climbingV = new Vector2(myRigidbody2D.velocity.x, verticalInput * climbspeed);
            myRigidbody2D.velocity = climbingV;

            myRigidbody2D.gravityScale = 0f;
        }
        else { myRigidbody2D.gravityScale = startingGravityScale; }
    }

    public void Jump()
    {
        if (!myPolygonCollider2d.IsTouchingLayers(LayerMask.GetMask("Ground"))) { return; }
        bool isJumping = Input.GetButtonDown("Jump");
        if (isJumping)
        {
            myRigidbody2D.velocity = new Vector2(myRigidbody2D.velocity.x, jumpspeed);
        }
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
