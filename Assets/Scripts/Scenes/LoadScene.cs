using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    [field: SerializeField]
    public int SceneToLoadIndex
    {  get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadMainSceneIndex()
    {
        SceneManager.LoadScene(SceneToLoadIndex);
    }
}
