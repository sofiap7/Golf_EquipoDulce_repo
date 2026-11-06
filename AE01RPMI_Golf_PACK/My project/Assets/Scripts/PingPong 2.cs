using UnityEngine;

public class PingPong2 : MonoBehaviour
{
    public float velocidad = 2f;
    public float altura = 3f;

    private float posicionInicial;

    void Start()
    {
        posicionInicial = transform.position.y;
    }

    void Update()
    {
        float y = posicionInicial + Mathf.PingPong(Time.time * velocidad, altura);
        transform.position = new Vector3(transform.position.x, y, transform.position.z);
    }
}
