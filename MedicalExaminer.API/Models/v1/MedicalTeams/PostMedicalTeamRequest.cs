﻿using System.ComponentModel.DataAnnotations;
using MedicalExaminer.API.Attributes;
using MedicalExaminer.API.Models.v1.Users;
using MedicalExaminer.Models;

namespace MedicalExaminer.API.Models.v1.Examinations
{
    public class PostMedicalTeamRequest
    {
        /// <summary>
        ///     Consultant primarily responsible for care of patient
        /// </summary>
        [Required]
        public ClinicalProfessional ConsultantResponsible { get; set; }


        /// <summary>
        ///     Other consultants involved in care of the patient
        /// </summary>
        [Required]
        public ClinicalProfessional[] ConsultantsOther { get; set; }

        /// <summary>
        ///     Consultant primarily responsible for care
        /// </summary>
        [Required]
        public ClinicalProfessional GeneralPractitioner { get; set; }

        /// <summary>
        ///     Clinician responsible for certification
        /// </summary>
        [Required]
        public ClinicalProfessional Qap { get; set; }

        /// <summary>
        ///     Nursing information
        /// </summary>
        public string NursingTeamInformation { get; set; }

        /// <summary>
        ///     Medical Examiner
        /// </summary>
        [ValidMedicalExaminer]
        public UserItem MedicalExaminer { get; set; }

        /// <summary>
        ///     Medical Examiner Officer
        /// </summary>
        [ValidMedicalExaminerOfficer]
        public UserItem MedicalExaminerOfficer { get; set; }
    }
}