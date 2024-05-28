using UnityEngine;

namespace Occlusion
{
    public class OcclusionController : MonoBehaviour
    {
        public int rayAmount = 1500;
        public int rayDistance = 300;

        public float stayTime = 2f;

        public Camera camera;

        public Vector2[] rPoints;

        private void Awake()
        {
            rPoints = new Vector2[rayAmount];
            GetPoints();
        }

        private void Update()
        {
            CastRays();
        }

        private void GetPoints()
        {
            float x = 0;
            float y = 0;

            for (int i = 0; i < rayAmount; i++)
            {
                if (x > 1)
                {
                    x = 0;
                    y += 1 / Mathf.Sqrt(rayAmount);
                }

                rPoints[i] = new Vector2(x, y);
                x += 1 / Mathf.Sqrt(rayAmount);
            }
        }

        private void CastRays()
        {
            for (int i = 0; i < rayAmount; i++)
            {
                Ray ray;
                RaycastHit hit;
                OcclusionObject occlusionObject;
                
                ray = camera.ViewportPointToRay(new Vector3(rPoints[i].x, rPoints[i].y, 0));

                if (Physics.Raycast(ray, out hit, rayDistance))
                {
                    if (occlusionObject = hit.transform.GetComponent<OcclusionObject>())
                    {
                        occlusionObject.HitOcclude(stayTime);
                    }
                }
            }
        }
    }
}