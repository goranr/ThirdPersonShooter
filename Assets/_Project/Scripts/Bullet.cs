using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    public Transform explosion;
    private Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.forward * speed;
    }

	private void OnTriggerEnter(Collider other)
	{
        Instantiate(explosion, transform.position, Quaternion.identity);
        if(other.GetComponent<ShootableObject>() != null)
		{
            /*other.GetComponent<ShootableObject>().rb.AddExplosionForce(
                500, transform.position, 5);*/
            other.GetComponent<ShootableObject>().rb.AddForce(transform.forward * 50);
        }

        Destroy(gameObject);
	}
}
