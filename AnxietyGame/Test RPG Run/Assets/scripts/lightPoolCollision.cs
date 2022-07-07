using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
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

        if (collider.gameObject.CompareTag("Player"))
        {
             gameObject.SetActive(false);
        }

    }

}
