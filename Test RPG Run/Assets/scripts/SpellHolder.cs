using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellHolder : MonoBehaviour
{
    public GameObject spellObject;
    public float globalCastDownTime;
    public float currentCastDownTime;
    public GameObject spellTarget;
    public GameObject spellCaster;
    public float castDownTime;
    public SpellTemplate spellTemplate;
    public Player player;
    public GameObject hero;


    // Start is called before the first frame update
    void Start()
    {
        currentCastDownTime = 0f;
        globalCastDownTime = 0f;

    }

    // Update is called once per frame
    void Update()
    {
        if (currentCastDownTime > 0f)
        {
            currentCastDownTime -= Time.deltaTime;


        }
        else if (currentCastDownTime < 0f)
        {
            currentCastDownTime = 0f;

        }

        if (globalCastDownTime > 0f)
        {
            globalCastDownTime -= Time.deltaTime;
        }
        else if (globalCastDownTime < 0f)
        {
            globalCastDownTime = 0f;
        }

        if (spellCaster == spellTemplate.hero)
        {

             player.spellIconMask.fillAmount = Mathf.Clamp01(currentCastDownTime / player.currentSpellTemplate.castDownTime);

        }
    }
}
