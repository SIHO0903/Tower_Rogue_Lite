using System;

[Serializable]
public class VolumeData
{
    public float bgm;
    public float sfx;
    public VolumeData(float bgm,float sfx)
    {
        this.bgm = bgm;
        this.sfx = sfx;
    }

}