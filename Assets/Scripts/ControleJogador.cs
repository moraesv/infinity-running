using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class ControleJogador : MonoBehaviour
{

    private Rigidbody rb;
    public float velocidade;
    private AudioSource somMoeda;


	private int pontos = 0;
	public TextMeshProUGUI  txtMoeda;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        somMoeda = GetComponents<AudioSource>()[0];
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        // float v = Input.GetAxis("Vertical");
        float v = 0.5f;

        /* float pulo = 0.0f;
        if(Input.GetKey(KeyCode.Space)){
            pulo = 10;
        } */

        Vector3 mov = new Vector3(h, 0.0f, v);

        rb.AddForce(mov * velocidade);
    }

    void OnTriggerEnter(Collider col) {
        if(col.gameObject.CompareTag("Coin")){
            col.gameObject.SetActive(false);
            somMoeda.Play();
            pontos++;
		    //exibir os pontos na HUD
		    txtMoeda.text = ""+pontos;
        }
    }
}
