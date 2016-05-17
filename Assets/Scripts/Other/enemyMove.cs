using UnityEngine;
using System.Collections;

public class enemyMove : MonoBehaviour
{
    public Transform enemy;
    public Ray ray;
    public RaycastHit[] objects;
    public Rigidbody rb;
    public GameObject gobje;
	
	void Start()
    {
        //ray = ;
	}
	
	void Update()
    {
        //findTarget();
	}

    /*public void findTarget()
    {
        objects = Physics.SphereCastAll(ray, 100.0f);
        {
            foreach (RaycastHit gobj in objects)
            {
                rb = gobj.transform.GetComponent<Rigidbody>();
                if (rb.tag == "Player Unit")
                {
                    enemy.transform.LookAt(rb.position);
                    enemy.transform.position = Vector3.Lerp(enemy.transform.position, rb.position, Time.deltaTime / 2);
                }
            }
        }
    }*/

    void OnCollisionEnter(Collision col)
    {
        rb = col.transform.GetComponent<Rigidbody>();
        {
            if (rb.tag == "Player Unit")
            {
                Debug.Log("Winner, winner, chicken dinner!");
            }
        }
    }
}