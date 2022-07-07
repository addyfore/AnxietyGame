using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEditor.Experimental.SceneManagement;
using UnityEngine.Rendering.Universal;

public class BrightSpellTemplate : SpellTemplate
{ 

    public GameObject activeLightPool;
    public bool forLoopFoundDisabledPoolToImplement = false;
    public List<GameObject> instantiatedPoolsTemp = new List<GameObject>(4);
    public GameObject seasideRuinsEntrance;
    public GameObject monsterStone;
    public GameObject summonedSlime;
    private GameObject hitObject;
    public LightPillars lightPillars;


    public override void Awake()
    {
        seasideRuinsEntrance.GetComponent<SpriteRenderer>().color = new Color(0f, 0f, 0f, 1f);
        seasideRuinsEntrance.gameObject.GetComponentInChildren<CapsuleCollider2D>().enabled = false;
        seasideRuinsEntrance.gameObject.GetComponent<CapsuleCollider2D>().enabled = true;
        base.Awake();
    }


    public override void castSpell()
    {
        int layerMask = LayerMask.GetMask("Scenery", "Enemy");
        //rawDirection = new Vector3(mousePos.x, mousePos.y, 0f) - tempHeroTransformValues;
        heroTransform = hero.transform.position;
        direction = (mousePos - heroTransform).normalized;
        Debug.Log("hero transform position = " + hero.transform.position);
        Debug.Log("Camera.main.ScreenToWorldPoint(hero.transform.position) = " + Camera.main.ScreenToWorldPoint(hero.transform.position));
        Debug.Log("heroTransform = " + heroTransform + ", direction = " + direction + ", mousePos = " + mousePos + ".");        


        RaycastHit2D hit = Physics2D.Raycast(heroTransform, direction, rayCastDistance, layerMask);

        Debug.Log("hit.point = " + hit.point + "."); 

        if (hit.collider)
        {
            hitObject = hit.collider.gameObject;

            gameObject.transform.position = hit.transform.position;
            gameObject.GetComponent<Animator>().SetBool("isCasting", true);
            Debug.Log("Spell now at " + gameObject.transform.position + " which should be the same as " + hit.transform.position + ".");

            if (hitObject == monsterStone)
            {
                monsterStone.GetComponent<Light2D>().enabled = true;
                summonedSlime.SetActive(true);
            }

            else if (hitObject == seasideRuinsEntrance)
            {
                seasideRuinsEntrance.GetComponent<SpriteRenderer>().color = new Color(198f, 198f, 198f, 1f);
                seasideRuinsEntrance.gameObject.GetComponentInChildren<CapsuleCollider2D>().enabled = true;
                seasideRuinsEntrance.gameObject.GetComponent<CapsuleCollider2D>().enabled = false;

            }

            else if (hitObject.GetComponent<LightPillars>())

            {
                hitObject.GetComponent<Light2D>().enabled = true;
            }

            else if (hitObject.CompareTag("Enemy"))
            {

                hitObject.GetComponent<NPCHealth>().damageNPCHealth(damageAmount);

            }

            else if (hitObject.CompareTag("Scenery"))
            {


            }
            else
            {
                Debug.Log("this is the statement appearing when a collider not on the layermask gets hit, it should never show up.");
                Debug.Log("hit.name because something weird happened : " + hit.collider.name + ".");
            }

            currentCastDownTime = castDownTime;
            spellHolderScript.currentCastDownTime = currentCastDownTime;
            spellHolderScript.globalCastDownTime = globalCastDownTime;
            spellIconMask.fillAmount = 1f;
            Debug.Log("hit Something : " + hit.collider.name + " spell should be " + spellHolderScript.currentSpell + ". Collision occurred at " + hit.transform.position);
            Debug.Log("hit tag  : " + hit.collider.tag);
        }


        else

        {
            Debug.Log("no collider hit, moving on to making a light pool.");

            for (int i = 0; i < spellHolder.GetComponent<SpellHolder>().numberOfPools -1; i++)
            {
                Debug.Log("for statement for the bright spell cycling");

                if (spellHolderScript.instantiatedPools[i].activeSelf == false)
                {
                    Debug.Log("found inactive pool.");
                    activeLightPool = spellHolderScript.instantiatedPools[i];
                    forLoopFoundDisabledPoolToImplement = true;
                    break;
                }

            }

            var instantiatedPoolsTemp = new List<GameObject>(spellHolderScript.instantiatedPools.Capacity);

            if (forLoopFoundDisabledPoolToImplement == false)
            {

                Debug.Log("No pools are disabled. Recycling pool.");
                activeLightPool = spellHolderScript.instantiatedPools[0];

                spellHolderScript.instantiatedPools.Remove(activeLightPool);
                instantiatedPoolsTemp.AddRange(spellHolderScript.instantiatedPools);
                instantiatedPoolsTemp.Insert(3, activeLightPool);

            }
            else
            {
                Debug.Log("using inactive pool.");
                forLoopFoundDisabledPoolToImplement = false;
                spellHolderScript.instantiatedPools.Remove(activeLightPool);
                for (int i = 0; i < spellHolder.GetComponent<SpellHolder>().numberOfPools - 1; i++)
                {

                    if (spellHolderScript.instantiatedPools[i] != activeLightPool)
                    {

                        instantiatedPoolsTemp.Insert(i, spellHolderScript.instantiatedPools[i]);
                    }
                }
                instantiatedPoolsTemp.Insert(3, activeLightPool);

            }


            spellHolderScript.instantiatedPools.Clear();
            spellHolderScript.instantiatedPools.AddRange(instantiatedPoolsTemp);
            instantiatedPoolsTemp.Clear();

            mouseDistance = Vector2.Distance(heroTransform, mousePos);
            Debug.Log("mouseDistance = " + mouseDistance + ", calculated using heroTransform (" + heroTransform + ") and mousePos (" + mousePos + ")");

            if (mouseDistance < rayCastDistance)
            {
                point = new Vector3(mousePos.x, mousePos.y, 0f);
                Debug.Log("point now converted to mousePos with 0f for z axis and located at " + point + ".");

            }
            else
            {
                point = new Vector3(heroTransform.x + (direction.x * rayCastDistance), heroTransform.y + (direction.y * rayCastDistance), 0f);
                Debug.Log("point now converted to heroTransform + direction * rayCastDistance and located at " + point + ".");
            }

            //I need to figure out a way to drop the lightpools closer to the hero than the raycastdistance if the mouse clicks somewhere closer than it.
            //Consulting Donut for math tutoring!           
            activeLightPool.transform.position = point;
            activeLightPool.SetActive(true);

            Debug.Log("Pool of light should be at point " + point + ". It reads at " + activeLightPool.transform.position + ".");

            spellHolderScript.globalCastDownTime = globalCastDownTime;
            spellHolderScript.currentCastDownTime = castDownTime;
            currentCastDownTime = castDownTime;


            spellIconMask.fillAmount = 1f;


        }
    }
}