using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public GameObject normal_shop_Image;
    public GameObject battle_shop_Image;

    public void shopOnOffUI()
    {
        if (normal_shop_Image.activeSelf == false && battle_shop_Image.activeSelf == false)
        {
            normal_shop_Image.SetActive(true);
        }

        else
        {
            normal_shop_Image.SetActive(false);
            battle_shop_Image.SetActive(false);
        }
    }

    public void shopChange()
    {
        if (normal_shop_Image.activeSelf)
        {
            normal_shop_Image.SetActive(false);

            battle_shop_Image.SetActive(true);
        }

        else
        {
            battle_shop_Image.SetActive(false);

            normal_shop_Image.SetActive(true);
        }
    }

    void Start()
    {
        if (this.gameObject.activeSelf == true)
        {
            normal_shop_Image.SetActive(false);

            battle_shop_Image.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
