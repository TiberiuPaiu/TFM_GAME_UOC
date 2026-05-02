using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class ControladorPausa : MonoBehaviour
{
	public GameObject panelPausa; // UI del menú pausa
    public GameObject panelControles;
    private bool estaPausado = false;

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			//Debug.Log("Saliendo del juego...");
			if (estaPausado)
				Play();
			else
				Pausar();
		}

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (estaPausado)
                Play();
            else
                Control();
        }
    }

	public void Pausar()
	{
		panelPausa.SetActive(true);
		Time.timeScale = 0f;
		estaPausado = true;
	}

    public void Control()
    {
        panelControles.SetActive(true);
        Time.timeScale = 0f;
        estaPausado = true;
    }

    public void Play()
	{
		panelPausa.SetActive(false);
        panelControles.SetActive(false);
        Time.timeScale = 1f;
		estaPausado = false;
	}


	public void Restart()
	{
        panelPausa.SetActive(false);
        Time.timeScale = 1f;
        estaPausado = false;
        Destroy(GameManager.Instance.gameObject);
        SceneManager.LoadScene(1);
	}
	

	public void Exit()
	{
		Debug.Log("Saliendo del juego...");
		Application.Quit(); 
	}
}