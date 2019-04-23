using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChangeColor : MonoBehaviour
{

    private TextMeshProUGUI tmpObj;

    // Start is called before the first frame update
    void Start()
    {
        tmpObj = GetComponent<TextMeshProUGUI>();
        tmpObj.color = new Color32(255, 255, 255, 255);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
