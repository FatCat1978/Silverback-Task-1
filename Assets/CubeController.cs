using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeController : MonoBehaviour
{
	[SerializeField] public List<GameObject> allCubes;  
	private List<GameObject> inactiveCubes = new List<GameObject>(); //these need to be initialized, allcubes doesn't
	private List<GameObject> activeCubes = new List<GameObject>();

    [Tooltip("It wasn't quite clear if you wanted the cube selection to be exclusive (ae, normally one able to be selected at a time), or inclusive, (ae, click to toggle individual), so I just supported both, shouldn't take any extra time.")]
	[SerializeField] bool exclusiveCubeSelection = false; //false by default though



    private void Start() //called once!
    {
     //we iterate over every cube in AllCubes, and add them to inactive or active cubes depending on whether
	 //they're active or inactive according to their individual controller.   

		foreach (GameObject cube in allCubes)
        {
			//we check for the Cubehandler
			CubeHandler CH = cube.GetComponent<CubeHandler>();
			if(CH == null) //not a valid cube!
            {
				print("Invalid Cube: " + cube.name + " found. deleting it!"); //soft sanity check, considering the cubes get auto added.
				allCubes.Remove(cube);
				GameObject.Destroy(cube);
            }
			else
            {
				if(CH.activeSelection)
					activeCubes.Add(cube);
				else
					inactiveCubes.Add(cube);
            }
        }
    }

    // Update is called once per frame
    void Update()
	{
		if (Input.GetMouseButtonDown(0)) //if we're clicking
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); //make a ray, cast it out
			RaycastHit hit;
			// Casts the ray and get the first game object hit
			Physics.Raycast(ray, out hit);

			GameObject foundObject = hit.rigidbody.gameObject;

			Debug.Log("This hit" + foundObject.name);
			if (foundObject != null) //sanity check
            {
				if(allCubes.Contains(foundObject))
                {
					ToggleCubeActivation(foundObject);
                }
            }
		}
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
