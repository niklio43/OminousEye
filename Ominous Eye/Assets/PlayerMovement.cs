using UnityEngine;
public class PlayerMovement : MonoBehaviour
{
    Animator anim;

    Rigidbody2D body;

    float horizontal;
    float vertical;

    public float runSpeed = 10.0f;
    public bool isEye;
    private bool Movement;


    // Start is called before the first frame update
    void Start()
    {
        Movement = false;
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        isEye = true;

    }

    // Update is called once per frame
    void Update()
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
            Destroy(gameObject);
        }
    }
}