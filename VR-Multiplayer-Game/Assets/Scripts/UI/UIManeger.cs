using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManeger : MonoBehaviour {

    public void Create() {
        SceneManager.LoadSceneAsync("Register", LoadSceneMode.Additive);
    }

    public void Login() {
        SceneManager.LoadSceneAsync("Login", LoadSceneMode.Additive);
    }

    public void JoinRandom() {
        SceneManager.LoadScene("Photon");
    }
}
