using pokemonStorageSystem.model;
using pokemonStorageSystem.util;
using System.Collections;
using System.ComponentModel;

namespace pokemonStorageSystem {
    public class Program {
        static void Main(string[] args) {
            var pokemons = new List<Pokemon>() {
                new() {
                    Id = 1,
                    Species = "bulbasaur",
                    PrimaryType = PokemonType.Grass,
                    SecondaryType = PokemonType.Poison
                },
                new() {
                    Id = 4,
                    Species = "squirtle",
                    PrimaryType = PokemonType.Water
                },
                new() {
                    Id = 7,
                    Species = "charmander",
                    PrimaryType = PokemonType.Fire
                }
            };

            var serializer = new Serializer(typeof(List<Pokemon>), FileFormat.Txt);

            serializer.Serialize(pokemons);

            pokemons.Clear();

            pokemons = serializer.Deserialize(pokemons);
            pokemons.ForEach(p => Console.WriteLine(p.Species));
            Console.WriteLine();

            Pokemon p = new() {
                Id = 25,
                Species = "pikachu",
                PrimaryType = PokemonType.Electric
            };

            serializer.Type = typeof(Pokemon);
            serializer.FileDirectory = @"files\test";
            serializer.FileName = "pikachu";
            serializer.Serialize(p);

            Console.WriteLine(serializer.Deserialize(p).Species);
        }
    }
}
