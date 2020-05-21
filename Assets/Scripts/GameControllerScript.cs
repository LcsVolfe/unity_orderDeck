using System;
using System.Collections;
using System.Linq;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;


public class GameControllerScript : MonoBehaviour
{
    
    public int SCENE;
 
    public GameObject mensagemGO;
    public GameObject voltarMenuGO;
    public GameObject reiniciarGO;
    public GameObject[] cartasGO;
    public Button reiniciar;
    public Button voltarMenu;
    public Button cartaBase;
    public Button[] botoes; 
    public Sprite[] imageCards;
    public Text jogadaText;
    public Text rodadaText;
    public Text mensagemText;
    public Sprite verso;
    public Sprite coringa;
    public int valorBase;
    public int contJogada;
    public int level = 3;
    public int[] baralho; 
    AudioSource recursoSomCarta;
    public AudioClip somCarta;

    void Start()
    {
        
        mensagemGO.SetActive(false);
        voltarMenuGO.SetActive(false);
        reiniciarGO.SetActive(false);
        
        reiniciar.onClick.AddListener(()=> restartCurrentScene()); 
        voltarMenu.onClick.AddListener(()=> SceneManager.LoadScene("index")); 
        
        recursoSomCarta = GetComponent<AudioSource>();
        recursoSomCarta.clip = somCarta;
        
        valorBase = 0;
        contJogada = 0;

        Random.InitState(System.Environment.TickCount);
        int pos = Random.Range(0,level);
        
        for(int i = 0; i < botoes.Length; i++)
        {
            botoes[i].onClick.AddListener(ExecutarAcaoCarta); 
        }
        
        for (int i = 0; i < level; i++)
        {
            while (baralho.Contains(pos))
            {
                pos = Random.Range(0,level);
            }

            if (botoes[i] != null)
            {
                int index = new int();
                index = i;
                botoes[i].onClick.AddListener(() => LogicMainGame(index));
                baralho[i] = pos;
            }
        }
        
        
    }

    public void LogicMainGame(int botao)
    {

        botoes[botao].image.sprite = imageCards[baralho[botao]];
        if (contJogada == 3)
        {
            StartCoroutine(gameOver());
        } 
        else if (baralho[botao] == valorBase)
        {
            cartaBase.image.sprite = imageCards[baralho[botao]];
            cartasGO[botao].SetActive(false);
            valorBase++;
            contJogada = 0;
            if (valorBase == level)
            {
                nextScene();
            }
        }
        else if (contJogada < 3)
        {
            StartCoroutine(esconderCarta(botao));
            contJogada++;
            if (contJogada == 3)
            {
                StartCoroutine(gameOver());
            } 
        }


        jogadaText.text = "Jogada: " + (3 - contJogada);
        rodadaText.text = "Nivel: " + SCENE;
    }
    
    public void ExecutarAcaoCarta(){
        recursoSomCarta.Play();
    }


    IEnumerator esconderCarta(int carta)
    {
        yield return new WaitForSeconds(0.7f);
        botoes[carta].image.sprite = verso;
    }

    IEnumerator gameOver()
    {
        mensagemText.text = "Você perdeu.";
        mensagemGO.SetActive(true);
        voltarMenuGO.SetActive(true);
        reiniciarGO.SetActive(true);
        foreach (var button in botoes) button.enabled = false;
        yield return new WaitForSeconds(0.5f);
    }

    void restartCurrentScene()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }
    
    public void nextScene()
    {
        mensagemText.text = "Parabéns!";
        mensagemGO.SetActive(true);
        new WaitForSeconds(2f);
        
        if(SCENE == 1) SceneManager.LoadScene("levelTwo");
        else if(SCENE == 2) SceneManager.LoadScene("index");
        
    }
}