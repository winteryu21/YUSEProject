using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void test_Go_InGame()
    {
        SceneManager.LoadScene("InGame");
    }

    public void test_Go_Main()
    {
        SceneManager.LoadScene("Main");
    }
}