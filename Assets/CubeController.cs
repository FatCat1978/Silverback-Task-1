using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeController : MonoBehaviour
{
	[SerializeField] List<GameObject> allCubes; 
	private List<GameObject> inactiveCubes;
	private List<GameObject> activeCubes;

    [Tooltip("It wasn't quite clear if you wanted the cube selection to be exclusive (ae, normally one able to be selected at a time), or inclusive, (ae, click to toggle individual), so I just supported both, shouldn't take any extra time.")]
	[SerializeField] bool exclusiveCubeSelection = false; //false by default though



    private void Start() //called once!
    {
     //we iterate over every cube in AllCubes, and add them to inactive or active cubes depending on whether they're active or inactive according to their individual controller.   
    }

    // Update is called once per frame
    void Update()
	{
		
	}

	void ToggleCubeActivation(GameObject toToggleCube)
    {

		if(inactiveCubes.Contains(toToggleCube))
        { //we can only activate it if it's active!
			inactiveCubes.Remove(toToggleCube);
			activeCubes.Add(toToggleCube);
			return; //we're done here.
        }
		else
        { //if it's active, deactivate it.
			if (activeCubes.Contains(toToggleCube))
            {
				activeCubes.Remove(toToggleCube);
				inactiveCubes.Add(toToggleCube);
				return; //done here too.
            }
        }

		//sanity checking.
		print("Warning! GameObject " + toToggleCube.name + " is in neither inactive cubes, or active cubes! somehow!");
		
		

    }


}
