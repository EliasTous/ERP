using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Session
{
    /// <summary>
    /// Session storage interface
    /// </summary>
    public interface ISessionStorage
    {

        void Save(string key, object value);
        object Retrieve(string key);


        void Clear();
    }
}
