using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    public float speed = 4.5f;

    // Update is called once per frame
    private void Update()
    {
        transform.position += transform.right * Time.deltaTime * speed;
    }

    private void OnCollisionEnter2D()
    {
        Destroy(gameObject);
    }
}
