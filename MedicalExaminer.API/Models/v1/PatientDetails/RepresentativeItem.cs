﻿using System;
using MedicalExaminer.Models.Enums;

namespace MedicalExaminer.API.Models.v1.PatientDetails
{
    /// <summary>
    ///     Object for creating and updating patients representative
    /// </summary>
    public class RepresentativeItem
    {
        /// <summary>
        ///     Representatives full name
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        ///     Representatives relationship to the patient
        /// </summary>
        public string Relationship { get; set; }

        /// <summary>
        ///     Representatives phone number
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        ///     The appointment date
        /// </summary>
        public DateTime? AppointmentDate { get; set; }

        /// <summary>
        ///     the appointment time
        /// </summary>
        public TimeSpan? AppointmentTime { get; set; }

        /// <summary>
        /// notes for each representative
        /// </summary>
        public string Notes { get; set; }
    }
}