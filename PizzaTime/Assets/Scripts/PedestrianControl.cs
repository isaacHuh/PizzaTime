using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PedestrianControl : MonoBehaviour
{
    public SkinnedMeshRenderer skinRenderer;
    public List<Material> skins;
    // Start is called before the first frame update
    void Start()
    {
        skinRenderer.material = skins[Random.Range(0, skins.Count)];
        transform.rotation = Quaternion.Euler(new Vector3(0,Random.Range(0,360f),0));

        Animation anim = gameObject.GetComponent<Animation>();
        int setAnimation = Random.Range(0, 6);
        switch (setAnimation) {
            case 0:
                anim.Play("idle");
                break;
            case 1:
                anim.Play("applause");
                break;
            case 2:
                anim.Play("applause2");
                break;
            case 3:
                anim.Play("celebration");
                break;
            case 4:
                anim.Play("celebration2");
                break;
            case 5:
                anim.Play("celebration3");
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && gameObject.GetComponent<Animation>().enabled == true)
        {
            MoneyManager moneyManager = GameObject.Find("MoneyManager").GetComponent<MoneyManager>();
            moneyManager.budget -= 100;
            moneyManager.fines += 100;
            gameObject.GetComponent<Animation>().enabled = false;
        }
    }
}
