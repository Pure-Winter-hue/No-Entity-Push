// Vintage Story 1.21, .NET 8
using Vintagestory.API.Common;
using Vintagestory.API.Common.Entities;

namespace NoEntityPush
{
    public class NoEntityPushModSystem : ModSystem
    {
        public override bool ShouldLoad(EnumAppSide side) => true;

        public override void Start(ICoreAPI api)
        {
            // Run on both client & server so physics/visuals match.
            api.Event.OnEntitySpawn += KillRepulse;
            api.Event.OnEntityLoaded += KillRepulse;
        }

        private void KillRepulse(Entity e)
        {
            if (e == null || e is EntityPlayer) return;

            // If this entity has the repulse-agents behavior, null it so it stops shoving!
            if (e.BHRepulseAgents != null)
            {
                e.BHRepulseAgents = null;

                // (Optional) Shrink "touch" radius to avoid any fallback nudge math. Meh.
                e.touchDistance = 0;
                e.touchDistanceSq = 0;
            }
        }
    }
}
