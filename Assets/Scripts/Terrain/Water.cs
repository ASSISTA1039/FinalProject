using UnityEngine;
using NaughtyAttributes;
[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class Water : MonoBehaviour
{
    [Header("Mesh Settings")]
    [Min(0)] public float width = 20;
    [Min(0)] public float height = 5;
    [Min(2)] public int vertexCount = 40;

    private MeshRenderer render;
    private MeshFilter filter;
    private Mesh mesh;
    private Vector3[] vertices;
    private int[] triangles;

    [Header("Physics Settings")]
    public float springConstant = 0.02f; 
    public float damping = 0.1f; 
    public float spread = 0.1f; 
    public float collisionVelocityFactor = 0.04f; 
    public float time = 5;

    private BoxCollider2D boxCollider;
    private BuoyancyEffector2D buoyancy;
    private float[] velocities;
    private float[] accelerations;
    private float[] leftDeltas;
    private float[] rightDeltas;
    private float timer;

    [Header("Render Settings")]
    public string sortingLayer = "Default";
    public int orderInLayer;

    private void OnValidate()
    {
        Create();
    }

    private void Awake()
    {
        Create();
        InitSimulation();
    }

    private void Update()
    {
        Simulation();
    }

    [Button("Create")]
    private void Create()
    {
        // Calculate vertex position
        vertices = new Vector3[vertexCount * 2];
        float interval = width / (vertexCount - 1);
        for (int i = 0; i < vertexCount; i++)
        {
            float x = i * interval - 0.5f* width;
            // Top Vertex
            vertices[i] = new Vector3(x, height * 0.5f, 0);
            // Bottom Vertex
            vertices[i + vertexCount] = new Vector3(x, -height *0.5f, 0);
        }

        // Set Triangle, Connect
        triangles = new int[(vertexCount - 1) * 6];
        int triIndex = 0;
        for (int i = 0; i < vertexCount - 1; i++)
        {
            triangles[triIndex++] = i;
            triangles[triIndex++] = i + 1;
            triangles[triIndex++] = i + vertexCount;

            triangles[triIndex++] = i + vertexCount;
            triangles[triIndex++] = i + 1;
            triangles[triIndex++] = i + vertexCount + 1;
        }

        // Mesh
        mesh = new Mesh();
        mesh.SetVertices(vertices);
        mesh.SetTriangles(triangles, 0);

        if (render == null)
            render = GetComponent<MeshRenderer>();
        render.sortingLayerName = sortingLayer;
        render.sortingOrder = orderInLayer;
        if (filter == null)
            filter = GetComponent<MeshFilter>();
        filter.sharedMesh = mesh;

        boxCollider = GetComponent<BoxCollider2D>();
        if (boxCollider == null)
            boxCollider = gameObject.AddComponent<BoxCollider2D>();
        boxCollider.size = new Vector2(width, height);
        boxCollider.isTrigger = true;


        boxCollider.usedByEffector = true;
        buoyancy = GetComponent<BuoyancyEffector2D>();
        if (buoyancy == null)
            buoyancy = gameObject.AddComponent<BuoyancyEffector2D>();
        //buoyancy.surfaceLevel = height *0.5f;

    }

    private void InitSimulation()
    {
        velocities = new float[vertexCount];
        accelerations = new float[vertexCount];
        leftDeltas = new float[vertexCount];
        rightDeltas = new float[vertexCount];
    }


    private void Simulation()
    {
        if (timer <= 0)
            return;
        timer -= Time.deltaTime;

        for (int i = 0; i < vertexCount; i++)
        {
            // Update location
            vertices[i] += new Vector3(0, velocities[i], 0);
            // Recalculation of force, acceleration, velocity
            float force = springConstant * (vertices[i].y - height * 0.5f) + velocities[i] * damping;
            accelerations[i] = -force;
            velocities[i] += accelerations[i];
        }

        for (int i = 0; i < vertexCount; i++)
        {
            //except first && last beacause array index out of bounds
            if (i > 0)
            {
                leftDeltas[i] = spread * (vertices[i].y - vertices[i - 1].y);
                velocities[i - 1] += leftDeltas[i];
            }
            if (i < vertexCount - 1)
            {
                rightDeltas[i] = spread * (vertices[i].y - vertices[i + 1].y);
                velocities[i + 1] += rightDeltas[i];
            }
        }

        mesh.SetVertices(vertices);
    }


    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player") && col.GetType().ToString() == "UnityEngine.CapsuleCollider2D")
        {
            Rigidbody2D rb = col.gameObject.GetComponent<Rigidbody2D>();
            Splash(col, rb.velocity.y * collisionVelocityFactor);
            Debug.Log(rb);
            
        }
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player") && col.GetType().ToString() == "UnityEngine.CapsuleCollider2D")
        {
            Vector2 velocity = col.GetComponent<Rigidbody2D>().velocity;
            //Continuously stay in the water force for the upper direction, here set the absolute value of the speed
            Stay(col, Mathf.Abs(velocity.x) * collisionVelocityFactor);
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player") && col.GetType().ToString() == "UnityEngine.CapsuleCollider2D")
        {
            Vector2 velocity = col.GetComponent<Rigidbody2D>().velocity;
            OutWater(col, velocity.y * collisionVelocityFactor);
        }
    }

    //Adding force when falling into water
    public void Splash(Collider2D col, float force)
    {
        timer = time;
        //The origin, because the drawn vertices are not world coordinates,
        //need to add the location of the origin to be the world coordinates
        Vector2 originalpos = transform.position;
        //Player position information
        float radius = col.bounds.max.x - col.bounds.min.x;
        Vector2 center = new Vector2(col.bounds.center.x, height*0.5f);
        for (int i = 0; i < vertexCount; i++)
        {
            //Calculate only X distance
            float dis = Vector2.Distance(new Vector2(originalpos.x + vertices[i].x, height * 0.5f), center);
            if (dis < radius)
            {
                //Deformation variables at the current frame
                velocities[i] = force * (radius - dis) / radius;
            }
        }
    }


    //Moving in the water
    private void Stay(Collider2D col, float force)
    {
        //Moving in water affects current speed, not gives initial speed
        Vector2 originalpos = transform.position;
        float radius = col.bounds.max.x - col.bounds.min.x;
        Vector2 center = new Vector2(col.bounds.center.x, height * 0.5f);
        for (int i = 0; i < vertexCount; i++)
        {
            float dis = Vector2.Distance(new Vector2(originalpos.x + vertices[i].x, height * 0.5f), center);
            if (dis < radius)
            {
                velocities[i] += (force) * dis / 5;
            }
        }
    }

    //Jumping out of the water
    private void OutWater(Collider2D col, float force)
    {
        Vector2 originalpos = transform.position;
        float radius = col.bounds.max.x - col.bounds.min.x;
        Vector2 center = new Vector2(col.bounds.center.x, height * 0.5f);
        for (int i = 0; i < vertexCount; i++)
        {
            float dis = Vector2.Distance(new Vector2(originalpos.x + vertices[i].x, height * 0.5f), center);
            if (dis < radius)
            {
                velocities[i] = force * (radius - dis) / radius;
            }
        }
    }
}
