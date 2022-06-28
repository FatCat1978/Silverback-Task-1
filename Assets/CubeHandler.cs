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


	Vector3 originalPosition; // where the box is returned to after falling for 7 seconds.

	private Rigidbody rigidBody;
	private MeshRenderer Renderer;
	private CubeController controller;
	// Start is called before the first frame update
	void Start()
	{
		Renderer = GetComponent<MeshRenderer>();
		SetMaterialToActiveState();
		rigidBody = GetComponent<Rigidbody>();
		//get the main cube controller, add ourselves to it if we aren't in there already.

		controller = GameObject.Find("CubeManager").GetComponent<CubeController>(); //Just so we have it.
		if(!controller.allCubes.Contains(this.gameObject))
			controller.allCubes.Add(this.gameObject);

		originalPosition = this.transform.position;
		originalPosition.y += 5;

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

	// Update is called once per frame, it's used to determine when the cube's fallen off the board, and return it after 7 seconds. pretty simple stuff

	private bool falling = false;
	private float secondsSinceFall = 0;
    void Update()
	{
		if(transform.position.y < 0 && !falling)
        {
			falling = true;
        }
		if (falling)
			secondsSinceFall += Time.deltaTime;

		if(secondsSinceFall >= 7)
        {
			falling = false;
			transform.position = originalPosition;
			rigidBody.velocity = Vector3.zero; //reset it's velocity, otherwise it'll just phase through the plane.
			secondsSinceFall = 0;
        }
		
	}
}
