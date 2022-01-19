using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Gameover : MonoBehaviour
{
    public Animator transition;
    string level;
    void Start()
    {
        level = PlayerPrefs.GetString("LastLevel");
    }

    // Update is called once per frame
    void Update()
    {
        
 
    
    
    }


    void OnMouseDown()
    {
        
       
        StartCoroutine(WaitForAnimation());
    }

    IEnumerator WaitForAnimation()
    {
        
        transition.SetTrigger("Start");
        FindObjectOfType<AudioManager>().musicTransition();
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene("Loading");
    }
}
