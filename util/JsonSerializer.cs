using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pokemonStorageSystem.util {
    public class JsonSerializer : ISerializer {
        public T Deserialize<T>(StreamReader sr) where T : new() {
            throw new NotImplementedException();
        }

        public void Serialize<T>(T obj, StreamWriter sw) {
            throw new NotImplementedException();
        }
    }
}
