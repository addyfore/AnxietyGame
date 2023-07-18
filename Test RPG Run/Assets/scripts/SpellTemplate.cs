using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using Unity.Mathematics;

public class SpellTemplate : MonoBehaviour
{

    //what will this shit hit? Do we need this in template?
    //public ContactFilter2D contactFilter;
    //public float rayCastDistance;

    //Spells, can we work off of prefabs alone?
    //protected GameObject spellPrefab;

    public float damageAmount;
    public float manaCost;
    public float castDownTime;
    public float globalCastDownTime;
    public float currentCastDownTime;

    //public float fizzleGlobalCastDownTime;

    public GameObject hero;
    public Player player;

    public Vector2 mousePos;
    public Vector2 worldPos;
    public Vector3 direction;
    public Vector2 casterTransform;
    //public Vector3 point;
    public Vector3 spellStartPoint;

    public float mouseDistance;

    public Image spellIconMask;



    public Camera mainCam;

    //public GameObject hitObject;
    public GameObject projectilePrefab;
    public GameObject currentProjectile;
    public GameObject spellCaster;
    public SpellHolder spellHolder;


    //much of this needs to be ported over to SpellProjectileMovement for anything that is not hitscan.


    public virtual void Awake()
    {


        currentCastDownTime = 0f;



    }





    public virtual void Update()
    {
 
        if (currentCastDownTime > 0f)
        {
            currentCastDownTime -= Time.deltaTime;


        }
        else if (currentCastDownTime < 0f)
        {
            currentCastDownTime = 0f;

        }
    }





     public virtual void castSpell()
    {
        //int layerMask = LayerMask.GetMask("Scenery", "Enemy");
        //rawDirection = new Vector3(mousePos.x, mousePos.y, 0f) - tempHeroTransformValues;
        casterTransform = spellCaster.transform.position;
        direction = (mousePos - casterTransform).normalized;

        //Debug.Log("hero transform position = " + hero.transform.position);
        //Debug.Log("Camera.main.ScreenToWorldPoint(hero.transform.position) = " + Camera.main.ScreenToWorldPoint(hero.transform.position));
        //Debug.Log("heroTransform = " + heroTransform + ", direction = " + direction + ", mousePos = " + mousePos + ".");

        currentProjectile = Instantiate(projectilePrefab, spellStartPoint, quaternion.identity);
        var projectileScript = currentProjectile.GetComponent<SpellProjectileMovement>();
        projectileScript.spellTemplate = gameObject.GetComponent<SpellTemplate>();
        projectileScript.spellCaster = spellCaster;

        //spellSound.Play();

        //Debug.Log("hit.point = " + hit.point + ".");

        if (spellCaster == hero)
        {

              player.spellIconMask.fillAmount = 1f;


        }



    }


    //Work on this later! Gamepad controls



}



