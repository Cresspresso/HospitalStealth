using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowAfterSeconds : MonoBehaviour
{
    public float duration = 1.0f;
    public GameObject visuals;

    // Start is called before the first frame update
    void Start()
    {
        visuals.SetActive(false);
        StartCoroutine(Show());
    }

    private IEnumerator Show()
    {
        yield return new WaitForSeconds(duration);
        visuals.SetActive(true);
    }
}
