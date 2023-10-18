using UnityEngine;
using UnityEngine.InputSystem;



public class Player : MonoBehaviour
{
    
    Rigidbody2D myRigidbody2D;
    [SerializeField]
    float speed = 2f;
    // Start is called before the first frame update
    void Start()
    {
        
        myRigidbody2D= GetComponent<Rigidbody2D>();
        
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
        myRigidbody2D.velocity = new Vector2(horizontalInput * speed, myRigidbody2D.velocity.y);
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log("Jump!" + context.phase);
            myRigidbody2D.AddForce(Vector2.up * speed, ForceMode2D.Impulse);
        }
    }   
}
