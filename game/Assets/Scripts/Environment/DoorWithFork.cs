namespace Assets.Scripts.Environment
{
    internal class DoorWithFork : Door
    {
        protected override bool IsUnlocked => PlayerItems.ForkIsCollected;
    }
}