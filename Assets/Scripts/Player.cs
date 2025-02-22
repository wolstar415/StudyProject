using UnityEngine;

public class Player : MonoBehaviour
{
    public float JumpForce;

    public Rigidbody2D PlayerRigidBody;


    private bool isGrounded = true;
    public Animator Animator;


    public BoxCollider2D PlayerCollider;


    public bool isInvincible = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameManager.Instance.Player = this;
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

     public void KillPlayer()
    {
        PlayerCollider.enabled = false;
        Animator.enabled = false;
        PlayerRigidBody.AddForceY(JumpForce, ForceMode2D.Impulse);
    }

    void Hit()
    {
        GameManager.Instance.lives -= 1;
    }
    void Heal()
    {
        GameManager.Instance.lives = Mathf.Min(3, GameManager.Instance.lives + 1);
    }

    void StartInvincible()
    {
        isInvincible = true;
        Invoke("StopInvincible", 5f);
    }

    void StopInvincible()
    {
        isInvincible = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "enemy")
        {
            if (!isInvincible)
            {

            Destroy(collision.gameObject);
            }
            Hit();
        }
        else if(collision.gameObject.tag =="food")
        {
            Destroy(collision.gameObject);
            Heal();
        }
        else if (collision.gameObject.tag == "golden")
        {
            Destroy(collision.gameObject);
            StartInvincible();
        }
    }
}
