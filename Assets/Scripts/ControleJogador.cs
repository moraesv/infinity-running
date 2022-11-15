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

    public GameObject blocoCenario;
    public GameObject obstaculo1;
    public GameObject obstaculo2;
    public GameObject moeda;

    private int inicioBloco = 0;
    private int quantidadeObstaculos = 10;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        somMoeda = GetComponents<AudioSource>()[0];

        criarObstaculos(quantidadeObstaculos);
        criarMoedas(30);
    }

    void criarObstaculos(int quantidade){
        for (int i = 0;i<quantidade; i++){
            float z = inicioBloco + Random.Range(3,88);
            float x = Random.Range(-12,12);
            float y = 0;

            float tipo = Random.Range(0,2);

            if (tipo < 1.0 ){
                GameObject go = GameObject.Instantiate(obstaculo1);
                go.transform.position = new Vector3(x,y,z);
            } else{
                GameObject go = GameObject.Instantiate(obstaculo2);
                go.transform.position = new Vector3(x,y,z);
            }
        }
    }

    void criarMoedas(int quantidade){
        for (int i = 0;i<quantidade; i++){
            float z = inicioBloco + Random.Range(3,88);
            float x = Random.Range(-12,12);
            float y = 0.5f;

            GameObject go = GameObject.Instantiate(moeda);
            go.transform.position = new Vector3(x,y,z);
        }
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        // float v = Input.GetAxis("Vertical");
        // float v = 0.5f;
        // Vector3 mov = new Vector3(h, 0.0f, v);
        // rb.AddForce(mov * velocidade);

        float andar = 0.5F;
        if (Input.GetAxis("Vertical")>0){
            andar = 1.0F;
        }


        float x = ( h * velocidade);
        float y = 0;
        float z = ( andar * velocidade);
        //transform.position = new Vector3(x,y,z);
        rb.velocity = (new Vector3(x,y,z));

        if (transform.position.z > inicioBloco) {
            //criar novo bloco

            inicioBloco = inicioBloco + 99;

            x=-3.077366f;
            y=-12.97272f;
            z=36.6775f +inicioBloco;

            
            GameObject go = GameObject.Instantiate(blocoCenario);
            go.transform.position = new Vector3(x,y,z);

            quantidadeObstaculos = quantidadeObstaculos * 2;
            criarMoedas(30);
            criarObstaculos(quantidadeObstaculos);
        }
    }

    void OnTriggerEnter(Collider col) {
        if(col.gameObject.CompareTag("Coin")){
            col.gameObject.SetActive(false);
            somMoeda.Play();
            pontos++;
		    //exibir os pontos na HUD
		    txtMoeda.text = ""+pontos;
        } else if(col.gameObject.CompareTag("Finish")){
             SceneManager.LoadScene("Fase2", LoadSceneMode.Single);
        }
    }
}
