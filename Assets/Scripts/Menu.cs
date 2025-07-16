using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    public void Jogar()
    {
        SceneManager.LoadScene("Historia");
    }

    public void Creditos()
    {
        SceneManager.LoadScene("Creditos");
    }

    public void Continuar()
    {
        SceneManager.LoadScene("AmbienteDoce");
    }

    public void Menup()
    {
        SceneManager.LoadScene("MenuPrincipal");
    }
}
