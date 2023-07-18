using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Mathematics;
using NUnit.Framework;
using System.Linq;

public class SpellProjectileMovement : MonoBehaviour
{

    public GameObject parentSpell;
    public Vector3 spellStartPoint;
    public GameObject spellEndPoint;
    public GameObject spellCaster;


    public Vector3 direction;
    public Vector2 casterTransform;
    public Vector3 point;
    public float rayCastDistance;

    public Animator fizzleSpellAnim;
    public GameObject fizzleSpell;
    public Image spellIconMask;

    //public float fizzleCastDowntime;
    public Animator animator;
    public SpellTemplate spellTemplate;
    public SpellHolder spellHolder;
    public AudioSource spellSound;

    public GameObject hitObject;
    public enum LayersInMask {Enemy, Scenery, NPC, Player}
    [SerializeField]
    public List<LayersInMask> layerMasksToHit;
    [SerializeField]
    public LayerMask layerMask;
    public int positionInLayerList;
    public String[] combinedString;
    public float damageAmount;
    public float maxDistance;

    RaycastHit2D hit;

    //make enum that allows for string values to be placed in a layermask.

    // Start is called before the first frame update
    void Awake()
    {
        //Debug.Log("Spell now at " + gameObject.transform.position + " which should be the same as " + hit.transform.position + ".");

        /*if (hitObject.CompareTag("Enemy"))
        {

            hitObject.GetComponent<NPCHealth>().damageNPCHealth(damageAmount);

        }

        else if (hitObject.CompareTag("Scenery"))
        {


        }
        else
        {
            //Debug.Log("this is the statement appearing when a collider not on the layermask gets hit, it should never show up.");
            //Debug.Log("hit.name because something weird happened : " + hit.collider.name + ".");
        }
        */
        positionInLayerList = 0;
        combinedString = new string[layerMasksToHit.Count];

        foreach (var layer in layerMasksToHit)
        {
            combinedString[positionInLayerList] = layer.ToString();
            positionInLayerList++;
        }
        layerMask = LayerMask.GetMask(combinedString);
        direction = spellTemplate.direction;
        hit = Physics2D.Raycast(transform.position, direction, rayCastDistance, layerMask);
        spellSound = gameObject.GetComponent<AudioSource>();
        //Debug.Log("hit.point = " + hit.point + ".");
        
        //below needs to happen with collision.
        spellSound.Play();
        if (hit.collider)
        {

            gameObject.GetComponent<Animator>().SetBool("projectileIsMoving", true);
            spellStartPoint = spellCaster.transform.position + direction;
            transform.rotation.SetFromToRotation(spellStartPoint, hit.transform.position);




            //Debug.Log("Spell now at " + gameObject.transform.position + " which should be the same as " + hit.transform.position + ".");
            /*
            if (hitObject.CompareTag("Enemy") || hitObject.CompareTag("NPC"))
            {

                hitObject.GetComponent<NPCHealth>().damageNPCHealth(damageAmount);

            }

            else if (hitObject.CompareTag("Scenery"))
            {


            }
            else if (hitObject.CompareTag("Player"))
            {
                hitObject.GetComponent<Player>().PlayerHealth(damageAmount);
            }
        else

        {
            //Debug.Log("heroTransform (" + heroTransform + ") and mousePos (" + mousePos + ")");
            point = new Vector3(heroTransform.x + (direction.x * rayCastDistance), heroTransform.y + (direction.y * rayCastDistance), 0f);
            //Debug.Log("point now converted to heroTransform + direction * rayCastDistance and located at " + point + ".");

            player.fizzleSpell.transform.position = point;
            player.fizzleSpellAnim.SetBool("isCasting", true);



           // Debug.Log("Nothing hit. Fizzlespell activating and moved to " + point);

        }

            */

        }
    }
    // Update is called once per frame

}
