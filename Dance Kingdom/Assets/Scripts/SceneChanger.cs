using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

//Class SceneChanger, to change between scenes.
public class SceneChanger : MonoBehaviour
{
    public GameObject fadeImage;
    public static SceneChanger instance;
    private string sceneName;

    void Start()
    {
        instance = this;
    }

    //Change to the scene with the name.
    public void ChangeScene(string name)
    {
        fadeImage.SetActive(true);
        fadeImage.GetComponent<Animator>().SetTrigger("FadeOut");
        sceneName = name;
        Time.timeScale = 1;
    }

    public void OnFadeComplete()
    {
        StartCoroutine(LoadAsynchronously(sceneName));
    }

    IEnumerator LoadAsynchronously (string name)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(name);

        while (!operation.isDone)
        {
            //Debug.Log(operation.progress);
            yield return 0;
        }
    }
}
