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
    private float pulo = 0.0F;


	private int pontos = 0;
	public TextMeshProUGUI  txtMoeda;

    public GameObject blocoCenario;
    public GameObject obstaculo1;
    public GameObject obstaculo2;
    public GameObject moeda;
    public GameObject moedaSuper;
    public GameObject moedaPlus;
    public GameObject pneu;

    private int inicioBloco = 0;
    private int quantidadeObstaculos = 10;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        somMoeda = GetComponents<AudioSource>()[0];

        criarObstaculos(quantidadeObstaculos);
        criarPneus(10);
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

    void criarPneus(int quantidade){
        for (int i = 0;i<quantidade; i++){
            float z = inicioBloco + Random.Range(3,88);
            float x = Random.Range(-12,12);
            float y = 0.0F;

            float tipo = Random.Range(0,2);

            GameObject go = GameObject.Instantiate(pneu);
            go.transform.position = new Vector3(x,y,z);     
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

        float zSuper = inicioBloco + 40F;
        float xSuper = Random.Range(-12,12);
        float ySuper = 1.5f;

        GameObject super = GameObject.Instantiate(moedaSuper);
        super.transform.position = new Vector3(xSuper,ySuper,zSuper);
    }

    void criarMoedasPlus(int quantidade){
        for (int i = 0;i<quantidade; i++){
            float z = inicioBloco + Random.Range(3,88);
            float x = Random.Range(-12,12);
            float y = 0.5f;

            GameObject go = GameObject.Instantiate(moedaPlus);
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

        float andar = 0.8F;
        if (Input.GetAxis("Vertical")>0){
            andar = 1.6F;
        }

        if (Input.GetAxis("Vertical")<0){
            andar = 0.4F;
        }

        float x = ( h * velocidade);
        float y = 0;
        float z = ( andar * velocidade);
        //transform.position = new Vector3(x,y,z);
        rb.velocity = (new Vector3(x,y,z));

        rb.AddForce(transform.up*pulo,ForceMode.Impulse);

        if (transform.position.y > 3F){
            pulo = -3f;
        }
        // } else if (transform.position.y < 2F && transform.position.y > 0.3F && pulo !=0F){
        //     rb.AddForce(transform.up*-2F,ForceMode.Impulse);
        // }

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
            criarPneus(10);
        }
    }

    void OnTriggerEnter(Collider col) {
        if(col.gameObject.CompareTag("Coin")){
            col.gameObject.SetActive(false);
            somMoeda.Play();
            pontos++;
		    //exibir os pontos na HUD
		    txtMoeda.text = ""+pontos;
        } else if(col.gameObject.CompareTag("CoinSuper")){
            col.gameObject.SetActive(false);
            somMoeda.Play();
            criarMoedasPlus(30);
        } else if(col.gameObject.CompareTag("Jump")){
            pulo = 3F;
        } else if(col.gameObject.CompareTag("Finish")){
             SceneManager.LoadScene("Fase2", LoadSceneMode.Single);
        } 
    }
}
