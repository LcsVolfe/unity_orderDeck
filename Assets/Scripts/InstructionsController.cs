using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InstructionsController : MonoBehaviour
{
    
    public Button botaoMenu;

    void Start()
    {
        
        botaoMenu.onClick.AddListener(() => SceneManager.LoadScene("index"));
    }

    void Update()
    {
        
    }
}
