using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuImageRotation : MonoBehaviour {

    public Sprite[] image;
    public GameObject[] otherImages;
    Image self;

    void Start()
    {
        self = gameObject.GetComponent<Image>();
        gameObject.GetComponent<Image>().sprite = image[Random.Range(0, image.Length)];        
    }

    void Update ()
    {
        if (otherImages[0].GetComponent<Image>().sprite == self.sprite || otherImages[1].GetComponent<Image>().sprite == self.sprite || otherImages[2].GetComponent<Image>().sprite == self.sprite)
        {
            gameObject.GetComponent<Image>().sprite = image[Random.Range(0, image.Length)];
        }
        if (self.sprite == image[0] || self.sprite == image[5] || self.sprite == image[8])
        {
            gameObject.GetComponent<Transform>().localScale = new Vector3(1f, 1f, 0);
        }
        else
        {
            gameObject.GetComponent<Transform>().localScale = new Vector3(1, 1, 0);
        }
        float AngleRad = Mathf.Atan2(Input.mousePosition.y - transform.position.y, Input.mousePosition.x - transform.position.x);
        float AngleDeg = (180 / Mathf.PI) * AngleRad;
        this.transform.rotation = Quaternion.Euler(0, 0, AngleDeg);
    }
}
