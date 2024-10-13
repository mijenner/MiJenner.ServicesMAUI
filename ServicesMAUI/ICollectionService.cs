namespace MiJenner.ServicesMAUI
{
    public interface ICollectionService<T>
    {
        Task<List<T>> GetAll(); 
        Task Save(T item);
    }
}
