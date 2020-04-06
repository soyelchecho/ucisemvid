using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalSpawn : MonoBehaviour
{

    public Vector3 initialPoint;
    public GameObject gato;

    // Start is called before the first frame update
    void Start()
    {
        initialPoint = new Vector3(0, 10f, 0);
        StartCoroutine(Spawn(10, initialPoint));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator Spawn(float cantidad, Vector3 initialPosition) {
        float randX = Random.Range(-10.0f, 10.0f);
        float randZ = Random.Range(-10.0f, 10.0f);
        Vector3 startPos = initialPosition;
        startPos += new Vector3(randX, randZ);

        while (cantidad != 0) {
            GameObject animal = Instantiate(gato, startPos, Quaternion.identity);
            animal.transform.rotation = Quaternion.LookRotation(startPos);
            randX = Random.Range(-10.0f, 10.0f);
            randZ = Random.Range(-10.0f, 10.0f);
            startPos += new Vector3(randX, randZ);
            cantidad -= 1;
            yield return null;
        }

        yield return new WaitForSeconds(1.0f);
    }
}
