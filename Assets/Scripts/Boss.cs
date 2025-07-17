using System.Collections;
using Unity.VisualScripting;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [Header("Ponha as referencias do ataque")]
    [SerializeField] private GameObject miraPrefabInim;
    [SerializeField] private GameObject ataquePrefabInim;
    [SerializeField] private GameObject quebraPreFabInim;
    [Header("Ponha a forca do tiro e do dash do inimigo")]
    [SerializeField] private int forcaTiroInim;
    [SerializeField] private int forcaDashInim;
    private Player player;
    private Rigidbody2D rb;
    private VidaInimigos vdInim;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GetComponent<Player>();
        vdInim = GetComponent<VidaInimigos>();  
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(TempoAtaques());
        if (vdInim.EstaVivoInim())
        {

        }
    }
    IEnumerator TempoAtaques()
    {
        yield return new WaitForSeconds(7);
        DashBoos(true);

        if (DashBoos(false))
        {
            TiroBoss();
            yield return new WaitForSeconds(2);
            TiroBoss();
        }
    }
    private void TiroBoss()
    {
        GameObject BTiro = Instantiate(ataquePrefabInim, miraPrefabInim.transform.position, miraPrefabInim.transform.rotation);
        Rigidbody2D tiro = BTiro.GetComponent<Rigidbody2D>();  
        tiro.AddForce(Vector2.right * forcaTiroInim, ForceMode2D.Impulse);
    }

    private bool DashBoos(bool dash)
    {
        if (dash)
        {
            rb.AddForce(player.transform.position, ForceMode2D.Impulse);
        }
        return dash = false;
    }




}
