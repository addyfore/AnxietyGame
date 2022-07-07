using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;

public class LightPillars : MonoBehaviour
{

	public GameManager gameManager;
	public GameObject pillarGameObjectLit;

	public void BrightHitPillar(GameObject pillarGameObject)
    {

		pillarGameObjectLit = pillarGameObject;
        gameManager.LightsOnPuzzleObjects.Add(pillarGameObjectLit);

	}

//constructor
/*
	public LightPillars()
	{
	

	
	}
*/
}