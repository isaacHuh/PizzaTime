using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skidEffects : MonoBehaviour
{
    public Transform skidPrefab;
    public static Transform skidParent;

    private bool skidding = false;
    private Transform skidTrail;
    private WheelCollider wheelCollider;

    // Start is called before the first frame update
    void Start()
    {
        wheelCollider = GetComponent<WheelCollider>();

        if (skidParent == null)
        {
            skidParent = new GameObject("skidtrail(detached)").transform;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
    }

    public void emission()
    {
        StartCoroutine(StartSkid());
    }

    public IEnumerator StartSkid()
    {
        skidding = true;
        skidTrail = Instantiate(skidPrefab);

        while (skidTrail == null)
        {
            yield return null;
        }

        skidTrail.parent = transform;
        skidTrail.localPosition = -Vector3.up * wheelCollider.radius;
    }

    public void StopSkid()
    {
        if (!skidding)
        {
            return;
        }
        skidding = false;
        skidTrail.parent = skidParent;
        Destroy(skidTrail.gameObject);
        Debug.Log("Destroyed?");
    }
}
