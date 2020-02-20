using System.Collections.Generic;
using UnityEngine;

#if DWP_CREST
using Crest;
#endif

namespace DWP2
{
    public class CrestWaterDataProvider : WaterDataProvider
    {
#if DWP_CREST
        class QueryToken { }

        private SamplingData samplingData = new SamplingData();
        private SamplingData samplingDataFlow = new SamplingData();
        
        private OceanRenderer oceanRenderer;
        private ICollProvider collProvider;
        private bool _initialized;
        private Rect rect;
        private float height;

        private Vector3[] worldPoints;
        private float[] waterHeights;

        QueryToken qt0, qt1, qt2;

        public override void Initialize()
        {
            base.Initialize();

            if (waterObject == null)
            {
                Debug.LogWarning("No objects tagged 'Water' found.");
                return;
            }
            
            oceanRenderer = waterObject.GetComponent<OceanRenderer>();
            if (oceanRenderer == null)
            {
                Debug.LogError(
                    "A gameobject tagged 'Water' has been found but it does not contain CREST's OceanRenderer component. " +
                    "You have defined DWP_CREST and therefore CREST's OceanRenderer component is required. ");
            }
            else
            {
                collProvider = OceanRenderer.Instance.CollisionProvider;

                _initialized = true;
            }

            samplingData = new SamplingData();
            samplingData._minSpatialLength = 0f;
            qt0 = new QueryToken();
            qt1 = new QueryToken();
            qt2 = new QueryToken();

            worldPoints = new Vector3[1];
            waterHeights = new float[1];
        }

        /// <summary>
        /// Returns water height at each given point.
        /// </summary>
        /// <param name="p0s">Position array in local coordinates.</param>
        /// <param name="p1s">Position array in local coordinates.</param>
        /// <param name="p2s">Position array in local coordinates.</param>
        /// <param name="waterHeights0">Water array height in world coordinates. Corresponds to p0s.</param>
        /// <param name="waterHeights1">Water array height in world coordinates. Corresponds to p1s.</param>
        /// <param name="waterHeights2">Water array height in world coordinates. Corresponds to p2s.</param>
        /// <param name="localToWorldMatrices">Maxtrix to convert from local to world coordinates.</param>
        public override void GetWaterHeights(ref Vector3[] p0s, ref Vector3[] p1s, ref Vector3[] p2s,
            ref float[] waterHeights0, ref float[] waterHeights1, ref float[] waterHeights2, ref Matrix4x4[] localToWorldMatrices)
        {
            Debug.Assert(p0s.Length == waterHeights0.Length, "Points and WaterHeights arrays must have same length.");

            int n = p0s.Length;

            // Fill in with 0s if no water object set
            if (waterObject == null || !queryWaterHeights)
            {
                for (int i = 0; i < n; i++)
                {
                    waterHeights0[i] = 0;
                    waterHeights1[i] = 0;
                    waterHeights2[i] = 0;
                }
            }
            else
            {
                int worldPointCount = n * 3;
                if (worldPointCount != worldPoints.Length) worldPoints = new Vector3[worldPointCount];
                if (worldPointCount != waterHeights.Length) waterHeights = new float[worldPointCount];

                int n2 = n * 2;
                for (int i = 0; i < n; i++)
                {
                    worldPoints[i] = localToWorldMatrices[i].MultiplyPoint3x4(p0s[i]);
                    worldPoints[n + i] = localToWorldMatrices[i].MultiplyPoint3x4(p1s[i]);
                    worldPoints[n2 + i] = localToWorldMatrices[i].MultiplyPoint3x4(p2s[i]);
                }

                collProvider.Query(GetHashCode(), samplingData, worldPoints, waterHeights, null, null);

                System.Array.Copy(waterHeights, waterHeights0, n);
                System.Array.Copy(waterHeights, n, waterHeights1, 0, n);
                System.Array.Copy(waterHeights, n2, waterHeights2, 0, n);
            }
        }
        #endif
    }
}

