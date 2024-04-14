using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public float jumpForce = 10f;
    public float gravityScale = 1f; // Yer çekimi kuvvetinin ölçeði
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
        rb.gravityScale *= -1; // Yer çekimi yönünü tersine çevir
    }

    void Jump()
    {
        rb.velocity = Vector2.up * jumpForce * Mathf.Sign(rb.gravityScale); // Yer çekimine göre zýplama
    }

    void FlipCharacter()
    {
        // Karakterin dönüþünü yer çekimine göre ayarla
        if (Mathf.Sign(rb.gravityScale) == 1) // Yer çekimi aþaðý doðru
        {
            spriteRenderer.flipY = false; // Karakter normal pozisyonda
        }
        else // Yer çekimi yukarý doðru
        {
            spriteRenderer.flipY = true; // Karakter baþ aþaðý pozisyonda
        }
    }

    void AdjustCollider()
    {
        // Collider'ýn yerini ve boyutunu ayarla
        if (Mathf.Sign(rb.gravityScale) == 1) // Yer çekimi aþaðý doðru
        {
            standingCollider.enabled = true;
            upsideDownCollider.enabled = false;
        }
        else // Yer çekimi yukarý doðru
        {
            standingCollider.enabled = false;
            upsideDownCollider.enabled = true;
        }
    }
}
