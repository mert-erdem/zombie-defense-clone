namespace Game.Scripts.Environment
{
    [System.Serializable]
    public class AreaData
    {
        public int Level { get; private set; }
    
        public bool[] PlatformActives { get; private set; }
    
        public bool[] GateActives { get; private set; }

        public AreaData(AreaManager areaManager)
        {
            Level = areaManager.CurrentLevel;
            PlatformActives = areaManager.PlatformActives;
            GateActives = areaManager.GateActives;
        }
    }
}
