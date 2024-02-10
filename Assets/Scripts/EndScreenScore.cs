using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEditor;

public class EndScreenScore : MonoBehaviour
{
    
    //public GameObject textMeshObj;
    public TextMeshProUGUI currentScoreRef;
    public TextMeshProUGUI highScoreRef;
    private int currentScore;
    private int highScore;

    public TextMeshProUGUI music;
    public TextMeshProUGUI sfx;

    
    // Start is called before the first frame update
    void Start()

    {
        Cursor.visible = true;
        //Text = FindObjectOfType<TextMeshProUGUI>();
        currentScore = PlayerPrefs.GetInt("coins");
        try{
            highScore = PlayerPrefs.GetInt("highCoin");

        }catch{
            highScore = currentScore;
        }
        
        
        currentScoreRef.text = $"x{currentScore}";
        
        if(currentScore > highScore){
            highScore = currentScore;
        }
        
        PlayerPrefs.SetInt("highCoin",highScore);
        
    }
    private void Update(){

        PlayerPrefs.SetInt("highCoin",highScore);
        highScoreRef.text = $"x{highScore}";
    }

    public void quit()
    {
        Application.Quit();
    }

    public void restartGame()
    {
        SceneManager.LoadScene("level_001");
    }

    public void resetHighScore()
    {
        highScore = 0;
        PlayerPrefs.SetInt("highCoin",highScore);
        highScoreRef.text = $"x{highScore}";
    }

    public void startGame()
    {
        SceneManager.LoadScene("level_00l");
    }


    public void MusicPressed()
    {
        if (BGMusic.Instance.music)
        {
            BGMusic.Instance.music = false;
            
            BGMusic.Instance.Stop();
            music.text = "Off";
        }
        else
        {
            BGMusic.Instance.music =true;
            BGMusic.Instance.Play();  
            music.text="On";
        }
    }

    public void SfxPressed()
    {
        if (BGMusic.Instance.sfx)
        {
            
            BGMusic.Instance.ChangeSfx(false);
            sfx.text = "Off";
        }
        else
        {
            
            BGMusic.Instance.ChangeSfx(true);
            sfx.text = "On";
        }
        
    }
 
}
