using System.Collections;
using UnityEngine;

public class TeleportEnemy : MonoBehaviour
{
    public bool gameOn;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Teleport());
        gameOn = true;
    }


    IEnumerator Teleport()
    {
        while (gameOn)
        {
            yield return new WaitForSeconds(Random.Range(2.5f, 5f));

            gameObject.transform.position = new Vector2(Random.Range(-12f, 12f), Random.Range(-2.3f, -6.7f));
        }
    }

}
