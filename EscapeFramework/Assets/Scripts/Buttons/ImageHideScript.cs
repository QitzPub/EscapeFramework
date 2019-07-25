using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ImageHideScript : MonoBehaviour
{
    private void Start()
    {
        var image = this.gameObject.GetComponent<Image>();
        image.color = Color.clear;
    }
}
