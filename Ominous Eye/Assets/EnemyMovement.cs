using UnityEngine;
using System.Collections;
public class EnemyMovement : MonoBehaviour
{
    Animator anim;
    Rigidbody2D body;

    float horizontal;
    float vertical;

    private bool isJumping;
    private bool isEnemy;
    private bool lbMovement;
    private bool lbJump;
    private bool lbExplode;

    private float jumpForce;
    private float moveSpeed;
    private float moveHorizontal;
    private float moveVertical;

    private Vector3 pos;

    public GameObject PlayerPrefab;

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
        jumpForce = 40f;

        this.gameObject.GetComponent<EnemyMovement>().enabled = false;

        pos = transform.position;
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
        if (collision.gameObject.tag == "Player")
        {
            isEnemy = true;
            this.gameObject.GetComponent<EnemyMovement>().enabled = true;
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