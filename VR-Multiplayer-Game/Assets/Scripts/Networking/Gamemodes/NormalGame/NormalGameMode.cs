using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;
using Photon.Pun;

public class NormalGameMode : MonoBehaviourPun, IPunObservable {

    public float SpawnTime;
    float timer;
    bool HasPlayerSpawned = false;
    void Start() {
    }

    // Update is called once per frame
    void Update() {
        timer += Time.deltaTime;
        if (timer >= SpawnTime) {
            if (!HasPlayerSpawned) {
                PhotonNetwork.Instantiate("MaleFree1", new Vector3(0, 2, 0), Quaternion.identity, 0);
                HasPlayerSpawned = true;
            }
            timer = 0;
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
        if (stream.IsWriting) {
            
        } else if (stream.IsReading) {
            
        }
    }
}
