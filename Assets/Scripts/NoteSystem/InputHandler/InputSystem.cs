using UnityEngine;

public class InputSystem : MonoBehaviour
{
    //public ProjectileController ProjectilePrefab;
    //public Transform LaunchOffset;

    // Update is called once per frame
    //void Update()
    //{
    //  if (Input.GetKeyDown("Up") || Input.GetKeyDown("Down") || Input.GetKeyDown("Left") || Input.GetKeyDown("Right")) {
    //        Instantiate(ProjectilePrefab, LaunchOffset.position, transform.rotation);
    //    }   
    //}

    public void Up() {
        Debug.Log("Up");
    }
}
