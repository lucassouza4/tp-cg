using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    private float currentTime = 0f;
    private float startTime = 60f;
    [SerializeField] TextMeshProUGUI text;

    private GameObject panel;

    // Start is called before the first frame update
    void Start()
    {
        panel = GameObject.FindWithTag("Panel");
        currentTime = startTime;
    }

    // Update is called once per frame
    void Update()
    {
        if(panel.GetComponent<RectTransform>().localScale == new Vector3(1, 1, 1) && text.text != "0")
        {
            currentTime -= 1 * Time.deltaTime;
            text.text = currentTime.ToString("0");
        }
    }
}
