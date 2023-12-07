using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pokemonStorageSystem.util {
    public enum FileFormat {
        Xml,
        Json,
        Txt
    }
    public class Serializer {

        // backing fields
        private Type _type;
        private ISerializer _serializer;
        private FileFormat _fileFormat;
        private string _filePath;
        private string _fileDirectory;
        private string _fileName;
        private string _fileExtension;

        // public properties
        public string FilePath { get { return _filePath; } }
        public string FileDirectory {
            get { return _fileDirectory; }
            set {
                _fileDirectory = value;

                _filePath = $@"{value}\{_fileName}.{_fileExtension}";
            }
        }
        public string FileName {
            get { return _fileName; }
            set {
                _fileName = value;

                _filePath = $@"{_fileDirectory}\{value}.{_fileExtension}";
            }
        }
        public Type Type {
            get { return _type; }
            set {
                _type = value;

                if (value.IsGenericType && value.GetGenericTypeDefinition() == typeof(List<>)) {
                    _fileName = $"{value.GetGenericArguments()[0].Name}List";
                } else {
                    _fileName = value.Name;
                }

            }
        }
        public FileFormat FileFormat {
            get { return _fileFormat; }
            set {
                _fileFormat = value;
                _fileExtension = value.ToString().ToLower();
                _fileDirectory = $@"files\{_fileExtension}";

                _serializer = value switch {
                    FileFormat.Xml => new XmlSerializer(),
                    FileFormat.Json => new JsonSerializer(),
                    _ => new TextSerializer(),
                };

                _filePath = $@"{_fileDirectory}\{_fileName}.{_fileExtension}";
            }
        }

        public Serializer(Type type, FileFormat fileFormat) {
            Type = type;
            FileFormat = fileFormat;
        }

        public void Serialize(object data) {
            if (data != null && data.GetType() == Type) {
                Directory.CreateDirectory(_fileDirectory);

                using StreamWriter sw = new(FilePath);
                _serializer.Serialize(data, sw);
            }
        }

        public T Deserialize<T>(T data) where T : new() {
            T obj = new();

            if (data != null && data.GetType() == Type) {
                using StreamReader sr = new(FilePath);
                obj = _serializer.Deserialize<T>(sr);
            }

            return obj;
        }
    }
}
