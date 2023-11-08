namespace Motocomplex.Data.Repositories.ViewRepository
{
    public class ViewRepository : IViewRepository
    {
        public string modelWithBrandName { get; }  = "view_ModelWithBrandName";

        public FormattableString Select(string viewName)
        {
            return $"SELECT * FROM [{viewName}]";
        }
    }
}
