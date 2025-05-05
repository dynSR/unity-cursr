using JetBrains.Annotations;
using UnityEngine;
using UnityTools.Library.Extensions;

namespace GridSystem.Runtime.Helpers {
    public class CellMesh {
        private GameObject go;
        private Mesh mesh;

        private readonly Vector3[] vertices = new Vector3[4];
        private readonly Vector2[] uv = new Vector2[4];
        private readonly int[] triangles = new int[6];

        private CellMesh() => Init();

        private void Init() {
            GenerateMeshData();

            mesh = new Mesh {
                name = "Cell Mesh"
            };
            go = new GameObject("Cell", typeof(MeshRenderer), typeof(MeshFilter));
            go.SetLocalRotation(new Vector3(90, 0, 0));
            go.GetComponent<MeshFilter>().mesh = mesh;

            mesh.vertices = vertices;
            mesh.uv = uv;
            mesh.triangles = triangles;
        }

        private void GenerateMeshData() {
            vertices[0] = Vector3.zero;
            vertices[1] = new Vector3(0, 1, 0);
            vertices[2] = new Vector3(1, 1, 0);
            vertices[3] = new Vector3(1, 0, 0);

            triangles[0] = 0;
            triangles[1] = 1;
            triangles[2] = 2;

            triangles[3] = 0;
            triangles[4] = 2;
            triangles[5] = 3;

            uv[0] = new Vector2(0, 0);
            uv[1] = new Vector2(0, 1);
            uv[2] = new Vector2(1, 1);
            uv[3] = new Vector2(1, 0);
        }

        public Vector3 GetWorldPosition() => go.transform.position;

        public static class Factory {
            public static CellMesh Create(
                Material material,
                [CanBeNull] Transform parent = null,
                [CanBeNull] Vector3? scale = null,
                [CanBeNull] Vector3? position = null,
                [CanBeNull] string name = null
            ) {
                CellMesh cellMesh = new();
                cellMesh.go.GetComponent<MeshRenderer>().material = material;

                if (parent != null)
                    cellMesh.go.SetParent(parent);

                if (scale != null)
                    cellMesh.go.SetScale(scale.Value);

                if (position != null)
                    cellMesh.go.SetLocalPosition(position.Value);

                if (name != null)
                    cellMesh.go.name = name;

                return cellMesh;
            }
        }
    }
}