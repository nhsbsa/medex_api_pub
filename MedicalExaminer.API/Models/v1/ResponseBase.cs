﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace MedicalExaminer.API.Models.v1
{
    /// <summary>
    ///     Base View Model that supports passing errors to the view.
    /// </summary>
    public class ResponseBase
    {
        /// <summary>
        ///     Get the Errors.
        /// </summary>
        public IDictionary<string, ICollection<string>> Errors { get; } = new Dictionary<string, ICollection<string>>();

        /// <summary>
        /// Lookups.
        /// </summary>
        public IDictionary<string, IEnumerable<object>> Lookups { get; private set; }

        /// <summary>
        ///     Gets a value indicating whether this response is successful.
        /// </summary>
        public virtual bool Success => !Errors.Any();

        /// <summary>
        ///     Add the error to the view model.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="message">The message.</param>
        public void AddError(string key, string message)
        {
            // If the error dictionary doesn't already have a key, create it and the new list to store messages
            if (!Errors.TryGetValue(key, out var messages))
            {
                messages = new List<string>();

                Errors[key] = messages;
            }

            messages.Add(message);
        }

        /// <summary>
        /// Add a lookup to the response.
        /// </summary>
        /// <param name="key">The key for the lookup</param>
        /// <param name="lookup">A dictionary of lookups</param>
        /// <exception cref="ArgumentException"></exception>
        /// <returns>Response Base for chainging.</returns>
        public ResponseBase AddLookup(string key, IEnumerable<object> lookup)
        {
            if (lookup == null)
            {
                throw new ArgumentException("Lookup null");
            }

            if (Lookups == null)
            {
                Lookups = new Dictionary<string, IEnumerable<object>>();
            }

            if (Lookups.ContainsKey(key))
            {
                throw new ArgumentException("Key already added");
            }

            Lookups[key] = lookup;

            return this;
        }
    }
}