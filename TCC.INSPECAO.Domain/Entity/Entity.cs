using System;
using Newtonsoft.Json;

namespace TCC.INSPECAO.Domain.Entity
{
    public abstract class Entity : IEquatable<Entity>
    {
        [JsonIgnore]
        public Guid Id { get; private set; }

        public Entity()
        {
            Id = Guid.NewGuid();
        }

        public bool Equals(Entity other)
        {
            return Id == other.Id;
        }
    }
}