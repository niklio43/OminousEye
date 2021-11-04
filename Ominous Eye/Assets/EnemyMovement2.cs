using UnityEngine;
using System.Collections;
public class EnemyMovement2 : MonoBehaviour
{
    Animator anim;

    Rigidbody2D body;

    float horizontal;
    float vertical;

    private bool isJumping;
    public bool isEnemy;

    private float jumpForce;
    private float moveSpeed;
    private float moveHorizontal;
    private float moveVertical;

    private bool lbMovement;
    private bool lbJump;
    private bool lbHelmet;
    private bool lbIdleHelmet;

    private bool lbExplode;

    private Vector3 pos;

    public GameObject PlayerPrefab;

    public GameObject gun;
    public GameObject bullet;
    public float bulletSpeed = 10f;
    public Transform firePoint;
    public float angle;








    // Start is called before the first frame update
    void Start()
    {
        lbExplode = false;
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        lbMovement = false;
        lbJump = false;
        lbHelmet = false;
        lbIdleHelmet = false;
        isEnemy = false;
        isJumping = false;
        moveSpeed = 3f;
        jumpForce = 40f;
        this.gameObject.GetComponent<EnemyMovement2>().enabled = false;



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
        anim.SetBool("lbHelmet", lbHelmet);
        anim.SetBool("lbIdleHelmet", lbIdleHelmet);
        pos = this.gameObject.transform.position;
        if (Input.GetKeyDown(KeyCode.E))
        {
            lbExplode = true;
            isEnemy = false;
            Destroy(this.gameObject); //not working with cooldown for some reason
            Instantiate(PlayerPrefab, pos, Quaternion.identity);
        }

        if (isEnemy)
        {
            HandleAiming();
            HandleShooting();
            HandleHelmet();


        }

    }

    private void FixedUpdate()
    {
        if (moveHorizontal > 0f || moveHorizontal < 0f)
        {
            body.AddForce(new Vector2(moveHorizontal * moveSpeed, 0f), ForceMode2D.Impulse);
        }
        if (moveHorizontal > 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);

        }
        if (moveHorizontal < 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);

        }

        if (!isJumping && isEnemy)
        {
            body.AddForce(new Vector2(0f, moveVertical * jumpForce), ForceMode2D.Impulse);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isEnemy = true;
            this.gameObject.GetComponent<EnemyMovement2>().enabled = true;
        }
        if (collision.gameObject.tag == "Floor")
        {
            isJumping = false;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            isJumping = true;
        }
    }

    void HandleAiming()
    {
        Vector3 mouse = Input.mousePosition;

        Vector3 screenPoint = Camera.main.WorldToScreenPoint(gun.transform.localPosition);

        Vector2 offset = new Vector2(mouse.x - screenPoint.x, mouse.y - screenPoint.y);

        angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;
        gun.transform.rotation = Quaternion.Euler(0f, 0f, angle);

        Vector3 localScale = Vector3.one;

        if (angle > 90 || angle < -90)
        {
            localScale.y = -1f;
        }

        else
        {
            localScale.y = +1f;
        }

        gun.transform.localScale = localScale;
    }

    void HandleShooting()
    {
        if (Input.GetMouseButton(0))
        {
            GameObject bulletClone = Instantiate(bullet);
            bulletClone.transform.position = firePoint.position;
            bulletClone.transform.rotation = Quaternion.Euler(0f, 0f, angle);

            bulletClone.GetComponent<Rigidbody2D>().velocity = firePoint.right * bulletSpeed;
        }
    }

    void HandleHelmet()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            lbHelmet = true;
            lbIdleHelmet = true;

            
        }
    }

}