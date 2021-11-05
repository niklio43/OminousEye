using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class ButtonManager : MonoBehaviour
{


    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        Debug.Log("HEHEHEH");
#else
        Application.Quit ();
#endif
    }

    public void NextScene()
    {
        SceneManager.LoadScene("characterAdded");
    }



}
 
