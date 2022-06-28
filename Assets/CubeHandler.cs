using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(MeshRenderer))]
public class CubeHandler : MonoBehaviour
{
	[SerializeField] Material selectedMaterial; //what do we look like when selected? red.
	[SerializeField] Material inactiveMaterial; //what about when we're inactive??

	[SerializeField] public bool activeSelection = false; //needs to be public. cubecontroller checks this to organize us properly.


	private Rigidbody rigidBody;
	private MeshRenderer Renderer;
	private CubeController controller;
	// Start is called before the first frame update
	void Start()
	{
		Renderer = GetComponent<MeshRenderer>();
		SetMaterialToActiveState();
		//get the main cube controller, add ourselves to it if we aren't in there already.

		controller = GameObject.Find("CubeManager").GetComponent<CubeController>(); //Just so we have it.
		if(!controller.allCubes.Contains(this.gameObject))
			controller.allCubes.Add(this.gameObject);

	}

	public void SetMaterialToActiveState() //also used by cubecontroller when we're clicked.
	{
		if(activeSelection)
			Renderer.material = selectedMaterial;
		else
			Renderer.material = inactiveMaterial;
		
	}

    //Editor Fluff
    private void OnValidate()
    {
		if (!Renderer)
			Renderer = GetComponent<MeshRenderer>();
		SetMaterialToActiveState();

	}

    // Update is called once per frame
    void Update()
	{
		
	}
}
