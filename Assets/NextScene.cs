using UnityEngine;
using UnityEngine.SceneManagement;

public class NextScene : MonoBehaviour
{
    [SerializeField] GameObject button;

    //Load the next scene in the build profile scene list when clicked. 
    public void OnClick()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void OnHover() 
    { 
        button.transform.localScale = new Vector3(this.transform.localScale.x + 0.05f, this.transform.localScale.y + 0.05f );
    }

    public void OnExit()
    {
        button.transform.localScale = new Vector3(this.transform.localScale.x - 0.05f, this.transform.localScale.y - 0.05f);
    }
}
