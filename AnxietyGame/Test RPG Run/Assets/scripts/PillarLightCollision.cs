using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PillarLightCollision : MonoBehaviour
{
    // Start is called before the first frame update
    public GameManager gameManager;
    public GameObject pillar;
    //public Light2D light2D;

    public void OnTriggerExit2D(Collider2D collider)
    {

        Debug.Log("There has been a collision!");

        if (collider.gameObject.CompareTag("Player"))
        {

            Debug.Log(collider.name + "has left the ring of light and it should now deactivate!");
            if (gameObject.GetComponentInParent<LightPillars>().pillarGameObjectLit != null)
            {
                gameManager.LightsOnPuzzleObjects.Remove(gameObject.GetComponentInParent<LightPillars>().pillarGameObjectLit);
                gameObject.GetComponentInParent<LightPillars>().pillarGameObjectLit = null;
            }
            if (pillar.TryGetComponent(out Light2D lightComponent))
            {
                pillar.GetComponentInParent<Light2D>().enabled = false;

            }

        }

    }
}
