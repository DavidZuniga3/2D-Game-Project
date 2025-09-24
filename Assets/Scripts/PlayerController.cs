using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 10;
    public float jumpPower = 50;
    public float playerGrav;
    public float knockback = 10;
    public bool onGround = true;
    private float horizontalInput;
    private Rigidbody2D playerRB;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
        Physics2D.gravity *= playerGrav;
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");

        //Moves player left or right
        transform.Translate(Vector2.right * Time.deltaTime * speed * horizontalInput);

        //Makes player jump
        if(Input.GetKeyDown(KeyCode.Space) && onGround)
        {
            playerRB.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);

            //Prevents double jumping
            onGround = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            onGround = true;
        }
        else if(collision.gameObject.CompareTag("Enemy"))
        {
            Vector2 knockDir = (transform.position - collision.transform.position).normalized;
            if (knockDir.y < 0)
                knockDir.y *= -1;

            playerRB.AddForce(knockDir * knockback, ForceMode2D.Impulse);
            Debug.Log("Hit" + knockDir + (knockDir * knockback));
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Collectible"))
        {
            Destroy(collision.gameObject);
            Debug.Log("Collected");
        }
    }
}
