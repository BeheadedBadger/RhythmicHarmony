using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public ProjectileController ProjectilePrefab;
    public Transform LaunchOffset;

    //private RigidBody2D _rigidbody;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //_rigidbody = GetComponent<RigidBody2D>();
    }

    // Update is called once per frame
    void Update()
    {
       if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow)) {
            Instantiate(ProjectilePrefab, LaunchOffset.position, transform.rotation);
        }
    }
}
