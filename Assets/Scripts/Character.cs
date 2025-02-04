using UnityEngine;
using UnityEngine.SceneManagement;

public class Character : MonoBehaviour
{
    public float health;
    bool isLightning = false;
    bool isOld = false;
    bool isWizard = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        health = 40;
        if (GetComponent<LightningWizard>() != null) { 
            isLightning = true;
        }
        else if(GetComponent<OldWizardScript>() != null){
            isOld = true;
        }
        else
        {
            isWizard = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            if (isLightning)
            {
                StartCoroutine(GetComponent<LightningWizard>().deathAnimation());
            }
            else if (isOld)
            {
                StartCoroutine(GetComponent<OldWizardScript>().deathAnimation());
            }
            else if(isWizard)
            {
               StartCoroutine(GetComponent<WizardScript>().deathAnimation());
            }
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

}
