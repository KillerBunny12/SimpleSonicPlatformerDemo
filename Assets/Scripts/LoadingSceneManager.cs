using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingSceneManager : MonoBehaviour
{
    public Animator transition;
    public float WaitTime = 2f;

    private string scene;
    // Start is called before the first frame update
    void Start()
    {
        scene = PlayerPrefs.GetString("LastLevel");
        StartCoroutine(LoadScene());
    }

    // Update is called once per frame
    

    IEnumerator LoadScene()
    {
        yield return new WaitForSeconds(WaitTime);
        AsyncOperation asyncload = SceneManager.LoadSceneAsync(scene);
        asyncload.allowSceneActivation = false;

        while (!asyncload.isDone)
        {

            if (asyncload.progress >= 0.9f)
            {
                transition.SetTrigger("Start");
                yield return new WaitForSeconds(1.5f);
                asyncload.allowSceneActivation = true;
            }
            yield return null;



            
        }
    }
}
