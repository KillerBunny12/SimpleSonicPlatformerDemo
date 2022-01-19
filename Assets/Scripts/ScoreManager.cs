using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    public TextMeshProUGUI text;
    int score;
    
    
    // Start is called before the first frame update
    void Start()
    {
        GameObject[] Coins = GameObject.FindGameObjectsWithTag("Coin");


        if (instance == null)
        {
            instance = this;
        }

        score = Coins.Length;
        Debug.Log(score);
        text.text = "x" + score.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeScore(int coinValue)
    {
        score -= coinValue;
        text.text = "x" + score.ToString();
    }
}
