

public interface IActor
{
    public void HorizontalMoveControl();
    public void Movement();
    public void StartRun();
    public void StopRun();
    public void StopHorizontalControl(bool controlIsLock = true);
}
