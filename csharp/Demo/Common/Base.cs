using System;

namespace Common
{
    /// <summary>
    ///     <para>
    ///         Provides a base class for models to inherit from that provides fields for Id, Created, Modified, and Deleted.
    ///     </para>
    ///     <para>
    ///         Note: Could also include CreatedBy, ModifiedBy and DeletedBy, but ignoring those for now since this demo doesn't have users implemented.
    ///         Id could also be changed to a bigint if needed.
    ///     </para>
    /// </summary>
    public abstract class Base
    {
        public int Id { get; set; }

        public DateTimeOffset Created { get; set; }

        public DateTimeOffset? Modified { get; set; }

        public DateTimeOffset? Deleted { get; set; }
    }
}
