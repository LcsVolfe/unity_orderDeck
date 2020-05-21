using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{

    public string scene = "index";
    public Button[] botoes; 

    void Start ()
    {
        for(int i = 0; i < botoes.Length; i++)
        {
            int index = i;
            botoes[i].onClick.AddListener(() => ButtonClicked(index)); 
        }
	
    }

    private void ButtonClicked(int i)
    {
        if (i == 0) chageScene("levelOne");
        else if (i == 1) chageScene("Instructions");
        else if (i == 2) Application.Quit();
    }

    public void chageScene(string scene)
    {
        SceneManager.LoadScene(scene);
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().buildIndex);
    }

    
}