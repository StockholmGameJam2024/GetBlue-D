using System.Collections.Generic;
using UnityEngine;

namespace SoftBody
{
    public class SoftBodyCircle : MonoBehaviour
    {
        public int vertexCount = 20;
        public float radius = 1f;
        public GameObject nodePrefab;
        public List<GameObject> points;
        public Mesh mesh;
        public GooglyEye googlyEyePrefab;
        private GooglyEye[] googlyEyes = new GooglyEye[2];

        [ContextMenu("GenerateCircleMesh")]
        public void GenerateCircleMesh()
        {
            var meshFilter = gameObject.GetOrAddComponent<MeshFilter>();
            meshFilter.mesh = CreateCircleMesh(vertexCount);
            gameObject.GetOrAddComponent<MeshRenderer>();
        }

        public Vector3[] vertices;
        public int[] triangles;
        public Vector2[] uv;
        public Vector3[] normals;

        /// <summary>
        /// Generates a 2D Circle Mesh based on the parameters specified in the component fields.
        /// </summary>
        /// <param name="vertexCount">How many outer edge vertices do you want?</param>
        /// <returns>A 2D circle Mesh</returns>
        Mesh CreateCircleMesh(int vertexCount)
        {
            Mesh mesh = new Mesh();

            vertices = new Vector3[vertexCount + 1];
            triangles = new int[vertexCount * 3];
            uv = new Vector2[vertexCount + 1];
            normals = new Vector3[vertexCount + 1];

            // Center vertex
            vertices[0] = Vector3.zero;
            uv[0] = new Vector2(0.5f, 0.5f);
            normals[0] = Vector3.up;

            // Generate vertices in a circle
            for (int i = 1; i <= vertexCount; i++)
            {
                float angle = (float)(i - 1) / vertexCount * Mathf.PI * 2f;
                vertices[i] = new Vector3(Mathf.Cos(angle) * radius, Mathf.Sin(angle) * radius, 0f);
                uv[i] = new Vector2(vertices[i].x / (radius * 2) + 0.5f, vertices[i].y / (radius * 2) + 0.5f);
                
                // Calculate normals
                normals[i] = Vector3.up;
                //float normalAngle = angle + Mathf.PI / 3f; // Rotate by 60 degrees
                //normals[i] = new Vector3(Mathf.Cos(normalAngle), Mathf.Sin(normalAngle), 0f).normalized;
            }

            // Generate triangles
            for (int i = 0; i < vertexCount; i++)
            {
                triangles[i * 3] = 0;
                triangles[i * 3 + 2] = i + 1;
                triangles[i * 3 + 1] = (i + 1) % vertexCount + 1;
            }

            mesh.vertices = vertices;
            mesh.triangles = triangles;
            mesh.uv = uv;
            mesh.normals = normals;

            //mesh.RecalculateNormals();

            return mesh;
        }

        /// <summary>
        /// Generates edges with rigidbodies and colliders to simulate soft body physics for each outer edge of the circle mesh
        /// </summary>
        void Start()
        {
            GenerateCircleMesh();
            mesh = GetComponent<MeshFilter>().mesh;
            var vertices = mesh.vertices;
            points = new List<GameObject>();
            for (int i = 1; i < vertices.Length; i++)
            {
                GameObject childObject = Instantiate(nodePrefab, gameObject.transform.position + vertices[i], Quaternion.identity);
                childObject.transform.parent = gameObject.transform;
                points.Add(childObject);
                childObject.GetComponent<CircleCollider2D>().offset = vertices[i].normalized * -childObject.GetComponent<CircleCollider2D>().radius;
                childObject.GetComponent<Rigidbody2D>().gravityScale = GetComponent<Rigidbody2D>().gravityScale;
            }
            googlyEyes[0] = Instantiate(googlyEyePrefab, points[0].transform);
            googlyEyes[0].transform.localPosition = new Vector3(-0.3f * radius, 0.3f * radius, 0f);
            googlyEyes[0].transform.localScale = new Vector3(radius * 0.3f, radius * 0.3f, 1f);
            googlyEyes[1] = Instantiate(googlyEyePrefab, points[0].transform);
            googlyEyes[1].transform.localPosition = new Vector3(-0.3f * radius, -0.3f * radius, 0f);
            googlyEyes[1].transform.localScale = new Vector3(radius * 0.3f, radius * 0.3f, 1f);

            for (int i = 0; i < points.Count; i++)
            {
                if (i == points.Count - 1)
                    points[i].GetComponent<HingeJoint2D>().connectedBody = points[0].GetComponent<Rigidbody2D>();
                else
                    points[i].GetComponent<HingeJoint2D>().connectedBody = points[i + 1].GetComponent<Rigidbody2D>();
            }

            SecureCenterBody(vertexCount/2);
        }

        /// <summary>
        /// This function makes it a bit more difficult for the center body to bounce outside of its outer edge colliders.
        /// </summary>
        /// <param name="anchorCount">The number of nodes to hook the center node onto.</param>
        void SecureCenterBody(int anchorCount)
        {
            for (int i = 0; i < anchorCount; i++)
            {
                var firstDistanceJoint = gameObject.AddComponent<SpringJoint2D>();
                firstDistanceJoint.connectedBody = points[points.Count*i/anchorCount].GetComponent<Rigidbody2D>();
                //firstDistanceJoint.di = true;
                firstDistanceJoint.dampingRatio = 0.9f;
                firstDistanceJoint.autoConfigureDistance = false;
                firstDistanceJoint.distance = Vector2.Distance(transform.position, points[points.Count*i/anchorCount].transform.position) * 1.0f;
            }   
        }

        /// <summary>
        /// Updates the outer edge mesh vertices to match the vertices of the edge colliders
        /// </summary>
        void Update()
        {
            for (int i = 0; i < points.Count; i++)
            {
                vertices[i+1] = points[i].transform.localPosition;
                {
                    mesh.vertices = vertices;
                }
            }
        }
    }
}