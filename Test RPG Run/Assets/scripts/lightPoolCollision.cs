using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEditor.Experimental.SceneManagement;
using UnityEngine.Rendering.Universal;

public class lightPoolCollision : MonoBehaviour
{
    public GameManager gameManager;
    //public Light2D light2D;

    public void Awake()
    {
        //light2D = gameObject.GetComponent<Light2D>();

    }


    public void OnTriggerExit2D(Collider2D collider)
    {

        Debug.Log("There has been a collision!");

        if (collider.gameObject.CompareTag("Player"))
        {

            Debug.Log(collider.name + "has left the ring of light and it should now deactivate!");
            gameManager.LightsOnPuzzleObjects.Remove(gameObject.GetComponentInParent<LightPillars>().pillarGameObjectLit);
            if (gameObject.GetComponentInParent<Light2D>())
            {
                gameObject.GetComponentInParent<Light2D>().enabled = false;
            }
            else
            {
                gameObject.SetActive(false);
            }


        }

    }

}
