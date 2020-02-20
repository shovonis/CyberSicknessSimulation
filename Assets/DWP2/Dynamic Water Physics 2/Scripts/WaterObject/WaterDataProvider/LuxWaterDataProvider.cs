using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DWP2
{
    public class LuxWaterDataProvider : WaterDataProvider
    {
#if DWP_LUX
        public Material WaterMaterial;

        private LuxWaterUtils.GersterWavesDescription Description;

        public override void Initialize()
        {
            base.Initialize();

            WaterMaterial = waterObject.GetComponent<MeshRenderer>()?.material;
            if(WaterMaterial == null)
            {
                Debug.LogError("Lux water object does not contain a mesh renderer or material.");
            }
            LuxWaterUtils.GetGersterWavesDescription(ref Description, WaterMaterial);

            waterHeightOffset = waterObject.transform.position.y;
        }

        public override float GetWaterHeight(Vector3 point)
        {
            return LuxWaterUtils.GetGestnerDisplacement(point, Description, 0).y;
        }
#endif
    }
}

