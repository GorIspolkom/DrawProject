using Assets.Scrypts.LevelManagerSystem;
using UnityEngine;
using UnityEngine.UI;

public class LoadFirstLevel : MonoBehaviour
{
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(() =>
            GameObject.FindObjectOfType<LevelManager>().LoadFirstLevel());
    }
}
