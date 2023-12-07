using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace pokemonStorageSystem.model {
    public enum PokemonType {
        Normal,
        Fire,
        Water,
        Grass,
        Electric,
        Ice,
        Fighting,
        Poison,
        Ground,
        Flying,
        Psychic,
        Bug,
        Rock,
        Ghost,
        Dragon,
        Dark,
        Steel,
        Fairy
    }

    public class Pokemon {
        public int Id { get; set; }
        public string? Species { get; set; }
        public string? Nickname { get; set; }
        public PokemonType PrimaryType { get; set; }
        public PokemonType? SecondaryType { get; set; }
        public long? TrainerId { get; set; }

        public Pokemon() { }

        public Pokemon(int id, string species, PokemonType primaryType, PokemonType? secondaryType = null, string? nickname = null, long? trainerId = null) {
            Id = id;
            Species = species;
            Nickname = nickname;
            PrimaryType = primaryType;
            SecondaryType = secondaryType;
            TrainerId = trainerId;
        }

        public override string ToString() {
            return $"{Id};{Species};{PrimaryType};{SecondaryType};{Nickname};{TrainerId}";
        }
    }
}
