using UnityEngine;
using UnityEngine.SceneManagement;

public class Character : MonoBehaviour
{
    private float health;
    GameObject chart;
    bool isLightning = false;
    bool isOld = false;
    bool isWizard = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        health = 200;
        chart = GetComponent<GameObject>();
        if (chart.GetComponent<LightningWizard>() != null) { 
            isLightning = true;
        }
        else if(chart.GetComponent<OldWizardScript>() != null){
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
            if (isWizard)
            {
                StartCoroutine(chart.GetComponent<LightningWizard>().deathAnimation());
            }
            else if (isOld)
            {
                StartCoroutine(chart.GetComponent<OldWizardScript>().deathAnimation());
            }
            else
            {
                StartCoroutine(chart.GetComponent<WizardScript>().deathAnimation());
            }
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision != null)
        {
            if (collision.otherCollider.CompareTag("Attacking"))
            {
                health -= 40;
            }

        }
    }

}
