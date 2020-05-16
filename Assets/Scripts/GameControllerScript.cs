using System.Collections; //mesmo conceito do Java, usamos aqui para vetor
using System.Collections.Generic;
using System.Linq;
using UnityEngine.SceneManagement; //já vimos antes
using UnityEngine;
using UnityEngine.UI;

//using TMPro;

public class GameControllerScript : MonoBehaviour
{
    //recursos para vincular a Cena da Unity
    public GameObject[] cartas; //elementos de cena
    public Button cartaBase; //elementos de cena
    public Button[] botoes; //botoes do canvas
    public Sprite[] imageCards; //são as figuras que atrelo ao botao
    public Text jogadaText;
    public Text rodadaText;
    public Sprite verso;
    public Sprite coringa;
    public int valorBase;
    public int contJogada;
    public int contRodadas = 0;
    public int[] baralho; 


    void Start()
    {
        valorBase = 0;
        contJogada = 0;
        baralho = new int[cartas.Length];
        for (int i = 0; i < baralho.Length; i++) baralho[i] = 100;

        Random.InitState(System.Environment.TickCount);
        int pos = Random.Range(0,cartas.Length);
        
        for (int i = 0; i < botoes.Length; i++)
        {
            while (baralho.Contains(pos))
            {
                pos = Random.Range(0,cartas.Length);
            }

            if (botoes[i] != null)
            {
                int index = new int();
                index = i;
                botoes[i].onClick.AddListener(() => ButtonClicked(index)); 
                baralho[i] = pos;
            }
        }
    }

    void ButtonClicked(int botao)
    {
        
        Debug.Log(baralho[botao]);

        botoes[botao].image.sprite = imageCards[baralho[botao]];
        // botoes[botao].image.sprite = imageCards[botao];
        if (contJogada == 3)
        {
            StartCoroutine(pausarPorUmTempoReiniciar());
        } 
        else if (baralho[botao] == valorBase)
        {
            cartaBase.image.sprite = imageCards[baralho[botao]];
            botoes[botao].enabled = false;
            valorBase++;
            contJogada = 0;
            if (valorBase == botoes.Length)
            {
                contRodadas++;
                StartCoroutine(pausarPorUmTempoReiniciar());
            }
        }
        else if (contJogada < 3)
        {
            StartCoroutine(esconderCarta(botao));
            contJogada++;
            if (contJogada == 3)
            {
                StartCoroutine(pausarPorUmTempoReiniciar());
            } 
        }


        jogadaText.text = "Jogada: " + (3 - contJogada);
        rodadaText.text = "Rodada: " + contRodadas;
    }

    IEnumerator esconderCarta(int carta)
    {
        yield return new WaitForSeconds(1f);
        botoes[carta].image.sprite = verso;
    }

    IEnumerator pausarPorUmTempoReiniciar()
    {
        contRodadas = 0;
        valorBase = 0;
        contJogada = 0;
        yield return new WaitForSeconds(0.5f);
        foreach (var button in botoes)
        {
            button.enabled = true;
            button.image.sprite = verso;
        }
        cartaBase.image.sprite = coringa;
        // this.restartCurrentScene();
    }

    public void restartCurrentScene()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }
}