using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] int puntosdevida;
    int count= 0 ;
    bool isSlow = false;
    bool gamePaused = false;
    bool gameOver = false;
    bool presionoBoton = false;
    private float secondsTime = 60f;
    private float secondsSlow = 1.5f;
    private int anteriorpuntosvida;
    [SerializeField] Text timerText;
    [SerializeField] Spaceship player;
    [SerializeField] GameObject pauseUI;
    [SerializeField] GameObject gameOverUI;
    [SerializeField] int numEnemies;


    // Start is called before the first frame update
    void Start()
    {
        pauseUI.SetActive(false);
        gameOverUI.SetActive(false);
        Animal.puntosDeVida = puntosdevida; 
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.P) && gameOver == false)
            PauseGame();
        if (Input.GetKeyDown(KeyCode.T))
            presionoBoton = true;

        ShowTime();
        SlowGame();
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
        Time.timeScale = 1;

    }
    
    public void ChangeScene(string escena)
    {
        SceneManager.LoadScene(escena);
        Time.timeScale = 1;
    }

    void PauseGame()
    {
        gamePaused = gamePaused ? false : true;

        player.gamePaused = gamePaused;
        
        pauseUI.SetActive(gamePaused);

        Time.timeScale = gamePaused ? 0 : 1;
    }
    void SlowGame()
    {
        if (presionoBoton == true)
        {
            if (isSlow != true)
            {
                count++;
                if (count <= 3)
                {
                    presionoBoton = true;
                    isSlow = true;
                    secondsSlow = 1.5f;
                }
                else
                {
                    Debug.Log("Limite de intentos alcanzado");
                }
            }
            else
            {
                if (presionoBoton == true)
                {
                    if (secondsSlow <= 0)
                    {
                        presionoBoton = false;
                        isSlow = false;
                        secondsSlow = 0;
                        Time.timeScale = 1;
                        Animal.puntosDeVida = anteriorpuntosvida; 
                    }
                    else
                    {
                        secondsSlow = secondsSlow - Time.deltaTime;
                        Time.timeScale = 0.5f;
                        anteriorpuntosvida = Animal.puntosDeVida; 
                        Animal.puntosDeVida = 1;

                    }
                }
            }
        }
        else
        {
            Time.timeScale = 1;
        }

    }
    void ShowTime()
    {
        secondsTime = secondsTime - Time.deltaTime;
        if(secondsTime <= 0)
        {
            Perder();
        }
        timerText.text = secondsTime.ToString("f0");
    }
    public void ReducirNumEnemigos()
    {
        numEnemies = numEnemies - 1;
        if(numEnemies < 1)
        {
            Perder();
        }
    }

    void Perder()
    {
        gameOver = true;
        Time.timeScale = 0;
        player.gamePaused = true;
        gameOverUI.SetActive(true);
    }
}
