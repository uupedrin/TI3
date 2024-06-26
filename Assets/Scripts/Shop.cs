using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public List<Material> shopShaders;
    void Start()
    {
        
    }

    void Update()
    {
        if(Input.GetKeyDown("1")) Buy(1);
        else if(Input.GetKeyDown("2")) Buy(2);
    }

    public void Buy(int shaderNumber)
    {
        Debug.Log("oiee");
        ShaderManager.instance.shaders.Add(shopShaders[shaderNumber]);
    }
}
