using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class VidaInimigos : MonoBehaviour
{
    [Header("Vida do inimigo")]
    [SerializeField] private int vidaInimigo = 30;
    [Header("Dano do Player no Inimigo")]
    [SerializeField] private int danoPlayer;
    private bool estahVivo = true;

    public VidaInimigos(bool estahVivo, bool levaDanoInim, bool estahVivoInim)
    {
        this.estahVivo = estahVivo;
        this.LevaDanoInim = levaDanoInim;
        this.EstahVivoInim = estahVivoInim;
    }

    private bool estahVivoInim;

    public VidaInimigos(bool estahVivoInim)
    {
        this.EstahVivoInim = estahVivoInim;
    }

    private bool levaDanoInim = true;

    public VidaInimigos(bool levaDanoInim, bool estahVivoInim)
    {
        this.LevaDanoInim = levaDanoInim;
        this.EstahVivoInim = estahVivoInim;
    }

    private Rigidbody2D rb;

    public bool LevaDanoInim { get => levaDanoInim; set => levaDanoInim = value; }
    public bool EstahVivoInim { get => EstahVivoInim1; set => EstahVivoInim1 = value; }
    public bool EstahVivoInim1 { get => estahVivoInim; set => estahVivoInim = value; }
    public bool EstahVivoInim2 { get => estahVivoInim; set => estahVivoInim = value; }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame


    IEnumerator LevarDanoInim(int dano)
    {
        vidaInimigo -= dano;
        EstaVivoInim();
        yield return new WaitForSeconds(0.5f);
    }


    public bool EstaVivoInim()
    {
        if (vidaInimigo < 1)
        {
            this.rb.GetComponent<Rigidbody2D>().Sleep();
            EstahVivoInim = false;
            Destroy(this.gameObject);
        } 
        return estahVivo = false; 
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Tiro"))
        {
            StartCoroutine(LevarDanoInim(danoPlayer));
        }
    }

    public override bool Equals(object obj)
    {
        return obj is VidaInimigos inimigos &&
               base.Equals(obj) &&
               estahVivo == inimigos.estahVivo;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(base.GetHashCode(), estahVivo);
    }
}
