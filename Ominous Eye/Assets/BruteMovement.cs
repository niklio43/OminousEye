using UnityEngine;
using System.Collections;
public class BruteMovement : MonoBehaviour
{
    Animator anim;
    Rigidbody2D body;

    private bool isJumping;
    private bool isEnemy;
    private bool lbMovement;
    private bool lbJump;
    private bool lbExplode;
    private bool lbHelmet;
    private bool lbIdleHelmet;
    private bool lbMelt;

    private float jumpForce;
    private float moveSpeed;
    private float moveHorizontal;
    private float moveVertical;
    private float bulletSpeed;
    private float angle;

    private Vector3 pos;

    public GameObject PlayerPrefab;
    public GameObject gun;
    public GameObject bullet;


    public Transform firePoint;
    private HealthBarOnEnemy Healthbar2;

    private int healthHolder;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        lbExplode = false;
        lbMovement = false;
        lbJump = false;
        lbHelmet = false;
        lbIdleHelmet = false;
        isEnemy = false;
        isJumping = false;
        lbMelt = false;

        bulletSpeed = 30f;
        moveSpeed = 1.5f;
        jumpForce = 20f;

        this.gameObject.GetComponent<BruteMovement>().enabled = false;
        Healthbar2 = gameObject.GetComponent<HealthBarOnEnemy>();

        pos = transform.position;

    }

    void Update()
    {
        moveHorizontal = Input.GetAxisRaw("Horizontal");
        moveVertical = Input.GetAxisRaw("Vertical");

        lbMovement = moveHorizontal != 0;
        lbJump = moveVertical != 0;

        anim.SetBool("lbMovement", lbMovement);
        anim.SetBool("lbJump", lbJump);
        anim.SetBool("lbExplode", lbExplode);
        anim.SetBool("lbHelmet", lbHelmet);
        anim.SetBool("lbIdleHelmet", lbIdleHelmet);
        anim.SetBool("lbMelt", lbMelt);

        pos = this.gameObject.transform.position;

        if (Input.GetKeyDown(KeyCode.E) && !lbExplode)
        {
            lbExplode = true;
            isEnemy = false;
            FindObjectOfType<AudioManager>().Play("PlayerLeavePossession");
            Invoke("instantiate", 2);
            Destroy(this.gameObject, 2);
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
        if (!lbExplode && isEnemy)
        {
            if (moveHorizontal > 0f || moveHorizontal < 0f)
            {
                body.AddForce(new Vector2(moveHorizontal * moveSpeed, 0f), ForceMode2D.Impulse);
            }
            if (!isJumping && isEnemy)
            {
                body.AddForce(new Vector2(0f, moveVertical * jumpForce), ForceMode2D.Impulse);
                FindObjectOfType<AudioManager>().Play("Jump");
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
        if (collision.gameObject.tag == "Barrel")
        {
            lbMelt = true;
        }


        if (collision.gameObject.tag == "Bullet" && this.gameObject.tag != "Possessed")
        {
            if (Mathf.Abs(this.gameObject.transform.position.x - collision.gameObject.transform.position.x) <= 1)
            {
                if (Mathf.Abs(this.gameObject.transform.position.y - collision.gameObject.transform.position.y) <= 1)
                {
                    if (Mathf.Abs(this.gameObject.transform.position.z - collision.gameObject.transform.position.z) <= 1)
                    {
                        Healthbar2.TakeDamage(1);
                        if (Healthbar2.TakeDamage(1) == 0)
                        {
                            lbExplode = true;
                            isEnemy = false;
                            Invoke("instantiate", 2);
                            Destroy(this.gameObject, 2);
                        }
                    }
                }
            }
        }

        if (collision.gameObject.tag == "EnemyBullet" && this.gameObject.tag == "Possessed")
        {
            Healthbar2.TakeDamage(1);
            if (Healthbar2.TakeDamage(1) == 0)
            {
                lbExplode = true;
                isEnemy = false;
                Invoke("instantiate", 2);
                Destroy(this.gameObject, 2);
            }
        }

        if (collision.gameObject.tag == "Player" && this.gameObject.tag == "Possessed")
        {
            isEnemy = true;
            this.gameObject.GetComponent<BruteMovement>().enabled = true;
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

    void instantiate()
    {
        Instantiate(PlayerPrefab, pos, Quaternion.identity);
    }

    void HandleHelmet()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            lbHelmet = true;
            lbIdleHelmet = true;
            Invoke("TakeOffHelmet", 5);
        }
    }

    void TakeOffHelmet()
    {
        lbHelmet = false;
    }


}