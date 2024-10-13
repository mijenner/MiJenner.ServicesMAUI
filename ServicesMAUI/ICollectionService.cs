using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiJenner.ServicesMAUI
{
    public interface ICollectionService<T>
    {
        Task<List<T>> GetAll(); 
        Task Save(T item);
    }
}
