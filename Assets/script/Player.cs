using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{


    public float jump_force;
    public bool isGround;

    public float move_speed = 5f;
    private float junp_force;

    private Rigidbody2D rigd;
    public Animator anim;

    public Transform point;
    public float radius;
    private bool isattack;


    // Start is called before the first frame update
    void Start()
    {
        rigd = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>(); //acessando o player no componente animator
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Jump();
        Attack();
    }

    void Move()
    {
        float tecla = Input.GetAxis("Horizontal");
        rigd.velocity = new Vector2(tecla * move_speed, rigd.velocity.y);

        if (tecla < 0 && isGround == true)
        {
            transform.eulerAngles = new Vector2(0f, 180f);
            anim.SetInteger("transition", 1);
        }
        if (tecla > 0 && isGround == true)
        {
            transform.eulerAngles = new Vector2(0f, 0f);
            anim.SetInteger("transition", 1);
        }
        if (tecla == 0 && isGround== true)
        {
            anim.SetInteger("transition", 0);
        }

    }

     void Jump()
     {
          if (Input.GetKeyDown(KeyCode.Space) && isGround == true) //condicional
          {
              rigd.AddForce(Vector2.up * jump_force , ForceMode2D.Impulse);
              isGround = false;
              anim.SetInteger("transition", 2);
          }
      }
    void Attack()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Collider2D hit = Physics2D.OverlapCircle(point.position, radius);
            if (hit != null)
            {
                Debug.Log(hit.name);
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(point.position, radius);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "ground")
        {
            Debug.Log("Estou no chao");
            isGround = true;

        }

        if (collision.gameObject.tag == "quadrado")
        {
            Debug.Log("Voce bateu na caixa branca seu tanso");
        }
    
    }

}