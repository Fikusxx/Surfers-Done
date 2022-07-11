using System;

[Serializable]
public class SaveState
{

    [NonSerialized] private const int HAT_COUNT = 12;

    public int Highscore { get; set; }
    public int Fish { get; set; }
    public DateTime LastSaveTime { get; set; }

    public int CurrentHatIndex { get; set; }
    public int[] UnlockedHatFlag { get; set; }



    public SaveState()
    {
        Fish = 0;
        Highscore = 0;
        LastSaveTime = DateTime.Now;

        CurrentHatIndex = 0;

        UnlockedHatFlag = new int[HAT_COUNT];
        UnlockedHatFlag[0] = 1;
    }
}
