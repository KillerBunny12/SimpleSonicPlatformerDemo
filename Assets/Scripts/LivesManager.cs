using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class LivesManager : MonoBehaviour
{
    public string level;
    public static LivesManager instance;
    public TextMeshProUGUI text;
   public int lives = 3;
    GameObject other;
    public Animator transition;
    public AudioSource musica;
    
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }

        
        level = SceneManager.GetActiveScene().name;
        

        text.text = lives.ToString();


    }

    //Lose a life
    public void loselife()
    {
        if(lives == 0)
        {
            //DEAD
            //TODO
            FindObjectOfType<PMovement>().Die();
            PlayerPrefs.SetString("LastLevel", level);
            StartCoroutine(WaitForAnimation());
            
            
        }
        else
        {
            lives--;
            text.text = lives.ToString();
        }
        
    }


    IEnumerator WaitForAnimation()
    {
        yield return new WaitForSeconds(2f);
        transition.SetTrigger("Start");
        FindObjectOfType<AudioManager>().musicTransition();
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene("GameOver");
    }

    public int getcurrentlives()
    {
        return lives;
    }
}
