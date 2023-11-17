using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interactions : MonoBehaviour
{
    PlayerMovement movement;

    public GameObject winScreen;

    public bool winScreenActivated = false;

    // Start is called before the first frame update
    void Start()
    {
        movement = GetComponent<PlayerMovement>();

        winScreen.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Vending Machine")
        {
            winScreenActivated = true;

            movement.enabled = false;

            winScreen.SetActive(true);

            StartCoroutine(Quit());
        }
    }

    IEnumerator Quit()
    {
        yield return new WaitForSeconds(5);

        Application.Quit();
    }
}
