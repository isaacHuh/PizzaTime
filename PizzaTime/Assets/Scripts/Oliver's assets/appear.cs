using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class appear : MonoBehaviour
{
    public float delay = 0f;

    private float timer = 0f;
    private RectTransform text;
    private Vector3 og_scale;
    // Start is called before the first frame update
    void Start()
    {
        text = gameObject.GetComponent<RectTransform>();
        og_scale = text.localScale;
        text.localScale = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer > delay)
        {
            text.localScale = og_scale;
        }
    }
}
