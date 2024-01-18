using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolgeOyunu : MonoBehaviour
{
    Camera camera;
    GameObject[] golgeler;
    Vector2 baslangicPoz;

    private void Start()
    {
        camera = GameObject.Find("camera").GetComponent<Camera>();
        golgeler = GameObject.FindGameObjectsWithTag("golge");
        baslangicPoz = transform.position;
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 pozisyon = camera.ScreenToWorldPoint(Input.mousePosition);
            pozisyon.z = 0;
            transform.position = pozisyon;
            Debug.Log("jkdsmkflsd");
        }
    }

    private void OnMouseUp()
    {
        foreach (GameObject golge in golgeler)
        {
            if (gameObject.name == golge.name)
            {
                float mesafe = Vector3.Distance(golge.transform.position, transform.position);
                if (mesafe <= 1)
                {
                    transform.position = golge.transform.position;
                    Destroy(this);
                }
                else
                {
                    transform.position = baslangicPoz;
                }
            }
        }
    }
}

