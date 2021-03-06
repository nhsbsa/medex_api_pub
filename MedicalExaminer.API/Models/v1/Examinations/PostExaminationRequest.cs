﻿using System;
using System.ComponentModel.DataAnnotations;
using MedicalExaminer.API.Attributes;
using MedicalExaminer.Models.Enums;

namespace MedicalExaminer.API.Models.v1.Examinations
{
    /// <summary>
    /// </summary>
    public class PostExaminationRequest
    {
        /// <summary>
        ///     Where the death occured
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = nameof(SystemValidationErrors.Required))]
        public string PlaceDeathOccured { get; set; }

        /// <summary>
        ///     Medical Examiner Office Responsible for dealing with the examination
        ///     377e5b2d-f858-4398-a51c-1892973b6537
        /// </summary>
        [ValidLocation]
        public string MedicalExaminerOfficeResponsible { get; set; }

        /// <summary>
        ///     Patients surname
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = nameof(SystemValidationErrors.Required))]
        [MinLength(1, ErrorMessage = nameof(SystemValidationErrors.MinimumLengthOf1))]
        [MaxLength(150, ErrorMessage = nameof(SystemValidationErrors.MaximumLength150))]
        public string Surname { get; set; }

        /// <summary>
        ///     Patients given names
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = nameof(SystemValidationErrors.Required))]
        [MinLength(1, ErrorMessage = nameof(SystemValidationErrors.MinimumLengthOf1))]
        [MaxLength(150, ErrorMessage = nameof(SystemValidationErrors.MaximumLength150))]
        public string GivenNames { get; set; }

        /// <summary>
        ///     Gender patient identifies as
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = nameof(SystemValidationErrors.Required))]
        public ExaminationGender? Gender { get; set; }

        /// <summary>
        ///     Comments regarding the patients gender identification
        /// </summary>
        [RequiredIfAttributesMatch(nameof(Gender), ExaminationGender.Other, ErrorMessage = nameof(SystemValidationErrors.Required))]
        public string GenderDetails { get; set; }

        /// <summary>
        ///     Patients NHS Number 943 476 5919
        /// </summary>
        [ValidNhsNumberNullAllowed]
        [PostUniqueNhsNumberAttribute]
        public string NhsNumber { get; set; }

        /// <summary>
        ///     Patients first hospital number
        /// </summary>
        public string HospitalNumber_1 { get; set; }

        /// <summary>
        ///     Patients second hospital number
        /// </summary>
        public string HospitalNumber_2 { get; set; }

        /// <summary>
        ///     Patients third hospital number
        /// </summary>
        public string HospitalNumber_3 { get; set; }

        /// <summary>
        ///     Patients date of birth
        /// </summary>
        [DateIsLessThanOrEqualToNullsAllowed(nameof(DateOfDeath))]
        public DateTime? DateOfBirth { get; set; }

        /// <summary>
        ///     Patients date of death
        /// </summary>
        public DateTime? DateOfDeath { get; set; }

        /// <summary>
        ///     Patients time of death
        /// </summary>
        public TimeSpan? TimeOfDeath { get; set; }
    }
}