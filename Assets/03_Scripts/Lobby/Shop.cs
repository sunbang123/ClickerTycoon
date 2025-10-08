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
        if (this.gameObject.activeSelf == true)
        {
            shopImage.SetActive(false);

            this.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
