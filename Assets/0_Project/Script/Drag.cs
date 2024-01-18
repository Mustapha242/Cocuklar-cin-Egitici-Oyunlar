using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drag : MonoBehaviour
{
    Vector2 vec;
    bool match;
    Transform matchTransform;
    Vector3 firstPos;
    public Game2Controller controller;


    private void Start()
    {
        firstPos = this.transform.position;
    }

    private void OnMouseDown()
    {
        vec = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - (Vector2)this.transform.position;
    }

    private void OnMouseDrag()
    {
        this.transform.position = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - vec;
    }

    private void OnMouseUp()
    {
        if (match) {
            this.transform.GetComponent<Collider2D>().enabled = false;
            controller.currentMatchAmount++;
            transform.DOMove(matchTransform.position, 1f);
            AudioManager.instance.Play("Correct");
            if (controller.currentMatchAmount >= controller.totalShapeAmount) UIManager.instance.OpenWinPanel(false);
        } 
        else {
            transform.DOMove(firstPos, 1f);
            AudioManager.instance.Play("Wrong");
        } 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (this.tag == collision.tag) {
            Debug.Log("assaf");
            match = true;
            matchTransform = collision.transform;
        } 
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (this.tag == collision.tag) match = false;
    }
}
