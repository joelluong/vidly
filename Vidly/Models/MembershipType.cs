using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vidly.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.Security.AccessControl;

    public class MembershipType
    {
        public byte Id { get; set; }
        public short SignUpFee { get; set; }

        public byte DurationInMonths { get; set; }
        public byte DiscountRate { get; set; }

        [Required]
        public string Name { get; set; }

        public static readonly byte Unknown = 0;

        public static readonly byte PayAsYouGo = 1;

    }
}

/*
 * These records should be consistent across different environments
 * (reference data)
 * so our development database, testing database, they all should have
 * the exact same membership types.
 *
 * We need to ensure this consistency, should not touch database at all
 * Should update through migration
 *
 * When it comes to deploying our application if this is the first deployment
 * we can get all these migrations from the beginning of the time to the last
 * migration and using a command in package manager console. We ask EF to generate
 * SQL script which would include all changes and then we run SQL script on production
 * database
 *
 * If not first deployment, meaning we already have a database we can find the last migration
 * we can find last migration run on that database and create new SQL from that migration to
 * the last one. Create new SQL script from that migration to the last one
 */