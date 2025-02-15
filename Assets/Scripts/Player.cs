using UnityEngine;

public class Player : MonoBehaviour
{
    public float JumpForce;

    public Rigidbody2D PlayerRigidBody;


    private bool isGrounded = true;
    public Animator Animator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            PlayerRigidBody.AddForceY(JumpForce,ForceMode2D.Impulse);
            isGrounded = false;
            Animator.SetInteger("state", 1);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name == "Platform")
        {
            if(!isGrounded)
            {
                Animator.SetInteger("state", 2);
            }
            isGrounded = true;

        }
    }
}
