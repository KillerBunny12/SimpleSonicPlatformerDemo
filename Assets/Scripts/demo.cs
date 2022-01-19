using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class demo : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetString("LastLevel", "GrassLandZone");

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


        FindObjectOfType<AudioManager>().musicTransition();
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene("Loading");
    }
}
