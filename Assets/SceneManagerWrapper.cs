using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerWrapper : MonoBehaviour
{
	// Start is called before the first frame update
	[SerializeField] private string targetSceneName; //what scene are we loading upon the "activate"?

	//void Start()
	//{ Normally for a "Manager Holder" Like this, I'd make DontDestroyOnLoad it, so it can carry things like audio etc over, haven't really had a proper chance to do that yet, though
	//...so I don't know if that's a good practice or not. I think it's better than having a duplicate "ManagerHolder" for every scene though.
	//but, for a project of this size, it's really not mandatory.
		
	//}

	//needs to be public to show up in the button's event selector.
	public void LoadTargetScene() //simple as it can get, effectively just call the actual SceneManager's LoadScene. don't need to specify
	{//any special mode or anything like that.
		print("Loading Scene!");
		try
        {
			SceneManager.LoadScene(targetSceneName);
        }
		catch (Exception E) //sanity checking.
        {
			print(E.Message);
        }
	}
}
