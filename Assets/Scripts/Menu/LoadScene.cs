using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadScene : MonoBehaviour {

    [Tooltip("The name of the scene that will be loaded upon pressing the button")]
    [SerializeField]
    private string sceneToLoad;

    private Button button;

	void Start () 
	{
        button = transform.GetComponent<Button>();
        button.onClick.AddListener(Load);
    }

    void Load()
    {
        Destroy(GameObject.Find("WwiseGlobal"));
        SceneManager.LoadScene(sceneToLoad);
    }

}
