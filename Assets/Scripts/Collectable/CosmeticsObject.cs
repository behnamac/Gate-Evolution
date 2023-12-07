
public class CosmeticsObject : Triggable
{
    protected override void Awake()
    {
        base.Awake();
        SetCollectBehavior(new GoodItem());
        SetDestroyBehavior(new DestoryMode());
    }
}
