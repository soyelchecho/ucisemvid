using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.UI;

public class Register : MonoBehaviour {
    public InputField Username;
    public InputField Password;
    public InputField Email;
    
    public void CreateAccount() {
        RegisterPlayFabUserRequest request = new RegisterPlayFabUserRequest();
        request.Username = Username.text;
        request.Password = Password.text;
        request.Email = Email.text;
        request.DisplayName = Username.text;

        PlayFabClientAPI.RegisterPlayFabUser(request, result => {
            Debug.Log("OK");
        }, error => {
            Debug.Log(error);
        });
    }
}
