using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows;

public class SistemaDeVida : MonoBehaviour
{
    [SerializeField] private Slider indicadorVida;
    [SerializeField] private Slider indicadorMana;
    private int vida = 100;
    private int mana = 100;
    //private int manaMaxima = 100;
    //private int taxaMana = 5;
    private bool estahVivo = true;
    private bool levarDano = true;

    private Player player;
    private Rigidbody2D rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GetComponent<Player>();
        if (indicadorMana == null)
        {
            GameObject.Find("Mana");
            indicadorMana.maxValue = indicadorMana.value;
            indicadorMana.value = mana;
        }
        if (indicadorVida == null)
        {
            GameObject.Find("Vida");
            indicadorVida.maxValue = indicadorVida.value;
            indicadorVida.value = vida;
        }
    }


    public bool EstaVivo()
    {
        return estahVivo;
    }

    private void VerificarVida()
    {
        if (vida <= 0)
        {
            vida = 0;
            estahVivo = false;
        }
    }
    IEnumerator LevarDano(int dano)
    {

        if(vida > 0 && levarDano)
        {
            VerificarVida();
            vida -= dano;
            indicadorVida.value = vida;
            levarDano = false;
            yield return new WaitForSeconds(0.5f);
            levarDano = true;
        }
    }


    public void UsarMana()
    {
        mana -= 10;
        indicadorMana.value = mana;

        if (mana < 1)
        {
            mana = 0;
            player.TemMana();
        }
    }



    //IEnumerator RecarregaMana()
    //{
    //    if(manaMaxima > mana)
    //    {
    //        yield return new WaitForSeconds(3.0f);
    //        (float)mana += taxaMana * Time.deltaTime;
    //    }
    //}

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Espinho") && estahVivo && levarDano)
        {
            StartCoroutine(LevarDano(10));
        }
    }
}
