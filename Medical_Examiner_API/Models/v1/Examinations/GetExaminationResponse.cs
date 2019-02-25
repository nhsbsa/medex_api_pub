﻿using System;
using System.ComponentModel.DataAnnotations;
using Medical_Examiner_API.Enums;
using Newtonsoft.Json;

namespace Medical_Examiner_API.Models.V1.Examinations
{
    /// <inheritdoc />
    /// <summary>
    /// Response object for a single examination.
    /// </summary>
    public class GetExaminationResponse : ResponseBase
    {
        public string ExaminationId { get; set; }

        public string FullName { get; set; }

        public string NHSNumber { get; set; }

        public ExaminationGender Gender { get; set; }

        public string HouseNameNumber { get; set; }

        public string Street { get; set; }

        public string Town { get; set; }

        public string County { get; set; }

        public string Postcode { get; set; }

        public string Country { get; set; }

        public string LastOccupation { get; set; }

        public string OrganisationCareBeforeDeathLocationId { get; set; }

        public string DeathOccuredLocationId { get; set; }

        public ModeOfDisposal ModeOfDisposal { get; set; }

        public string FuneralDirectors { get; set; }

        public bool PersonalAffectsCollected { get; set; }

        public string PersonalAffectsDetails { get; set; }

        public bool JewelleryCollected { get; set; }

        public string JewelleryDetails { get; set; }

        public DateTime DateOfBirth { get; set; }

        public DateTimeOffset DateOfDeath { get; set; }

        public bool FaithPriority { get; set; }

        public bool ChildPriority { get; set; }

        public bool CoronerPriority { get; set; }

        public bool OtherPriority { get; set; }

        public string PriorityDetails { get; set; }

        public bool Completed { get; set; }

        public CoronerStatus CoronerStatus { get; set; }
    }
}