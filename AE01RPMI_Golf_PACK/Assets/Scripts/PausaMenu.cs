using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pantallaPausa;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Time.timeScale == 1f)
                Pausar();
            else
                Continuar();
        }
    }

    public void Pausar()
    {
        pantallaPausa.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Continuar()
    {
        pantallaPausa.SetActive(false);
        Time.timeScale = 1f;
    }

    public void IrAlMenuPrincipal()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MenuPrincipal");
    }
}