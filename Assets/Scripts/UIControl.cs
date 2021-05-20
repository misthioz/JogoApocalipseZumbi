using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIControl : MonoBehaviour
{
    //private PlayerControl scriptPlayer;
    private PlayerControl scriptplayer;
    private GunControl scriptGun;
    public Slider SliderHP;
    public GameObject gameOverPanel;
    public Text textSurvivalTime;
    public Text textRecorde;
    public Text textoQtdZumbisMortos;

    public Text textQtdChefesMortos;
    public Text textQtdBalas;
    public Text textAvisoChefe;
    private float timeRecord;

    private int qtdZumbisMortos;
    private int qtdChefesMortos;

    private int qtdTotalBalas, qtdBalas;
    
    void Start()
    {
        scriptplayer = GameObject.FindWithTag("Player").GetComponent<PlayerControl>();
        scriptGun = GameObject.FindObjectOfType(typeof(GunControl)) as GunControl;

        SliderHP.maxValue = scriptplayer.status.health;
        SliderHP.minValue = 0;
        Time.timeScale = 1;
        timeRecord = PlayerPrefs.GetFloat("TimeRecord");

        textQtdBalas.text = string.Format("x {0}", scriptGun.currentAmmo);
    }
    public void AtualizaSliderHP()
    {
        SliderHP.value = scriptplayer.status.health;
    }

    public void AtualizaQtdZUmbisMortos()
    {
        qtdZumbisMortos++;
        textoQtdZumbisMortos.text = string.Format("x {0}", qtdZumbisMortos);
    }

    public void AtualizaQtdChefesMortos()
    {
        qtdChefesMortos++;
        textQtdChefesMortos.text = string.Format("x {0}", qtdChefesMortos);
    }

    public void AtualizaQtdBalas()
    {
        qtdBalas = scriptGun.currentAmmo;
        textQtdBalas.text = string.Format("x {0}", qtdBalas);
    }
    
    public void GameOver()
    {
        gameOverPanel.SetActive(true);
        Time.timeScale = 0;

        int minutos = (int)(Time.timeSinceLevelLoad/60);
        int segundos = (int)(Time.timeSinceLevelLoad % 60);

        textSurvivalTime.text = "Você sobreviveu por " + minutos + " min e " + segundos + " segundos";

        SetRecordTime(minutos, segundos);
    }

    void SetRecordTime(int min, int seg)
    {
        if(Time.timeSinceLevelLoad > timeRecord)
        {
            timeRecord = Time.timeSinceLevelLoad;
            textRecorde.text = string.Format("Seu recorde é {0}min e {1}segundos", min, seg);

            PlayerPrefs.SetFloat("TimeRecord", timeRecord);
        }

        if(textRecorde.text == "")
        {
            min = (int)timeRecord/60;
            seg = (int)timeRecord%60;
            textRecorde.text = string.Format("Seu recorde é {0}min e {1}segundos", min, seg);
        }
    }
    
    public void Restart()
    {
        SceneManager.LoadScene("game");
    }

    public void ApareceTextoChefe()
    {
        StartCoroutine(DesaparecerText(2, textAvisoChefe));
    }
    
    IEnumerator DesaparecerText(float tempo, Text texto)
    {
        texto.gameObject.SetActive(true);
        yield return new WaitForSeconds(tempo);
        //nao pode mexer direto no alpha ent precisa criar variaveis    necessario garantir q ao mostarr o texto, o alpha é 1
        //vamos fazre o text desaparecer diminuindo sua opacidade baseado no tempo para desaparecimento

        Color corTexto = texto.color;
        corTexto.a = 1;
        texto.color = corTexto;
        
        float contador = 0;
        while(texto.color.a>0)
        {
            contador+=Time.deltaTime/tempo;
            corTexto.a = Mathf.Lerp(1, 0, contador); //troca gradativamente de 1 a 0 baseado no contador
            texto.color = corTexto;

            if(corTexto.a<=0)
            {
                texto.gameObject.SetActive(false);
            }
            yield return null;
        }
    }
}
