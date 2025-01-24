namespace TasksAPI.Interfaces;

public interface IRepository<T>
{
    T? Add(T t);
    List<T>? GetAll();
    List<T>? GetById(int id);
    List<T>? Update(int id, T t);
    bool? Delete(int id);
}