using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;
using Photon.Pun;
using Photon.Realtime;

public class AnimalSpawn : MonoBehaviourPunCallbacks, Photon.Pun.IPunObservable
{

    public Vector3 initialPoint;
    public GameObject gato;
    public int cantidad;

    // Start is called before the first frame update
    void Start()
    {
        cantidad = 0;
        initialPoint = new Vector3(0, 100f, 0);
        StartCoroutine(Spawn(5, initialPoint));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator Spawn(float needed, Vector3 initialPosition) {
        float randX = Random.Range(0.0f, 20.0f);
        float randZ = Random.Range(0.0f, 20.0f);
        Vector3 startPos = initialPosition;
        startPos += new Vector3(randX, randZ);

        while (needed != 0) {
            GameObject animal = PhotonNetwork.Instantiate("Gato Prefab", startPos, Quaternion.identity, 0);
            animal.transform.rotation = Quaternion.LookRotation(startPos);
            randX = Random.Range(0.0f, 20.0f);
            randZ = Random.Range(0.0f, 20.0f);
            startPos += new Vector3(randX, randZ);
            needed -= 1;
            cantidad += 1;
            yield return null;
        }

        yield return new WaitForSeconds(1.0f);
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting) {
            stream.SendNext(cantidad);
        } else {
            cantidad = (int)stream.ReceiveNext();   
        }
    }
}
