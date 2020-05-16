using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatoControllerScript : MonoBehaviour
{

    public Animator animador;
    public bool state;
    public float ratioDecision = 5f;
    public float ultimaDecision = 0.0f;
    public float movementDuration = 10.0f;
    public float waitBeforeMoving = 1.0f;
    public bool hasArrived = false;
    public bool galleta = false;
    private GameObject target;
    private Vector3 targetPoint;
    private Quaternion targetRotation;
    public AudioClip audioClip1;
    public AudioClip audioClip2;
    public AudioSource audioSource;

    // Start is called before the first frame update     
    void Start()
    {
        state = false;
    }

    // Update is called once per frame     
    void Update()
    {
        if ((Time.time > ratioDecision + ultimaDecision))
        {
            float decision = Random.Range(0, 2);
            if (decision == 1) {
                state = true;
            } else {
                state = false;
            }
            ultimaDecision = Time.time;
        }
        if (!hasArrived && state == true && galleta == false) {
            hasArrived = true;
            float randX = Random.Range(-5.0f, 5.0f);
            float randZ = Random.Range(-5.0f, 5.0f);
            StartCoroutine(MoveToPoint(new Vector3(randX, gameObject.transform.position.y, randZ)));
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player" && galleta == false) {
            transform.LookAt(other.transform.position);
            galleta = true;
            setAnimations(false, true, false);
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.transform.tag == "Player" && galleta == true && Input.GetKeyDown("space"))
        {
            float decision = Random.Range(0, 2);
            if (decision == 1)
            {
                audioSource.clip = audioClip1;
            }
            else
            {
                audioSource.clip = audioClip2;
            }
            audioSource.Play();
            setAnimations(false, false, true);
            galleta = false;
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.transform.tag == "Player" && galleta == true) {
            galleta = false;
            var rotationVector = transform.rotation.eulerAngles;
            rotationVector.z = 0;
            rotationVector.x = 0;
            rotationVector.y = 0;
            transform.rotation = Quaternion.Euler(rotationVector);
            setAnimations(false, true, false);
        }
    }

    private void setAnimations(bool idlep, bool activep, bool animationcookiep) {
        animador.SetBool("sentado", idlep);
        animador.SetBool("levantarse", activep);
        animador.SetBool("tomarGalleta", animationcookiep);
    }

    public IEnumerator MoveToPoint(Vector3 targetPos)
    {
        float timer = 0.0f;
        Vector3 startPos = transform.position;
        transform.rotation = Quaternion.LookRotation(targetPos);

        while (timer < movementDuration)
        {
            timer += Time.deltaTime;
            if (galleta == false)
            {
                float t = timer / movementDuration;
                t = t * t * t * (t * (6f * t - 15f) + 10f);
                transform.position = Vector3.Lerp(startPos, targetPos, t);
            }

            yield return null;
        }

        yield return new WaitForSeconds(waitBeforeMoving);
        hasArrived = false;
    }


}
