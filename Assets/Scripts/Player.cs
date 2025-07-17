using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.U2D;

public class Player : MonoBehaviour
{
    [SerializeField] private float velocidadeAndar;
    [SerializeField] private float forcaPulo;
    [SerializeField] private GameObject miraPrefab;
    [SerializeField] private GameObject ataquePrefab;
    [SerializeField] private GameObject quebraPreFab;
    [SerializeField] private int forcaArremeco;
    private bool noPiso = true;
    private bool atak = true;
    private bool morreu = true;
    private bool temMana = true;
    private float inputH;
    private Animator animator;
    private SpriteRenderer sprite;
    private Rigidbody2D rb;
    private SistemaDeVida sVida;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sVida = GetComponent<SistemaDeVida>();
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(sVida.EstaVivo())
        {
            Andar();
            Pular();
            Atacar();
        }

        if(!sVida.EstaVivo() && morreu)
        {
            Morrer();
        }
    }

    private void Andar()
    {
        inputH = Input.GetAxis("Horizontal");
        transform.position += new Vector3(inputH * Time.deltaTime * velocidadeAndar, 0, 0);
        AnimaAndar();
    }

    private void AnimaAndar()
    {
        if (inputH > 0)
        {
            sprite.flipX = false;
            animator.SetBool("Run", true);
        }
        else if (inputH < 0)
        {
            sprite.flipX = true;
            animator.SetBool("Run", true);
        }
        else
        {
            animator.SetBool("Run", false);
        }
    }

    private void Pular()
    {
        if (Input.GetKey(KeyCode.Space) && noPiso)
        {
            rb.AddForce(Vector2.up * forcaPulo, ForceMode2D.Impulse);
            noPiso = false;
            animator.SetBool("Piso", false);
            animator.SetTrigger("Pulo");
        }
    }

    private void Atacar()
    {
        if(Input.GetMouseButtonDown(0))
        {
            StartCoroutine(LancarAtaque());
        }
    }


    IEnumerator LancarAtaque()
    {
        if(atak && temMana)
        {
            GameObject ataque = Instantiate(ataquePrefab, miraPrefab.transform.position, miraPrefab.transform.rotation);
            Rigidbody2D rbAtaque = ataque.GetComponent<Rigidbody2D>();
            rbAtaque.AddForce(miraPrefab.transform.forward * forcaArremeco, ForceMode2D.Force);
            sVida.UsarMana();
            atak = false;
            yield return new WaitForSeconds(1f);
            atak = true;
        }
    }

    public void TemMana()
    {
        temMana = false;
    }

    public void ComandoMana()
    {
        temMana = true;
    }


    private void Morrer()
    {
        morreu = false;
        rb.Sleep();
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Piso"))
        {
            noPiso = true;
            animator.SetBool("Piso", true);
        }
    }
}
