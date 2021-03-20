using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Scoreboard : MonoBehaviour
{
    private int score;
    private TMP_Text scoreText;
    private void Start() 
    {
        scoreText = GetComponent<TMP_Text>();
        scoreText.text = "No Points Yet";
        
    }

    public void UpdateScore(int amountToIncrease)   
    {
        score += amountToIncrease;
        scoreText.text = score.ToString();
    }
}
