public class MockCrate : Crate
{
    public bool wasInteractedWith = false;

    public override void InteractWithCrate()
    {
        wasInteractedWith = true;
    }
}