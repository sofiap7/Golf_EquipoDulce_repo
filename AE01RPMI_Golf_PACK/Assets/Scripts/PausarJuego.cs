using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class NewMonoBehaviourScript : MonoBehaviour
{
    public GameObject menuPausa;
    public bool juegoPausado = false;

    private void Update()
    {
        if (Keyboard.current.pKey.wasPressedThisFrame)
        {
            if (juegoPausado)
            {
                Reanudar();
            }
            else
            {
                Pausar();
            }
        }
    }
    public void Reanudar()
    {
        menuPausa.SetActive(false);
        Time.timeScale = 1;
        juegoPausado = false;
    }
    public void Pausar()
    {
        menuPausa.SetActive(true);
        Time.timeScale = 0;
        juegoPausado = true;
    }
}
