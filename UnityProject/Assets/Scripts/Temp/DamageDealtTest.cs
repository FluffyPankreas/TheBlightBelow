using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealtTest : MonoBehaviour
{
    public float activeTime = 0.5f;
    public float inActiveTime = 3f;

    public SpriteRenderer bloodSplat;
    public ParticleSystem bloodPArticles;

    private float timeTillSwitch;
    
    public void Start()
    {
        bloodSplat.gameObject.SetActive(false);
        bloodPArticles.gameObject.SetActive(false);

        timeTillSwitch = inActiveTime;

    }

    public void Update()
    {
        timeTillSwitch -= Time.deltaTime;
        
        if (bloodSplat.gameObject.activeSelf)
        {
            if (timeTillSwitch <= 0)
            {
                timeTillSwitch = inActiveTime;
                bloodSplat.gameObject.SetActive(false);
                bloodPArticles.gameObject.SetActive(false);
            }
        }
        else
        {
            if (timeTillSwitch <= 0)
            {
                timeTillSwitch = activeTime;
                bloodSplat.gameObject.SetActive(true);
                bloodPArticles.gameObject.SetActive(true);
                
            }
        }
        
        
    }
}
