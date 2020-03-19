using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatoControllerScript : MonoBehaviour
{

    public Animator animador;
    public bool state;
    public float ratioDecision = 3f;
    private float ultimaDecision = 0.0f;
    private float movementDuration = 2.0f;
    private float waitBeforeMoving = 2.0f;
    private bool hasArrived = false;

    // Start is called before the first frame update     
    void Start()
    {
        state = false;
        animador = GetComponent<Animator>();
    }

    // Update is called once per frame     
    void Update()
    {
        if ((Time.time > ratioDecision + ultimaDecision))
        {
            float decision = Random.Range(0, 1);
            if (decision == 1)
            {
                animador.SetBool("walk", true);
                state = true;
            }
            else
            {
                animador.SetBool("walk", false);
                state = false;
            }
            ultimaDecision = Time.time;
        }
        if (!hasArrived && state == true)
        {
            hasArrived = true;
            float randX = Random.Range(-5.0f, 5.0f);
            float randZ = Random.Range(-5.0f, 5.0f);
            StartCoroutine(MoveToPoint(new Vector3(randX, -1.504f, randZ)));
        }
    }


    private IEnumerator MoveToPoint(Vector3 targetPos)
    {
        float timer = 0.0f;
        Vector3 startPos = transform.position;

        while (timer < movementDuration)
        {
            timer += Time.deltaTime;
            float t = timer / movementDuration;
            t = t * t * t * (t * (6f * t - 15f) + 10f);
            transform.position = Vector3.Lerp(startPos, targetPos, t);

            yield return null;
        }

        yield return new WaitForSeconds(waitBeforeMoving);
        hasArrived = false;
    }


}
