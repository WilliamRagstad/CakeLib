namespace CakeLang
{
    public abstract class ARecipe : IMCModel
    {
        File[] IMCModel.ToFiles()
        {
            throw new System.NotImplementedException();
        }
    }
}
