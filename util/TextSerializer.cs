using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace pokemonStorageSystem.util {
    public class TextSerializer : ISerializer {
        public T Deserialize<T>(StreamReader sr) where T : new() {
            T obj = new();
            Type type = obj.GetType();
            ConstructorInfo constructor;
            ParameterInfo[] parameterTypes;
            object?[] parameters;
            string? line;
            string[] data;
            bool once = true;

            if (obj is IList) {
                type = type.GenericTypeArguments[0];
            }

            constructor = type.GetConstructors()[1];
            parameterTypes = constructor.GetParameters();

            while ((line = sr.ReadLine()) is not null && once) {
                data = line.Split(';');
                parameters = new object[data.Length];

                for (int i = 0; i < data.Length; i++) {
                    if (data[i] != string.Empty) {
                        parameters[i] = TypeDescriptor.GetConverter(parameterTypes[i].ParameterType).ConvertFromString(data[i]);
                    }
                }

                if (obj is IList list) {
                    list.Add(constructor.Invoke(parameters));
                } else {
                    obj = (T) constructor.Invoke(parameters);
                    once = false;
                }
            }

            return obj;
        }

        public void Serialize<T>(T obj, StreamWriter sw) {
            if (obj is IList list) {
                foreach (var item in list) {
                    sw.WriteLine(item);
                }
            } else {
                sw.WriteLine(obj);
            }
        }
    }
}
