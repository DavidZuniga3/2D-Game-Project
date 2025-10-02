using System.Threading.Tasks;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 10;
    public float jumpPower = 50;
    public float playerGrav;
    public float knockback = 10;
    public float playerHealth = 100;
    public float enemyHealth = 10;
    public bool onGround = true;
    public bool attack = true;
    public bool canMove = true;
    private float horizontalInput;
    private Rigidbody2D playerRB;
    public Transform respawnPoint;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
        Physics2D.gravity *= playerGrav;
        playerRB.position = respawnPoint.position;
    }

    // Update is called once per frame
    async Task Update()
    {
        if (playerHealth > 0 && canMove == true)
        {

            horizontalInput = Input.GetAxis("Horizontal");

            //Moves player left or right
            transform.Translate(Vector2.right * Time.deltaTime * speed * horizontalInput);

            //Makes player jump
            if (Input.GetKeyDown(KeyCode.Space) && onGround)
            {
                playerRB.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);

                //Prevents double jumping
                onGround = false;
            }
            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                attack = true;
            }
        }
        if (playerHealth == 0)
        {
            playerRB.position = respawnPoint.position;
            Debug.Log("Player Respawned");
            await Task.Delay(500);
            playerHealth = 100;
        }
        /*if (enemyHealth <= 0){
            Destroy(GameObject.CompareTage("Enemy"));
        }*/
    }

    private async Task OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            onGround = true;
        }
        else if(collision.gameObject.CompareTag("Enemy") && playerHealth > 0 && attack == false)
        {
            canMove = false;
            playerRB.velocity = Vector2.zero;
            horizontalInput = -5;
            //Knocks player left or right
            transform.Translate(Vector2.right * Time.deltaTime * knockback * horizontalInput);
            playerRB.AddForce(Vector2.up * 20, ForceMode2D.Impulse);
            playerHealth -= 10;
            Debug.Log("Hit");
            await Task.Delay(1000);
            canMove = true;
        }
        else if(collision.gameObject.CompareTag("Enemy") && attack == true)
        {
            enemyHealth -= 10;
            Debug.Log("Killed Enemy");
            Destroy(gameObject.CompareTag("Enemy"));
            await Task.Delay(1000);
            attack = false;
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
