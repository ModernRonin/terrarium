using System;
using System.Collections.Generic;
using ModernRonin.Terrarium.Logic;

namespace ModernRonin.Terrarium.Rendering.Windows.Interaction
{
    public class Picker : IPicker
    {
        readonly ICamera mCamera;
        readonly MouseDelta mMouseDelta;
        readonly ISimulationState mSimulationState;
        public Picker(MouseDelta mouseDelta, ICamera camera, ISimulationState simulationState)
        {
            mMouseDelta = mouseDelta;
            mCamera = camera;
            mSimulationState = simulationState;
        }
        public event Action<IEnumerable<Entity>> OnEntitiesPicked;
        public void Update()
        {
            if (!mMouseDelta.HasLeftBeenClicked) return;
            var worldCoordinates = mCamera.ScreenToWorldCoordinates(mMouseDelta.PointerPosition);
            var entities = mSimulationState.GetEntityAt(worldCoordinates);
            OnEntitiesPicked(entities);
        }
    }
}