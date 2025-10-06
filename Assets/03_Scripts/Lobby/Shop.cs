using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public GameObject shopImage;


    public void shopUI()
    {
        if (shopImage.activeSelf == false)
        {
            shopImage.SetActive(true);
        }
        else
        {
            shopImage.SetActive(false);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
