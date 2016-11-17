using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Threading;

public class GoToMenu : MonoBehaviour {

    void Start()
    {
        //Thread t = new Thread(Thread);
        //t.Start();
        LoadItemsFromDatabase.getItemsArray();
    }

    private void Thread()
    {
    }

    public void changeScence(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
