using UnityEngine;
using System.Collections;



/// <summary>
/// 画出一个扇形的Mesh
/// </summary>
/// 

[RequireComponent(typeof(MeshRenderer), typeof(MeshFilter))]
public class SectorMesh : MonoBehaviour
{

    /// <summary>
    /// 定义一个画扇形的类
    /// </summary>
    #region
    private class SectorMeshCreator
    {
        private float radius;
        private float angleDegree;
        private int segments;

        private Mesh cacheMesh;

        /// <summary>  
        /// 创建一个扇形Mesh  
        /// </summary>  
        /// <param name="radius">扇形半价</param>  
        /// <param name="angleDegree">扇形角度</param>  
        /// <param name="segments">扇形弧线分段数</param>  
        /// <param name="angleDegreePrecision">扇形角度精度（在满足精度范围内，认为是同个角度）</param>  
        /// <param name="radiusPrecision">  
        /// <pre>  
        /// 扇形半价精度（在满足半价精度范围内，被认为是同个半价）。  
        /// 比如：半价精度为1000，则：1.001和1.002不被认为是同个半径。因为放大1000倍之后不相等。  
        /// 如果半价精度设置为100，则1.001和1.002可认为是相等的。  
        /// </pre>  
        /// </param>  
        /// <returns></returns>  
        //创建扇形网格，并且返回一个Mesh
        public Mesh CreateMesh(float radius, float angleDegree, int segments, int angleDegreePrecision, int radiusPrecision)
        {
            if (checkDiff(radius, angleDegree, segments, angleDegreePrecision, radiusPrecision))
            {
                Mesh newMesh = Create(radius, angleDegree, segments);
                if (newMesh != null)
                {
                    cacheMesh = newMesh;
                    this.radius = radius;
                    this.angleDegree = angleDegree;
                    this.segments = segments;
                }
            }
            return cacheMesh;
        }

        //创建扇形网格，主要画扇形网格的逻辑
        private Mesh Create(float radius, float angleDegree, int segments)
        {

            if (segments == 0)
            {
                segments = 1;
#if UNITY_EDITOR
                Debug.Log("segments must be larger than zero.");
#endif
            }

            Mesh mesh = new Mesh();
            //创建三角形面的顶点个数
            Vector3[] vertices = new Vector3[3 + segments - 1];
            vertices[0] = new Vector3(0, 0, 0);

            //将角度转化为弧度
            float angle = Mathf.Deg2Rad * angleDegree;
            float currAngle = angle / 2;
            float deltaAngle = angle / segments;
            //计算网格顶点的坐标
            for (int i = 1; i < vertices.Length; i++)
            {
                vertices[i] = new Vector3(Mathf.Cos(currAngle) * radius, 0, Mathf.Sin(currAngle) * radius);
                currAngle -= deltaAngle;
            }

            //计算链接三角形，计算图元
            int[] triangles = new int[segments * 3];
            for (int i = 0, vi = 1; i < triangles.Length; i += 3, vi++)
            {
                triangles[i] = 0;
                triangles[i + 1] = vi;
                triangles[i + 2] = vi + 1;
            }

            //重新赋值网格的顶点和三角形面
            mesh.vertices = vertices;
            mesh.triangles = triangles;

            //计算网格中的UV，重新绑定UVs
            Vector2[] uvs = new Vector2[vertices.Length];
            for (int i = 0; i < uvs.Length; i++)
            {
                uvs[i] = new Vector2(vertices[i].x, vertices[i].z);
            }
            mesh.uv = uvs;

            return mesh;
        }

        //检测输入的半径和圆周角是否正确
        private bool checkDiff(float radius, float angleDegree, int segments, int angleDegreePrecision, int radiusPrecision)
        {
            return segments != this.segments || (int)((angleDegree - this.angleDegree) * angleDegreePrecision) != 0 ||
                   (int)((radius - this.radius) * radiusPrecision) != 0;
        }
    }
    #endregion

    //定义变量
    #region
    public float radius = 2;   //半径
    public float angleDegree = 100;    //扇形的半径
    public int segments = 10;
    public int angleDegreePrecision = 1000;
    public int radiusPrecision = 1000;

    private MeshFilter meshFilter;

    private SectorMeshCreator creator = new SectorMeshCreator();



    #endregion

    [ExecuteInEditMode]
    private void Awake()
    {

        meshFilter = GetComponent<MeshFilter>();
    }

    private void Update()
    {
        meshFilter.mesh = creator.CreateMesh(radius, angleDegree, segments, angleDegreePrecision, radiusPrecision);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.gray;
        DrawMesh();
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        DrawMesh();
    }

    private void DrawMesh()
    {
        Mesh mesh = creator.CreateMesh(radius, angleDegree, segments, angleDegreePrecision, radiusPrecision);
        int[] tris = mesh.triangles;
        for (int i = 0; i < tris.Length; i += 3)
        {
            Gizmos.DrawLine(convert2World(mesh.vertices[tris[i]]), convert2World(mesh.vertices[tris[i + 1]]));
            Gizmos.DrawLine(convert2World(mesh.vertices[tris[i]]), convert2World(mesh.vertices[tris[i + 2]]));
            Gizmos.DrawLine(convert2World(mesh.vertices[tris[i + 1]]), convert2World(mesh.vertices[tris[i + 2]]));
        }
    }

    private Vector3 convert2World(Vector3 src)
    {
        return transform.TransformPoint(src);
    }
}


