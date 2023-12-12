
public class GunModel : FireBaseModel,ModelFunInterface
{
    public float gundamage;
    public void Init()
    {
        MaxBullets = 35;
        CurrentBullets = 0;
        gundamage = 10;

    }
    public BulletData GetBulletData()
    {
        return Bulletdata;
    }
    public void SetBulletData(BulletData bd)
    {
        Bulletdata = bd;
    }
}
