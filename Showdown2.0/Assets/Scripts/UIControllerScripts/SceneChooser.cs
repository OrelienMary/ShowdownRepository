using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChooser : MonoBehaviour
{
    public void Choose2()
    {
        SceneManager.LoadScene(1);
    }

    public void Choose3()
    {
        SceneManager.LoadScene(2);
    }

    public void Choose4()
    {
        SceneManager.LoadScene(3);
    }
}
