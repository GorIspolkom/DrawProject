using Assets.Scrypts.LevelManagerSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadNextLevel : MonoBehaviour
{
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(() =>
            GameObject.FindObjectOfType<LevelManager>().LoadNextLevel());
    }
}
