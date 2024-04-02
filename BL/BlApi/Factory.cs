namespace BlApi;
//design pattern that wraps the BL(internal) so the PL will be able to create an object from type BL
public static class Factory
{
    public static IBl Get() => new BlImplementation.Bl();
}
