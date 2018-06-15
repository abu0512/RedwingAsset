using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LobbyScene : MonoBehaviour {

	public void AnyKeyDown()
    {
        if (Input.anyKey)
        {
            SceneManager.LoadSceneAsync("NYR(MergeOnly)");
        }

    }
	
	// Update is called once per frame
	void Update () {
        AnyKeyDown();
    }
}
