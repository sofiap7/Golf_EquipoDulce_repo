using UnityEngine;

public class Movimiento : MonoBehaviour
{
    public float velocidad = 2f;
    public float distancia = 3f;

    private float posicionInicial;

    void Start()
    {
        posicionInicial = transform.position.x;
    }

    void Update()
    {
        float x = posicionInicial + -Mathf.PingPong(Time.time * velocidad, distancia);
        transform.position = new Vector3(x, transform.position.y, transform.position.z);
    }
}