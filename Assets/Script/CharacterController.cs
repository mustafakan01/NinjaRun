using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public float jumpForce = 10f;
    public float gravityScale = 1f; // Yer �ekimi kuvvetinin �l�e�i
    public bool isOnGround = true;
    public Transform groundCheck;
    public LayerMask groundLayer;
    public Collider2D standingCollider;
    public Collider2D upsideDownCollider;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ChangeGravityDirection();
            FlipCharacter();
            AdjustCollider();
        }

        if (isOnGround && Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }

    void FixedUpdate()
    {
        isOnGround = Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);
    }

    void ChangeGravityDirection()
    {
        rb.gravityScale *= -1; // Yer �ekimi y�n�n� tersine �evir
    }

    void Jump()
    {
        rb.velocity = Vector2.up * jumpForce * Mathf.Sign(rb.gravityScale); // Yer �ekimine g�re z�plama
    }

    void FlipCharacter()
    {
        // Karakterin d�n���n� yer �ekimine g�re ayarla
        if (Mathf.Sign(rb.gravityScale) == 1) // Yer �ekimi a�a�� do�ru
        {
            spriteRenderer.flipY = false; // Karakter normal pozisyonda
        }
        else // Yer �ekimi yukar� do�ru
        {
            spriteRenderer.flipY = true; // Karakter ba� a�a�� pozisyonda
        }
    }

    void AdjustCollider()
    {
        // Collider'�n yerini ve boyutunu ayarla
        if (Mathf.Sign(rb.gravityScale) == 1) // Yer �ekimi a�a�� do�ru
        {
            standingCollider.enabled = true;
            upsideDownCollider.enabled = false;
        }
        else // Yer �ekimi yukar� do�ru
        {
            standingCollider.enabled = false;
            upsideDownCollider.enabled = true;
        }
    }
}
