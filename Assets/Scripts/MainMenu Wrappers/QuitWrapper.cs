using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class QuitWrapper : MonoBehaviour
{
  //doesn't do much, but it has a completely different goal when compared to SceneManagerWrapper, so it's a different class.
  //in a full game/application, I would make this do sanity checks to make sure everything is saved properly, but for this..
  //no need!

	//still public for the button
	public void StartGameQuit()
	{
		print("Quitting game.");
		Application.Quit(); //as far as I know, only works inside a compiled exe.
		EditorApplication.ExecuteMenuItem("Edit/Play"); //for the editor, then.
	}

}
