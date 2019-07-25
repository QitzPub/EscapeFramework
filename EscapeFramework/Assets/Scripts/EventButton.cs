using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button)), RequireComponent(typeof(Image))]
public class EventButton : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
        var image = this.gameObject.GetComponent<Image>();
        image.color = Color.clear;
    }
}
