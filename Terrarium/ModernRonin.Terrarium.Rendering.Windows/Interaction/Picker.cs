using System;
using System.Collections.Generic;
using ModernRonin.Terrarium.Logic;

namespace ModernRonin.Terrarium.Rendering.Windows.Interaction
{
    public class Picker : IPicker
    {
        readonly ICamera mCamera;
        readonly MouseDelta mMouseDelta;
        readonly Func<ISimulationState> mSimulationStateGetter;
        public Picker(MouseDelta mouseDelta, ICamera camera, Func<ISimulationState> simulationStateGetter)
        {
            mMouseDelta = mouseDelta;
            mCamera = camera;
            mSimulationStateGetter = simulationStateGetter;
        }
        public event Action<IEnumerable<Entity>> OnEntitiesPicked;
        public void Update()
        {
            if (!mMouseDelta.HasLeftBeenClicked) return;
            var worldCoordinates = mCamera.ScreenToWorldCoordinates(mMouseDelta.PointerPosition);
            var entities = mSimulationStateGetter().GetEntitiesAt(worldCoordinates);
            OnEntitiesPicked(entities);
        }
    }
}