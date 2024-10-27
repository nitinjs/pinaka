using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Pinaka.Core
{
    public class DyanamicInstance<T> where T : class
    {
        private T _instance = null;
        public T? Instance
        {
            get
            {
                if (_instance == null)
                    _instance = (T?)Activator.CreateInstance(typeof(T));//default;
                return _instance;
            }
        }
        public bool SetParameter(string name, object value)
        {
            const BindingFlags flags = BindingFlags.Public | BindingFlags.Instance;
            var objectProperties = this.Instance.GetType().GetProperties(flags);
            foreach (var property in objectProperties.Where(p => name.Equals(p.Name, StringComparison.InvariantCultureIgnoreCase)))
            {
                if (property.PropertyType == typeof(System.Boolean))
                {
                    property.SetValue(this.Instance, true);//this is console app specific
                }
                else
                {
                    property.SetValue(this.Instance, Convert.ChangeType(value, property.PropertyType), null);
                }
                return true;
            }
            return false;
        }
    }
}
