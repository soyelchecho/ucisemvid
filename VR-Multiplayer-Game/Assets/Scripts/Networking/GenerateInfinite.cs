using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;
using Photon.Pun;
using Hashtable = ExitGames.Client.Photon.Hashtable;

internal class Tile {
    private GameObject tileGameObject;

    private float creationTime;

    public Tile(GameObject t, float ct) {
        tileGameObject = t;
        creationTime = ct;
    }

    public GameObject TileGameObject {
        get {return tileGameObject;} 
        set {tileGameObject = value;}
    }

    public float CreationTime {
        get {return creationTime;} 
        set {creationTime = value;}
    }
}

public class GenerateInfinite :MonoBehaviour {
    public GameObject plane;
    private GameObject[] players;
    private const float MAX_DISTANCE_ALLOWED = 15f;

    private bool boundariesWereGen = false;

    int planeSize = 20;
    int halfTilesX = 4;
    int halfTilesZ = 4;

    Vector3 startPos;

    Hashtable tiles = new Hashtable();

    private void Start() {
        StartCoroutine(getCurrentConnectedPlayers());
        this.gameObject.transform.position = Vector3.zero;
        startPos = Vector3.zero;

        float updateTime = Time.realtimeSinceStartup;

        for(int x=-halfTilesX;x< halfTilesX; x++) {
            for(int z = -halfTilesZ; z < halfTilesZ; z++) {
                Vector3 pos = new Vector3((x * planeSize + startPos.x), 0, (z * planeSize + startPos.z));
                GameObject t = (GameObject)Instantiate(plane, pos, Quaternion.identity);

                string tileName = "Tile_" + ((int)(pos.x)).ToString() + "_" + ((int)(pos.z)).ToString();
                t.name = tileName;
                Tile tile = new Tile(t, updateTime);
                tiles.Add(tileName, tile);
            }
        }
    }

    IEnumerator getCurrentConnectedPlayers() {
        while (true) {
            players = GameObject.FindGameObjectsWithTag("Player");
            yield return new WaitForSeconds(2f);
        }
    }

    public void Update() {
        if (players != null && players.Length != 0) {
            if (findMaxDistanceBetweenPlayers(players) < MAX_DISTANCE_ALLOWED) {
                if (boundariesWereGen) {
                    cleanBoundariesInTiles(tiles);
                    boundariesWereGen = false;
                }
                foreach (GameObject player in players) {
                    generatePlaneBasedPlayer(player);
                }
            } else {
                float[] corners = findCornersInTiles(tiles);
                activateCornerBoundariesInTiles(corners, tiles);
                boundariesWereGen = true;
            }
        }
    }

    private void generatePlaneBasedPlayer(GameObject player) {
        int xMove = (int)(player.transform.position.x - startPos.x);
        int zMove = (int)(player.transform.position.z - startPos.z);

        if(Mathf.Abs(xMove) >= planeSize || Mathf.Abs(zMove) >= planeSize) {
            float updateTime = Time.realtimeSinceStartup;

            int playerX = (int)(Mathf.Floor(player.transform.position.x / planeSize) * planeSize);
            int playerZ = (int)(Mathf.Floor(player.transform.position.z / planeSize) * planeSize);

            for (int x = -halfTilesX; x < halfTilesX; x++) {
                for (int z = -halfTilesZ; z < halfTilesZ; z++) {
                    Vector3 pos = new Vector3((x * planeSize + playerX), 0, (z * planeSize + playerZ));
                    string tileName = "Tile_" + ((int)(pos.x)).ToString() + "_" + ((int)(pos.z)).ToString();

                    if (!tiles.ContainsKey(tileName)) {
                        GameObject t = (GameObject)Instantiate(plane, pos, Quaternion.identity);
                        t.name = tileName;
                        Tile tile = new Tile(t, updateTime);
                        tiles.Add(tileName, tile);

                    }
                    else {
                        (tiles[tileName] as Tile).CreationTime = updateTime;
                    }
                }
            }

            Hashtable newTerrain= new Hashtable();
            foreach(Tile tls in tiles.Values) {
                if (tls.CreationTime != updateTime) {
                    Destroy(tls.TileGameObject);
                }
                else {
                    newTerrain.Add(tls.TileGameObject.name, tls);
                }
            }
            tiles = newTerrain;
            startPos = player.transform.position;
        }
    }

    private void cleanBoundariesInTiles(Hashtable tiles) {
        foreach (Tile tls in tiles.Values) {
            Transform boundBox = tls.TileGameObject.transform.GetChild(0);
            for (int i = 0; i < boundBox.childCount; i++) {
                boundBox.transform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }

    private void activateCornerBoundariesInTiles(float[] corners, Hashtable tiles) {
        for (int i = 0; i < corners.Length; i++) {
            float corner = corners[i];
            foreach (Tile tls in tiles.Values) {
                GameObject tile = tls.TileGameObject;
                if (tile.transform.position.x == corner) {
                    if (i == 0 || i == 1) {
                        tile.transform.GetChild(0).GetChild(i).gameObject.SetActive(true);
                    }
                }

                if (tile.transform.position.z == corner) {
                    if (i == 2 || i == 3) {
                        tile.transform.GetChild(0).GetChild(i).gameObject.SetActive(true);
                    }
                }
            }
        }
    }

    /*
    cornerValues[0] = maxX
    cornerValues[1] = minX
    cornerValues[2] = maxZ
    cornerValues[3] = minZ
    */
    private float[] findCornersInTiles(Hashtable tiles) {
        float[] cornerValues = {float.MinValue, float.MaxValue, float.MinValue, float.MaxValue};
        foreach (Tile tls in tiles.Values) {
            Vector3 pos = tls.TileGameObject.transform.position;
            if (pos.x > cornerValues[0]) cornerValues[0] = pos.x;
            if (pos.x < cornerValues[1]) cornerValues[1] = pos.x;
            if (pos.z > cornerValues[2]) cornerValues[2] = pos.z;
            if (pos.z < cornerValues[3]) cornerValues[3] = pos.z;
        }

        return cornerValues;
    }

    private float findMaxDistanceBetweenPlayers(GameObject[] players) {
        float maxDistance = 0f;
        for (int i = 0; i < players.Length; i++) {
            for (int j = i; j < players.Length; j++) {
                float distance = Vector3.Distance(players[i].transform.position, players[j].transform.position);
                if (distance > maxDistance) {
                    maxDistance = distance;
                }
            }
        }

        return maxDistance;
    }
}
