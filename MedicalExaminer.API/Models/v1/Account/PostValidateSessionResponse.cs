﻿using System.Collections.Generic;
using MedicalExaminer.Common.Authorization;
using MedicalExaminer.Models.Enums;

namespace MedicalExaminer.API.Models.v1.Account
{
    /// <summary>
    ///     Post Validate Session Response
    /// </summary>
    public class PostValidateSessionResponse : ResponseBase
    {
        /// <summary>
        ///     User Id
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        ///     First Name
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        ///     Last Name
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        ///     Email Address
        /// </summary>
        public string EmailAddress { get; set; }

        /// <summary>
        /// Current user's GMC Number.
        /// </summary>
        public string GmcNumber { get; set; }

        /// <summary>
        /// Role.
        /// </summary>
        /// <remarks>All roles the user has, for display purposes</remarks>
        public IEnumerable<UserRoles> Role { get; set; }

        /// <summary>
        /// Permissions.
        /// </summary>
        public IDictionary<Permission, bool> Permissions { get; set; }
    }
}