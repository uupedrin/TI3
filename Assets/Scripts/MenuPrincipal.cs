using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPrincipal : MonoBehaviour
{
	[SerializeField] private GameObject painelMenuInicial;
	[SerializeField] private GameObject painelOpcoes;
	[SerializeField] private GameObject painelGraficos;
	[SerializeField] private GameObject painelConfig;
	[SerializeField] private GameObject painelAudio;

	public void LoadScene(string scene)
	{
		SceneManager.LoadScene(scene);
	}
	public void AbrirOpcoes()
	{
		painelMenuInicial.SetActive(false);
		painelOpcoes.SetActive(true);
	}
	public void FecharOpcoes()
	{
		painelMenuInicial.SetActive(true);
		painelOpcoes.SetActive(false);
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
	public void Quit()
	{
		Application.Quit();
	}
}