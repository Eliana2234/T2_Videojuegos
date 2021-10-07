using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer sr;
    private Transform _transform;

    private const int ANIM_QUIETO = 0;
    private const int ANIM_CORRER = 1;
    private const int ANIM_SALTAR = 2;
    private const int ANIM_RUN_ATACAR = 3;
    private const int ANIM_ATACAR = 4;
    private const int ANIM_AGACHA = 5;
    private const int ANIM_MUERTE = 6;

    private float velocity = 10f;
    private float JumpForce = 10f;
    private int vidas = 3;

    public GameObject PisRigth;
    public GameObject PisLeft;
    public GameObject PisRigthM;
    public GameObject PisLeftM;
    private float tiempo = 1;

    public ControlVida Vida;

    private bool muerte = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        _transform = GetComponent<Transform>();
    }

    void Update()
    {

        rb.velocity = new Vector2(0, rb.velocity.y);
        animator.SetInteger("Estado", ANIM_QUIETO);

        if (Input.GetKey(KeyCode.RightArrow))
        {
            rb.velocity = new Vector2(velocity, rb.velocity.y);
            animator.SetInteger("Estado", ANIM_CORRER);
            sr.flipX = false;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            rb.velocity = new Vector2(-velocity, rb.velocity.y);
            animator.SetInteger("Estado", ANIM_CORRER);
            sr.flipX = true;
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.AddForce(new Vector2(rb.velocity.x, JumpForce), ForceMode2D.Impulse);
            animator.SetInteger("Estado", ANIM_SALTAR);
        }
        if (Input.GetKey(KeyCode.DownArrow))
            animator.SetInteger("Estado", ANIM_AGACHA);

        if (Input.GetKey("x"))
        {
            tiempo += Time.deltaTime;
            Debug.Log("El tiempo es de : " + tiempo);
        }

        if (Input.GetKeyUp("x"))
        {
            Debug.Log("El tiempo es de : " + tiempo);
            if (rb.velocity == new Vector2(0, 0))
                animator.SetInteger("Estado", ANIM_ATACAR);
            else
                animator.SetInteger("Estado", ANIM_RUN_ATACAR);
            if (tiempo >= 1 && tiempo < 3)
            {
                if (!sr.flipX)
                {
                    var KunaiPosition = new Vector3(_transform.position.x + 3f, _transform.position.y, _transform.position.z);
                    Instantiate(PisRigth, KunaiPosition, Quaternion.identity);
                }
                if (sr.flipX)
                {
                    var KunaiPosition = new Vector3(_transform.position.x - 3f, _transform.position.y, _transform.position.z);
                    Instantiate(PisLeft, KunaiPosition, Quaternion.identity);
                }
            }
            if (tiempo >= 3)
            {
                if (!sr.flipX)
                {
                    var KunaiPosition = new Vector3(_transform.position.x + 3f, _transform.position.y, _transform.position.z);
                    Instantiate(PisRigthM, KunaiPosition, Quaternion.identity);
                }
                if (sr.flipX)
                {
                    var KunaiPosition = new Vector3(_transform.position.x - 3f, _transform.position.y, _transform.position.z);
                    Instantiate(PisLeftM, KunaiPosition, Quaternion.identity);
                }
            }
            tiempo = 1;
        }


        if (muerte)
            animator.SetInteger("Estado", ANIM_MUERTE);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            vidas--;
            if (vidas == 0) muerte = true;
            if (vidas >= 0)
            {
                Vida.QuitarVida(1);
                Debug.Log(Vida.GetVida());
            }

        }
        if (collision.gameObject.CompareTag("Nivel"))
        {
            LoadNextLevel();
        }
    }
    [System.Obsolete]
    public void LoadNextLevel()
    {

        if (Application.loadedLevel < Application.levelCount - 1)
            Application.LoadLevel(Application.loadedLevel + 1);
    }
}
