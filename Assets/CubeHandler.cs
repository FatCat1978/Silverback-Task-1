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

	[SerializeField] bool active = false;


	private Rigidbody rigidBody;
	private MeshRenderer Renderer;

	// Start is called before the first frame update
	void Start()
	{
		Renderer = GetComponent<MeshRenderer>();
		SetMaterialToActiveState();
		//get the main cube controller
	}

	private void SetMaterialToActiveState()
	{
		if(active)
			Renderer.material = selectedMaterial;
		else
			Renderer.material = inactiveMaterial;
		
	}

    //Editor Fluff
    private void OnValidate()
    {
		SetMaterialToActiveState();
    }

    // Update is called once per frame
    void Update()
	{
		
	}
}
