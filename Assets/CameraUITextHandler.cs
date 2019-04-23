using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraUITextHandler : MonoBehaviour
{
    public GameObject[] textElements;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < textElements.Length; i++)
        {
            if (textElements[i] != null)
            {
                textElements[i].SetActive(false);
            }
        }
    }
}
