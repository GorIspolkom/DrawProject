using Assets.Scrypts.GameData;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResetData : MonoBehaviour
{
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(() => Profile.Reset());   
    }
}
