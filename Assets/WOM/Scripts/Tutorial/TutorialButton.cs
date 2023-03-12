using SRDebugger;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.Accessibility;
using UnityEngine.UI;


public class TutorialButton : MonoBehaviour
{
    public int id;
    public Image image;
    TutorialManager tutorialManager;

    void Start()
    {
        GetManager();
    }

    void GetManager()
    {
        if (tutorialManager == null)
            tutorialManager = FindAnyObjectByType<TutorialManager>();
    }
}
