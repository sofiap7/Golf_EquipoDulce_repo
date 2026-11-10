
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;


public class PauseManager : MonoBehaviour
{
    private static string escenaAnterior;

    public void AbrirPausa()
    {
        escenaAnterior = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene("Pausa");
    }

    public void Continuar()
    {
       SceneManager.LoadScene(escenaAnterior);
    }

    void Update()
    {
        if (Keyboard.current.pKey.wasPressedThisFrame)
        {
            AbrirPausa();
        }
    }
}

