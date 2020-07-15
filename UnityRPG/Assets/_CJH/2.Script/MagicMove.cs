using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicMove : MonoBehaviour
{
    public float lifeTime;
    public float flySpeed;
    public GameObject player;
    
    IEnumerator Disable(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        DestroyImmediate(gameObject);
    }

    private void OnEnable()
    {
        StartCoroutine(Disable(lifeTime));
    }

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        transform.GetComponent<Rigidbody>().AddForce(gameObject.transform.forward, ForceMode.Impulse);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.CompareTag("Enemy"))
        {
            Debug.Log(other);
            if (other.transform.name.Contains("Foot"))
            {
                other.transform.GetComponent<FootManFSM>().HitDamage(player.GetComponent<PlayerStatus>().magicAtk * 3);
            }
            else
            {
                other.GetComponent<EnemyFSM>().HitDamage(player.GetComponent<PlayerStatus>().magicAtk*3);
            }
            StopCoroutine(Disable(lifeTime));
            DestroyImmediate(gameObject);
        }
    }
    
}
