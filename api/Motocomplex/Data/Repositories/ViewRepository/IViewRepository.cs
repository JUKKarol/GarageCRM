namespace Motocomplex.Data.Repositories.ViewRepository
{
    public interface IViewRepository
    {
        string modelWithBrandName { get; }

        FormattableString Select(string viewName);
    }
}
