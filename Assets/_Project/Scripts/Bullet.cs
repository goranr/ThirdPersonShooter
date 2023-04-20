using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody rb;
    public Transform explosion;
    public Transform badExplosion;
    public float speed = 10;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = Vector3.forward * speed;
    }

	private void OnTriggerEnter(Collider other)
	{
        transform.GetChild(0).gameObject.SetActive(false);
        if(other.GetComponent<ShootableObject>() != null)
		{
            Instantiate(explosion, transform.position, Quaternion.identity);
		}
        else
		{
            Instantiate(badExplosion, transform.position, Quaternion.identity);
        }

        Destroy(gameObject);
	}

    private IEnumerator IDestroyObject()
	{
        yield return new WaitForSeconds(1.5f);
        Destroy(gameObject);
    }
}
