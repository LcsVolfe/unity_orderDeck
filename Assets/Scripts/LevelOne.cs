using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LevelOne : MonoBehaviour
{
	public Button cartaBase;
	public int level = 3;
	public Button[] botoes; 
	public Sprite[] imageCards;
	public Text jogadaText;
	public Text rodadaText;
	AudioSource recursoSomCarta;
	public AudioClip somCarta;
	public int valorBase;
	public int contJogada;
	public int[] baralho;
	public Sprite verso;
	public Sprite coringa;

	void Start ()
	{		

		recursoSomCarta = GetComponent<AudioSource>();
		recursoSomCarta.clip = somCarta;
        
		valorBase = 0;
		contJogada = 0;
		baralho = new int[level];
		for (int i = 0; i < baralho.Length; i++) baralho[i] = 100;

		Random.InitState(System.Environment.TickCount);
		int pos = Random.Range(0,level);
        
		for(int i = 0; i < level; i++)
		{
			botoes[i].onClick.AddListener(ExecutarAcaoCarta); 
		}
        
		for (int i = 0; i < level; i++)
		{
			while (((IList) baralho).Contains(pos))
			{
				pos = Random.Range(0,level);
			}
		
			if (botoes[i] != null)
			{
				int index = new int();
				index = i;
				// botoes[i].onClick.AddListener(() => LogicMainGame(index)); 
				botoes[i].onClick.AddListener(ExecutarAcaoCarta); 
		
				baralho[i] = pos;
			}
		}
	
	}
	
	public void LogicMainGame(int botao)
	{
		recursoSomCarta.Play();

		Debug.Log(baralho[botao]);

		botoes[botao].image.sprite = imageCards[baralho[botao]];
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
			if (valorBase == level)
			{
				level += 2;
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
		rodadaText.text = "Rodada: " + level;
	}
	
	public void ExecutarAcaoCarta(){
		Debug.Log("aquiii");

		recursoSomCarta.Play();
	}


	IEnumerator esconderCarta(int carta)
	{
		yield return new WaitForSeconds(1f);
		botoes[carta].image.sprite = verso;
	}

	IEnumerator pausarPorUmTempoReiniciar()
	{
		level = 0;
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
    
	

}
