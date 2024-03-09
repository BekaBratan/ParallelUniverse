using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    public bool gameOn;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Spawn());
        gameOn = true;
    }



    IEnumerator Spawn()
    {
        while (gameOn)
        {
            yield return new WaitForSeconds(Random.Range(4.5f, 7.5f));

            GameObject newEnemy = Instantiate(gameObject, transform.position, Quaternion.identity);

            newEnemy.transform.SetParent(gameObject.transform.parent);

            newEnemy.SetActive(true);

            newEnemy.transform.position = new Vector2(Random.Range(-12f, 12f), Random.Range(-2.3f, -6.7f));

            PlayerPrefs.SetInt("Clone", PlayerPrefs.GetInt("Clone", 0) + 1);
        }
    }

}
