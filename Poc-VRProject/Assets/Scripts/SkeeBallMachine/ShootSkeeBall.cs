using UnityEngine;

public class ShootSkeeBall : MonoBehaviour
{
    [SerializeField] GameObject ballObj;
    [SerializeField] float speed;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject newBall = Instantiate(ballObj);
            newBall.transform.position = transform.position;
            newBall.GetComponent<Rigidbody>().linearVelocity = Vector3.forward * speed;
        }
    }
}
