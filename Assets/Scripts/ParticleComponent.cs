using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace Rain.Components
{
    [GenerateAuthoringComponent]
    public class ParticleComponent : IComponentData
    {
        public float3 velocity;
    }
}