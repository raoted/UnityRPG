using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicMove : MonoBehaviour
{
    public float lifeTime;
    public float flySpeed;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Disable(lifeTime));
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Rigidbody>().AddForce(transform.forward * flySpeed);
    }
    IEnumerator Disable(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        gameObject.SetActive(false);
    }
}
