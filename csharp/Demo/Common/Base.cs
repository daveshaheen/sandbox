using System;

namespace Common
{
    /// <summary>
    ///     <para>
    ///         Provides a base class for database table models to inherit from.
    ///     </para>
    ///     <para>
    ///         If a class inherits from Base then Id, Created, Modified, and Deleted will be available.
    ///     </para>
    ///     <para>
    ///         Note: Could also include CreatedBy, ModifiedBy and DeletedBy, but ignoring for now since this demo doesn't have users implemented.
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
