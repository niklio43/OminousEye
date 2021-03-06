using UnityEngine;
public class PlayerMovement : MonoBehaviour
{
    Animator anim;

    Rigidbody2D body;

    float horizontal;
    float vertical;

    public bool isEye;

    public float runSpeed = 10.0f;

    private bool Movement;


    // Start is called before the first frame update
    void Start()
    {

        isEye = true;
        Movement = false;
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        if (isEye)
        {
            horizontal = Input.GetAxisRaw("Horizontal");
            vertical = Input.GetAxisRaw("Vertical");

            if (horizontal > 0)
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
            }
            if (horizontal < 0)
            {
                transform.eulerAngles = new Vector3(0, 180, 0);
            }
        }
        Movement = horizontal != 0;
        anim.SetBool("Movement", Movement);

    }

    private void FixedUpdate()
    {
            body.velocity = new Vector2(horizontal * runSpeed, vertical * runSpeed);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            isEye = false;
            collision.transform.gameObject.tag = "Possessed";
            Destroy(gameObject);
        }
    }
}