using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NinjaController : MonoBehaviour
{
    private const int ANIM_CORRER = 1;
    private const int ANIM_ATACAR = 3;

    private Rigidbody2D rb;
    private Animator animator;
    private Transform _transform;

    private float velocity = 5f;
    private int vidas = 3;
    private float tiempo = 0;
    private bool muerte = false;
    public GameObject KunaiLeft;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        _transform = GetComponent<Transform>();
    }

    void Update()
    {
        rb.velocity = new Vector2(-velocity, rb.velocity.y);
        animator.SetInteger("Estado", ANIM_CORRER);
        tiempo += Time.deltaTime;
        if (tiempo > 6)
        {
            animator.SetInteger("Estado", ANIM_ATACAR);
            var KunaiPosition = new Vector3(_transform.position.x - 3f, _transform.position.y, _transform.position.z);
            Instantiate(KunaiLeft, KunaiPosition, Quaternion.identity);
            tiempo = 0;
        }

        if (muerte)
        {
            Destroy(this.gameObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "kunay")
        {
            vidas--;
            if (vidas == 0) muerte = true;
        }
        if (collision.gameObject.tag == "kunayM")
        {
            vidas -=2;
            if (vidas == 0) muerte = true;
        }
    }
}
