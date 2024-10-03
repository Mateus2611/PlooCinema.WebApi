using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlooCinema.WebApi.Repositories.PostgreSql
{
    public interface IDefaultQueries<T>
    {
        T? Create(T entity);
        IEnumerable<T> SearchAll();
        // IEnumerable<Movie> SearchByName(string name);
        // Movie? SearchById(int id);
        T? Update(T entity);
        void Delete(T entity);
    }
}