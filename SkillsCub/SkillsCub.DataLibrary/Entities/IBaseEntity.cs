using System;
using System.Collections.Generic;
using System.Text;

namespace SkillsCub.DataLibrary.Entities.Implementation
{
    /// <summary>
    /// The base entity.
    /// </summary>
    public interface IBaseEntity
    {
        /// <summary>
        /// Gets or sets id.
        /// </summary>
        Guid Id { get; set; }
    }
}
