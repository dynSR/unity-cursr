using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;
using UnityTools.Library.Extensions;

namespace GridSystem.Runtime.Core {
    public class CellMesh {
        private GameObject gameObject;
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
            gameObject = new GameObject("Cell", typeof(MeshRenderer), typeof(MeshFilter), typeof(GridCell)) {
                isStatic = true,
                transform = { localEulerAngles = new Vector3(90f, 0, 0) }
            };

            MeshRenderer meshRenderer = gameObject.GetComponent<MeshRenderer>();
            meshRenderer.shadowCastingMode = ShadowCastingMode.Off;
            meshRenderer.receiveShadows = false;
            meshRenderer.lightProbeUsage = LightProbeUsage.Off;
            meshRenderer.motionVectorGenerationMode = MotionVectorGenerationMode.ForceNoMotion;
            // Disable Contribute Global Illumination
            GameObjectUtility.SetStaticEditorFlags(gameObject, StaticEditorFlags.BatchingStatic);

            gameObject.GetComponent<MeshFilter>().mesh = mesh;

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

        public Vector3 GetWorldPosition() => gameObject.transform.position;

        public static class Factory {
            public static CellMesh Create(
                Material material,
                [CanBeNull] Transform parent = null,
                [CanBeNull] Vector3? scale = null,
                [CanBeNull] Vector3? position = null,
                [CanBeNull] string name = null
            ) {
                CellMesh cellMesh = new();
                cellMesh.gameObject.GetComponent<MeshRenderer>().material = material;

                if (parent is not null)
                    cellMesh.gameObject.SetParent(parent);

                if (scale is not null)
                    cellMesh.gameObject.SetScale(scale.Value);

                if (position is not null)
                    // cellMesh.gameObject.SetLocalPosition(position.Value);
                    cellMesh.gameObject.SetLocalPosition(new Vector3(
                        position.Value.x - cellMesh.gameObject.transform.localScale.x / 2f,
                        position.Value.y,
                        position.Value.z - cellMesh.gameObject.transform.localScale.y / 2f
                    ));

                if (name is not null)
                    cellMesh.gameObject.name = name;

                return cellMesh;
            }
        }
    }
}