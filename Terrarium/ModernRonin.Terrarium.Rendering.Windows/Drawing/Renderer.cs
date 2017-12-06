using System.Linq;
using ModernRonin.Terrarium.Logic;

namespace ModernRonin.Terrarium.Rendering.Windows.Drawing
{
    public class Renderer
    {
        readonly BackgroundRenderer mBackgroundRenderer;
        readonly EnergyDensityRenderer mEnergyDensityRenderer;
        readonly EntityRenderer mEntityRenderer;
        public Renderer(
            EnergyDensityRenderer energyDensityRenderer,
            BackgroundRenderer backgroundRenderer,
            EntityRenderer entityRenderer)
        {
            mEnergyDensityRenderer = energyDensityRenderer;
            mBackgroundRenderer = backgroundRenderer;
            mEntityRenderer = entityRenderer;
        }
        public void Render(ISimulationState simulationState)
        {
            mBackgroundRenderer.Render(simulationState.Size);
            mEnergyDensityRenderer.Render(simulationState.EnergyDensity);
            mEntityRenderer.Render(simulationState.Entities.Select(e => e.State));
        }
    }
}