using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatoInteractiveController : MonoBehaviour
{
    public Animator animador;
    public GameObject gato;
    public bool idle;
    public bool active;
    public bool animationCookie;

    private void Start()
    {
        idle = true;
        active = false;
        animationCookie = false;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player" && idle == true)
        {
            idle = false;
            active = true;
            animationCookie = false;
            setAnimations(idle, active, animationCookie);
            gato.GetComponent<GatoControllerScript>().enabled = true;

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "Player" && idle == false)
        {
            idle = true;
            active = false;
            animationCookie = false;
            setAnimations(idle, active, animationCookie);
            gato.GetComponent<GatoControllerScript>().enabled = false;
        }
    }
    private void setAnimations(bool idlep, bool activep, bool animationcookiep)
    {
        animador.SetBool("sentado", idlep);
        animador.SetBool("levantarse", activep);
        animador.SetBool("tomarGalleta", animationcookiep);
    }

}
