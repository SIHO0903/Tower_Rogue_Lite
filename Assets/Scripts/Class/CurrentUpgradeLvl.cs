public class CurrentUpgradeLvl
{
    public int[] level = new int[8];
    public void Init()
    {
        for (int i = 0; i < level.Length; i++)
        {
            level[i] = 0;
        }
    }
}