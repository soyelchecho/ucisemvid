using System.Collections;
using UnityEngine;

public class GenerateTerrain : MonoBehaviour {
    private const float BASE_SEED = 420f;
    private float[] posibleAngles = {-180f, 180f, 90f, -90f};

    public GameObject[] trees;
    public GameObject[] envDecorations;


    private void Start() {
        Vector3 planeCentre = this.GetComponent<Collider>().bounds.center;
        Random.InitState(cantorPairingFunction(planeCentre.x, planeCentre.z));
        Mesh mesh = this.GetComponent<MeshFilter>().mesh;
        Vector3[] vertices = mesh.vertices;
        for(int v = 0; v < vertices.Length; v++) {
            double ran = Random.Range(0, 1000);
            if(ran<1) {
                Vector3 treePos = new Vector3(vertices[v].x + this.transform.position.x, vertices[v].y, vertices[v].z + this.transform.position.z);
                int test = Random.Range(0, 3);
                GameObject t;
                t = (GameObject)Instantiate(trees[test], treePos, Quaternion.identity);
                t.transform.parent = this.transform;
            } else if (ran>2 & ran<6) {
                Vector3 animalPos = new Vector3(vertices[v].x + this.transform.position.x, vertices[v].y, vertices[v].z + this.transform.position.z);
                GameObject envDecoration = (GameObject) Instantiate(envDecorations[Random.Range(0, envDecorations.Length)], animalPos, Quaternion.identity);

                float angle = Random.Range(0, 360);
                envDecoration.transform.eulerAngles += new Vector3(0, angle, 0);
                envDecoration.transform.parent = this.transform;
            }
        }

        float anglesToRotateInY = posibleAngles[Random.Range(0, posibleAngles.Length)];
        this.transform.eulerAngles += new Vector3(0f,  anglesToRotateInY, 0f);
        this.transform.GetChild(0).transform.eulerAngles += new Vector3(0f, - anglesToRotateInY, 0f);
        mesh.vertices = vertices;
        mesh.RecalculateBounds();
        mesh.RecalculateNormals();
        this.gameObject.AddComponent<MeshCollider>();
    }

    private int cantorPairingFunction(float x, float z) {
        return (int) (0.5f * (x + z) * (x + z + 1f) + z);
    }
}
