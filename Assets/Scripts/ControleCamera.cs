using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControleCamera : MonoBehaviour
{
    public GameObject jogador;
    private Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - jogador.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 position = new Vector3(jogador.transform.position.x, 0f, jogador.transform.position.z);
        transform.position = position + offset;
    }
}
