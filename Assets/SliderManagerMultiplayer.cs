﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SliderManagerMultiplayer : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject[] sliders;
    public GameObject[] sliderObject;
    public Text[] sliderTexts;
    public int[] values;

    public GameObject[] upgrades;
    public int[] upgradecheckpoints; //valores que o player precisa de chegar para fazer upgrade
    public int currentupgrade; //Diz qual o upgrade em que o player esta atualmente

    private GameObject player;

    public float highestValue; //O maior valor entre os sliders
    public int currentSlider; //O slider com maior valor

    public bool isVisible; //Todos os sliders que não forem o primeiro vicam visiveis


    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {

        isVisible = false;

    }

    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {

        isVisible = true;

    }


    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {

        player = GameObject.FindGameObjectWithTag("Player");

        sliders[0].GetComponent<Slider>().value = values[0];
        sliders[1].GetComponent<Slider>().value = values[1];
        sliders[2].GetComponent<Slider>().value = values[2];

        bool existsValue = false; //certefica-se que o highestValue ainda existe.
        for (int i = 0; i < sliders.Length; i++)
        {
            //Ver se o valor de cada slider é maior que o valor atual. 
            //Se isso acontecer o valor atual muda.
            if (sliders[i].GetComponent<Slider>().value > highestValue)
            {
                highestValue = sliders[i].GetComponent<Slider>().value;
                currentSlider = i;
                existsValue = true;
            }

            if (i == currentSlider)
            {
                sliderObject[i].transform.SetSiblingIndex(sliderObject.Length);
            }

            if (sliders[i].activeSelf)
            {
                sliderTexts[i].text = "" + sliders[i].GetComponent<Slider>().value;
            }

            if (values[i] == highestValue)
            {
                existsValue = true;
            }

            if (sliderObject[i].transform.GetSiblingIndex() != sliders.Length - 1 && !isVisible)
            {

                sliderObject[i].SetActive(false);


            }
            else
            {
                sliderObject[i].SetActive(true);


            }

        }

        //Se o highestValue não existir o valor fica 0.
        //O for loop é feito outra fez para preceber qual o novo highestValue.
        if (!existsValue)
        {

            highestValue = 0;

        }

        if (existsValue)
        {
            //0 ----> 1 Hidrogenio H
            if (values[0] >= sliders[0].GetComponent<Slider>().maxValue && currentupgrade == 0)
            {
                Instantiate(upgrades[0], player.transform.position, Quaternion.identity);
                Destroy(player);
                player = GameObject.FindGameObjectWithTag("Player");
                for (int i = 0; i < values.Length; i++)
                {

                    values[i] = 0;

                }
                currentupgrade = 1;
                sliders[0].GetComponent<Slider>().maxValue = 75;
                sliders[1].GetComponent<Slider>().maxValue = 0;
                sliders[2].GetComponent<Slider>().maxValue = 0;
                UpgradeSound();
            }

            //0 ----> 2 Oxigenio O
            if (values[1] >= sliders[1].GetComponent<Slider>().maxValue && currentupgrade == 0)
            {
                Instantiate(upgrades[1], player.transform.position, Quaternion.identity);
                Destroy(player);
                player = GameObject.FindGameObjectWithTag("Player");
                for (int i = 0; i < values.Length; i++)
                {

                    values[i] = 0;

                }
                currentupgrade = 2;
                sliders[0].GetComponent<Slider>().maxValue = 125;
                sliders[1].GetComponent<Slider>().maxValue = 75;
                sliders[2].GetComponent<Slider>().maxValue = 125;
                UpgradeSound();
            }

            //0 ----> 3 Carbono C
            if (values[2] >= sliders[2].GetComponent<Slider>().maxValue && currentupgrade == 0)
            {
                Instantiate(upgrades[2], player.transform.position, Quaternion.identity);
                Destroy(player);
                player = GameObject.FindGameObjectWithTag("Player");
                for (int i = 0; i < values.Length; i++)
                {

                    values[i] = 0;

                }
                currentupgrade = 3;
                sliders[0].GetComponent<Slider>().maxValue = 0;
                sliders[1].GetComponent<Slider>().maxValue = 125;
                sliders[2].GetComponent<Slider>().maxValue = 75;
                UpgradeSound();
            }

            //2 ----> 6 Oxigenio O2
            if (values[1] >= sliders[1].GetComponent<Slider>().maxValue && currentupgrade == 2)
            {
                Instantiate(upgrades[5], player.transform.position, Quaternion.identity);
                Destroy(player);
                player = GameObject.FindGameObjectWithTag("Player");
                for (int i = 0; i < values.Length; i++)
                {

                    values[i] = 0;

                }
                currentupgrade = 6;
                sliders[1].GetComponent<Slider>().maxValue = 100;
                sliders[0].GetComponent<Slider>().maxValue = 0;
                sliders[2].GetComponent<Slider>().maxValue = 0;
                UpgradeSound();
            }

            //1 ----> 4 Hidrogenio H2
            if (values[0] >= sliders[0].GetComponent<Slider>().maxValue && currentupgrade == 1)
            {
                Instantiate(upgrades[3], player.transform.position, Quaternion.identity);
                Destroy(player);
                player = GameObject.FindGameObjectWithTag("Player");
                for (int i = 0; i < values.Length; i++)
                {

                    values[i] = 0;

                }
                currentupgrade = 4;
                sliders[0].GetComponent<Slider>().maxValue = 0;
                sliders[2].GetComponent<Slider>().maxValue = 0;
                sliders[1].GetComponent<Slider>().maxValue = 100;
                UpgradeSound();
            }

            //2 ----> 5 Hidrogenio H2O
            //4 ----> 5 Hidrogenio H2O
            if (values[1] >= sliders[1].GetComponent<Slider>().maxValue && currentupgrade == 4 ||
                values[0] >= sliders[0].GetComponent<Slider>().maxValue && currentupgrade == 2)
            {
                Instantiate(upgrades[4], player.transform.position, Quaternion.identity);
                Destroy(player);
                player = GameObject.FindGameObjectWithTag("Player");
                for (int i = 0; i < values.Length; i++)
                {

                    values[i] = 0;

                }
                currentupgrade = 5;
                sliders[0].GetComponent<Slider>().maxValue = 0;
                sliders[1].GetComponent<Slider>().maxValue = 0;
                sliders[2].GetComponent<Slider>().maxValue = 0;
                UpgradeSound();
            }

            //2 ----> 8 DioxidoCarbono CO2
            //3 ----> 8 DioxidoCarbono CO2
            if (values[2] >= sliders[2].GetComponent<Slider>().maxValue && currentupgrade == 2 ||
                values[1] >= sliders[1].GetComponent<Slider>().maxValue && currentupgrade == 3)
            {
                Instantiate(upgrades[7], player.transform.position, Quaternion.identity);
                Destroy(player);
                player = GameObject.FindGameObjectWithTag("Player");
                for (int i = 0; i < values.Length; i++)
                {

                    values[i] = 0;

                }
                currentupgrade = 8;
                sliders[0].GetComponent<Slider>().maxValue = 0;
                sliders[1].GetComponent<Slider>().maxValue = 0;
                sliders[2].GetComponent<Slider>().maxValue = 0;
                UpgradeSound();
            }

            //6 ----> 7 Ozono O3
            if (values[1] >= sliders[1].GetComponent<Slider>().maxValue && currentupgrade == 6)
            {
                Instantiate(upgrades[6], player.transform.position, Quaternion.identity);
                Destroy(player);
                player = GameObject.FindGameObjectWithTag("Player");
                for (int i = 0; i < values.Length; i++)
                {

                    values[i] = 0;

                }
                currentupgrade = 7;
                sliders[0].GetComponent<Slider>().maxValue = 0;
                sliders[1].GetComponent<Slider>().maxValue = 0;
                sliders[2].GetComponent<Slider>().maxValue = 0;
                UpgradeSound();
            }

            //3 ----> 9 Carbono C2
            if (values[2] >= sliders[2].GetComponent<Slider>().maxValue && currentupgrade == 3)
            {
                Instantiate(upgrades[8], player.transform.position, Quaternion.identity);
                Destroy(player);
                player = GameObject.FindGameObjectWithTag("Player");
                for (int i = 0; i < values.Length; i++)
                {

                    values[i] = 0;

                }
                currentupgrade = 9;
                sliders[0].GetComponent<Slider>().maxValue = 0;
                sliders[1].GetComponent<Slider>().maxValue = 0;
                sliders[2].GetComponent<Slider>().maxValue = 0;
                UpgradeSound();
            }


        }


    }

    public void UpgradeSound()
    {
        if (GetComponent<AudioSource>().enabled)
        {

            GetComponent<AudioSource>().Play();

        }
        else
        {

            GetComponent<AudioSource>().enabled = true;

        }

    }
}
