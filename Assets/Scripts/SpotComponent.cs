using System;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;

[GenerateAuthoringComponent]
public struct SpotComponent : IComponentData
{
    public float3 Velocity;
}
