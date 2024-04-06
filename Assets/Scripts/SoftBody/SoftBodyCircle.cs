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

        [ContextMenu("GenerateCircleMesh")]
        public void GenerateCircleMesh()
        {
            var meshFilter = gameObject.GetOrAddComponent<MeshFilter>();
            meshFilter.mesh = CreateCircleMesh(vertexCount);
            gameObject.GetOrAddComponent<MeshRenderer>();
        }

        public Vector3[] vertices;
        public int[] triangles;

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

            // Center vertex
            vertices[0] = Vector3.zero;

            // Generate vertices in a circle
            for (int i = 1; i <= vertexCount; i++)
            {
                float angle = (float)(i - 1) / vertexCount * Mathf.PI * 2f;
                vertices[i] = new Vector3(Mathf.Cos(angle) * radius, Mathf.Sin(angle) * radius, 0f);
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

            mesh.RecalculateNormals();

            return mesh;
        }

        /// <summary>
        /// Generates edges with rigidbodies and colliders to simulate soft body physics for each outer edge of the circle mesh
        /// </summary>
        void Start()
        {
            mesh = GetComponent<MeshFilter>().mesh;
            var vertices = mesh.vertices;
            points = new List<GameObject>();
            for (int i = 1; i < vertices.Length; i++)
            {
                GameObject childObject = Instantiate(nodePrefab, gameObject.transform.position + vertices[i], Quaternion.identity);
                childObject.transform.parent = gameObject.transform;
                points.Add(childObject);
                childObject.GetComponent<CircleCollider2D>().offset = vertices[i].normalized * -childObject.GetComponent<CircleCollider2D>().radius;
            }

            for (int i = 0; i < points.Count; i++)
            {
                if (i == points.Count - 1)
                    points[i].GetComponent<HingeJoint2D>().connectedBody = points[0].GetComponent<Rigidbody2D>();
                else
                    points[i].GetComponent<HingeJoint2D>().connectedBody = points[i + 1].GetComponent<Rigidbody2D>();
            }

            SecureCenterBody(3);
        }

        /// <summary>
        /// This function makes it a bit more difficult for the center body to bounce outside of its outer edge colliders.
        /// </summary>
        /// <param name="anchorCount">The number of nodes to hook the center node onto.</param>
        void SecureCenterBody(int anchorCount)
        {
            for (int i = 0; i < anchorCount; i++)
            {
                var firstDistanceJoint = gameObject.AddComponent<DistanceJoint2D>();
                firstDistanceJoint.connectedBody = points[points.Count*i/anchorCount].GetComponent<Rigidbody2D>();
                firstDistanceJoint.maxDistanceOnly = true;
                firstDistanceJoint.autoConfigureDistance = false;
                firstDistanceJoint.distance = Vector2.Distance(transform.position, points[points.Count*i/anchorCount].transform.position) * 1.5f;
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