using System;

namespace Witcherius.Infrastructure
{
    public interface IEntity
    {
        /// <summary>
        /// Unique id of the entity.
        /// </summary>
        Guid Id { get; set; }
    }
}
