using UnityEngine;
using System.Collections;
public class LongLegsMovement : MonoBehaviour
{
    Animator anim;
    Rigidbody2D body;

    float horizontal;
    float vertical;

    private bool isJumping;
    public bool isEnemy;
    private bool lbMovement;
    private bool lbJump;
    private bool lbExplode;

    private float jumpForce;
    private float moveSpeed;
    private float moveHorizontal;
    private float moveVertical;

    private Vector3 pos;

    public GameObject PlayerPrefab;
    public HealthBarOnEnemy Healthbar2;

    public float health;



    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        lbExplode = false;
        lbMovement = false;
        lbJump = false;
        isEnemy = false;
        isJumping = false;

        moveSpeed = 3f;
        jumpForce = 60f;

        this.gameObject.GetComponent<LongLegsMovement>().enabled = false;
        Healthbar2 = gameObject.GetComponent<HealthBarOnEnemy>();

        pos = transform.position;

        health = 100;

    }

    void Update()
    {
        moveHorizontal = Input.GetAxisRaw("Horizontal");
        moveVertical = vertical = Input.GetAxisRaw("Vertical");

        lbMovement = moveHorizontal != 0;
        lbJump = moveVertical != 0;

        anim.SetBool("lbMovement", lbMovement);
        anim.SetBool("lbJump", lbJump);
        anim.SetBool("lbExplode", lbExplode);

        pos = this.gameObject.transform.position;

        if (Input.GetKeyDown(KeyCode.E) && !lbExplode)
        {
            lbExplode = true;
            isEnemy = false;
            Invoke("instantiate", 2);
            Destroy(this.gameObject, 2);
        }

       


    }

    private void FixedUpdate()
    {
        if (!lbExplode)
        {
            if (moveHorizontal > 0f || moveHorizontal < 0f)
            {
                body.AddForce(new Vector2(moveHorizontal * moveSpeed, 0f), ForceMode2D.Impulse);
            }
            if (!isJumping && isEnemy)
            {
                body.AddForce(new Vector2(0f, moveVertical * jumpForce), ForceMode2D.Impulse);
            }
            if (moveHorizontal > 0)
            {
                transform.eulerAngles = new Vector3(0, 180, 0);
            }
            if (moveHorizontal < 0)
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet" && this.gameObject.tag != "Possessed")
        {
            Healthbar2.TakeDamage(1);
            if (Healthbar2.TakeDamage(1) == 0)
            {

                anim.SetBool("lbExplode", true);
                Destroy(this.gameObject, 2);
            }

        }
        if (collision.gameObject.tag == "Player")
        {
            isEnemy = true;
            this.gameObject.GetComponent<LongLegsMovement>().enabled = true;
        }

        if (collision.gameObject.tag == "Floor" || collision.gameObject.tag == "Enemy")
        {
            isJumping = false;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Floor" || collision.gameObject.tag == "Enemy")
        {
            isJumping = true;
        }
    }



    void instantiate()
    {
        Instantiate(PlayerPrefab, pos, Quaternion.identity);
    }

    
    

    

}