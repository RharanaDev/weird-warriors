using UnityEngine;
using UnityEngine.SceneManagement;

public class VersusScript : MonoBehaviour
{
    public GameObject filter;
    // Start is called before the first frame update
    void FilterAndFade()
    {
        filter.SetActive(true);
    } 
    void NextScene() //Only for filter object
    {
        SceneManager.LoadScene(2);
    }
}
