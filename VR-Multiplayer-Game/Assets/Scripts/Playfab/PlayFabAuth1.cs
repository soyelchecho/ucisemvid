using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayFabAuth1 : MonoBehaviour {

    public InputField Username;
    public InputField Password;
    public string LevelToLoad;
    
    public void Login() {
        LoginWithPlayFabRequest request = new LoginWithPlayFabRequest();
        request.Username = Username.text;
        request.Password = Password.text;

        PlayFabClientAPI.LoginWithPlayFab(request, result => {
            Debug.Log("OK");
            SceneManager.LoadScene(LevelToLoad);
        }, error => {
            Debug.Log(error.ErrorMessage);
        });
    }
}
