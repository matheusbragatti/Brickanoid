using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickFade : MonoBehaviour
{
    Material material;
    float fade = 1f;


    // Start is called before the first frame update
    void Start()
    {
        material = GetComponent<SpriteRenderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        fade -= Time.deltaTime * 2;

        if(fade < 0f)
        {
            fade = 0f;
            Destroy(this.gameObject);
        }

        material.SetFloat("_Fade", fade);
        
    }
}
