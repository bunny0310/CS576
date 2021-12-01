using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Linq;

public class AI : Player
{

    // scan properties
    public float distance = 5;
    public float angle = 30;
    public float height = 3.2f;
    public Color meshColor = Color.red;
    public int scanFrequency = 30;
    public LayerMask layers = ~8;
    public LayerMask occlusionLayers = ~0;
    UnityEngine.AI.NavMeshAgent agent;

    public List<GameObject> objs
    {
        get
        {
            Objects.RemoveAll(o => !o);
            return Objects;
        }
        set { }
    }
    private List<GameObject> Objects = new List<GameObject>();

    GameObject human;
    Collider[] colliders = new Collider[50];
    Mesh mesh;
    int count;
    float scanInterval;
    float scanTimer;
    Renderer rend;



    public new void Start()
    {
        base.Start();
        gameObject.name = Enum.GetName(typeof(PLAYER_TYPE), PLAYER_TYPE.AI);
        scanInterval = 1.0f / scanFrequency;
        transform.LookAt(GameObject.Find("ArenaObjects/BaseBlue-L").transform);
        WalkForwards();
    }

    public new void Update()
    {
        base.Update();
        // IMPLEMENT ME - MAK
        scanTimer -= Time.deltaTime;
        if (scanTimer < 0)
        {
            scanTimer += scanInterval;
            var scannedObjects = Scan();
            if (scannedObjects.Count != 0)
            {
                GetComponent<WeaponIK>().targetTransform = scannedObjects[0].transform;
                SwitchToIdle();
                SetAimAndShoot(scannedObjects[0]);

            } else
            {
                GetComponent<WeaponIK>().FixRotation();
                GetComponent<WeaponIK>().targetTransform = null;
            }
        }
    }

    private List<GameObject> Scan()
    {
        colliders = new Collider[50];
        objs.Clear();
        count = Physics.OverlapSphereNonAlloc(transform.position, distance, colliders, layers, QueryTriggerInteraction.Collide);
        for(int i=0; i<count; ++i)
        {
            if (IsInsight(colliders[i].gameObject))
            {
                objs.Add(colliders[i].gameObject);
            }
        }
        
        return objs;
    }

    public bool IsInsight(GameObject obj)
    {
        Vector3 origin = transform.position;
        Vector3 destination = obj.transform.position;
        Vector3 direction = destination - origin;
        if (direction.y < 0 || direction.y > height)
        {
            return false;
        }
        direction.y = 0;
        float deltaAngle = Vector3.Angle(direction, transform.forward);
        if (deltaAngle > angle)
        {
            return false;
        }
        //origin.y += height / 2;
        //destination.y = origin.y;
        //if (Physics.Linecast(origin, destination, occlusionLayers))
        //{
        //    return false;
        //}
        return true;
    }

    Mesh CreateMesh()
    {

        Mesh mesh = new Mesh();

        int segments = 10;
        int triangleNum = (segments * 4) + 2 + 2;
        int verticesNum = triangleNum * 3;

        Vector3[] vertices = new Vector3[verticesNum];
        int[] triangles = new int[verticesNum];

        Vector3 bottomCenter = Vector3.zero;
        Vector3 bottomLeft = Quaternion.Euler(0, -angle, 0) * Vector3.forward * distance;
        Vector3 bottomRight = Quaternion.Euler(0, angle, 0) * Vector3.forward * distance;

        Vector3 topCenter = bottomCenter + Vector3.up * height;
        Vector3 topRight = bottomRight + Vector3.up * height;
        Vector3 topLeft = bottomLeft + Vector3.up * height;

        int vert = 0;

        //left side
        vertices[vert++] = bottomCenter;
        vertices[vert++] = bottomLeft;
        vertices[vert++] = topLeft;

        vertices[vert++] = topLeft;
        vertices[vert++] = topCenter;
        vertices[vert++] = bottomCenter;

        //right side
        vertices[vert++] = bottomCenter;
        vertices[vert++] = topCenter;
        vertices[vert++] = topRight;

        vertices[vert++] = topRight;
        vertices[vert++] = bottomRight;
        vertices[vert++] = bottomCenter;

        float curAngle = -angle;
        float deltaAngle = (angle * 2) / segments;
        for (int i = 0; i < segments; i++)
        {
            bottomLeft = Quaternion.Euler(0, curAngle, 0) * Vector3.forward * distance;
            bottomRight = Quaternion.Euler(0, curAngle + deltaAngle, 0) * Vector3.forward * distance;

            topRight = bottomRight + Vector3.up * height;
            topLeft = bottomLeft + Vector3.up * height;

            //far side
            vertices[vert++] = bottomCenter;
            vertices[vert++] = bottomRight;
            vertices[vert++] = topRight;

            vertices[vert++] = topRight;
            vertices[vert++] = topLeft;
            vertices[vert++] = bottomLeft;

            //top
            vertices[vert++] = topCenter;
            vertices[vert++] = topLeft;
            vertices[vert++] = topRight;

            //bottom
            vertices[vert++] = bottomCenter;
            vertices[vert++] = bottomRight;
            vertices[vert++] = bottomLeft;

            curAngle += deltaAngle;
        }

        for (int i = 0; i < verticesNum; i++)
        {
            triangles[i] = i;
        }

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();

        return mesh;
    }

    private void OnValidate()
    {
        mesh = CreateMesh();
    }

    private void OnDrawGizmos()
    {
        if (mesh)
        {
            Gizmos.color = meshColor;
            Gizmos.DrawMesh(mesh, transform.position, transform.rotation);
        }

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, distance);
        for (int i = 0; i < count; i++)
        {
            Gizmos.DrawSphere(colliders[i].transform.position, 0.2f);
        }

        Gizmos.color = Color.green;
        foreach (var obj in objs)
        {
            Gizmos.DrawSphere(obj.transform.position, 0.2f);
        }
    }

    void SetAimAndShoot(GameObject detected)
    {
        if (detected == null)
        {
            return;
        }
        gameObject.GetComponent<WeaponIK>().SetTargetTransform(detected.transform);
        Shoot(detected);
        Debug.Log(Pulses);
        if (IsDeactivated)
        {
            GetComponent<WeaponIK>().FixRotation();
            GetComponent<WeaponIK>().targetTransform = null;
            transform.LookAt(GameObject.Find("ArenaObjects/RedChargeStation").transform);
            WalkForwards();
        }
    }

    void RemoveAim()
    {
        gameObject.GetComponent<WeaponIK>().targetTransform = null;
    }

}
