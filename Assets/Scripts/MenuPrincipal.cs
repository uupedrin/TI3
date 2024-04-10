using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPrincipal : MonoBehaviour
{
    [SerializeField] private string nomeDoLevel;
    [SerializeField] private string telaMenu;
    [SerializeField] private string telaCreditos;
    [SerializeField] private GameObject painelMenuInicial;
    [SerializeField] private GameObject painelOp�oes;
    [SerializeField] private GameObject painelGraficos;
    [SerializeField] private GameObject painelConfig;
    [SerializeField] private GameObject painelAudio;

    public void jogar()
    {
        SceneManager.LoadScene("City");
    }

    public void Menu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void creditos()
    {
        SceneManager.LoadScene("credits");
    }

    public void AbrirOp�oes()
    {
        painelMenuInicial.SetActive(false);
        painelOp�oes.SetActive(true);
    }

    public void FecharOp�oes()
    {
        painelMenuInicial.SetActive(true);
        painelOp�oes.SetActive(false);
    }

    public void AbrirConfig()
    {
        painelGraficos.SetActive(false);
        painelAudio.SetActive(false);
        painelConfig.SetActive(true);
    }

    public void AbrirGraficos()
    {
        painelGraficos.SetActive(true);
        painelAudio.SetActive(false);
        painelConfig.SetActive(false);
    }
    public void AbrirAudio()
    {
        painelGraficos.SetActive(false);
        painelAudio.SetActive(true);
        painelConfig.SetActive(false);
    }
    public void SairJogo()
    {
        Application.Quit();
    }
}
