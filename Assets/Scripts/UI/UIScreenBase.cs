using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class UIScreenBase : MonoBehaviour
{
    public virtual void Initialize()
    {

    }

    public void QuitButton()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}
