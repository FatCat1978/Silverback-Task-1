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


	[Range(0, 100)] //as a baseline. negative speed would be possible but unintuitive, feel free to get rid of this if you want to mess around with it.
	[SerializeField] float movementSpeed;	

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
		//Raytracing - for toggling individual cubes on a per-click basis.
		if (Input.GetMouseButtonDown(0)) //if we're clicking, we use this to toggle.
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); //make a ray, cast it out
			RaycastHit hit;
			// Casts the ray and get the first game object hit
			Physics.Raycast(ray, out hit);
			GameObject foundObject = null;
			if (hit.rigidbody != null)
				foundObject = hit.rigidbody.gameObject;
						
			if (foundObject != null) //sanity check
            {
				Debug.Log("This hit: " + foundObject.name);
				if (allCubes.Contains(foundObject))
                {
					//if the cube we're selecting is inactive, make sure every other cube is inactive first!
					CubeHandler CH = foundObject.GetComponent<CubeHandler>();
					if (CH.activeSelection == false && exclusiveCubeSelection)
					{
						ToggleAllIn(activeCubes);
					}
					ToggleCubeActivation(foundObject);
                }
            }
		}

		//keyboard input: M - toggles all to on.
		if(Input.GetKeyDown(KeyCode.M))
        {
			print("Toggling all cubes to \"Active\". ");
			ToggleAllIn(inactiveCubes);
        }

		//the rest of the inputs use getAxisRaw. Cube Movement time!
		float HorizontalAxisPos = Input.GetAxisRaw("Horizontal");
		float VerticalAxisPos = Input.GetAxisRaw("Vertical"); //this should probably work with controllers, too, now that I think about it.

		Vector3 MoveVect = new Vector3(HorizontalAxisPos, 0, VerticalAxisPos);
		MoveVect = MoveVect.normalized * movementSpeed * Time.deltaTime; //basic Vector stuff, move it in the dir of the axis based on the speed var. Deltatime prevents it from pissing off at mach speeds.

		foreach (GameObject cube in activeCubes) //only move the active ones.
        {
			Rigidbody toMove = cube.GetComponent<Rigidbody>(); //not a fan of doing getComponent in a loop like this potentially every frame, I could probably cache these but there's an extent where the extra complexity isn't worth.
			toMove.MovePosition(cube.transform.position + MoveVect);
        }

	}

	void ToggleCubeActivation(GameObject toToggleCube)
    {
		CubeHandler CH = toToggleCube.GetComponent<CubeHandler>(); //there has to be a better way of doing this, but I don't know it.
																   //I do know that getComponent's pretty expensive. this isn't an every frame thing, though, so it's probably not that bad.

		CH.activeSelection = !CH.activeSelection; //invert it's active state
		CH.SetMaterialToActiveState(); //and refresh the material so it looks right.


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
		print("Warning! GameObject " + toToggleCube.name + " is in neither inactive cubes, or active cubes!");

    }

	private void ToggleAllIn(List<GameObject> toToggle) //a little janky. not a fan of this.
    {
		foreach(GameObject cube in new List<GameObject>(toToggle))
        {
			ToggleCubeActivation(cube);
        }
    }



}
