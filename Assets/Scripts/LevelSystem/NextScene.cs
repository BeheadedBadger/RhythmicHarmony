using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextScene : MonoBehaviour
{
    [SerializeField] GameObject button;
    [SerializeField] AudioClip selectSFX;
    [SerializeField] AudioClip hoverSFX;

    AudioSource audioSource;
    private void Awake() {
        audioSource = GetComponent<AudioSource>();
    }

    //Load the next scene in the build profile scene list when clicked. 
    public void OnClick()
    {
        // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void LoadScene(int sceneIndex) {
        if (selectSFX == null) SceneManager.LoadScene(sceneIndex);
        audioSource.Stop();
        audioSource.PlayOneShot(selectSFX);
        StartCoroutine(DelayLoad());

        IEnumerator DelayLoad() {
            yield return new WaitForSeconds(0.5f);
            SceneManager.LoadScene(sceneIndex);
        }
    }

    public void OnHover() 
    { 
        audioSource.Stop();
        audioSource.PlayOneShot(hoverSFX);
        button.transform.localScale = new Vector3(this.transform.localScale.x + 0.05f, this.transform.localScale.y + 0.05f );
    }

    public void OnExit()
    {
       
        button.transform.localScale = new Vector3(this.transform.localScale.x - 0.05f, this.transform.localScale.y - 0.05f);
    }
}
