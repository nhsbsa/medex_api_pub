﻿
using AutoMapper;
using MedicalExaminer.API.Models.v1.PatientDetails;
using MedicalExaminer.Models;
using MedicalExaminer.Models.Enums;

namespace MedicalExaminer.API.Extensions.Data
{
    /// <summary>
    /// Patient Details Profile.
    /// </summary>
    public class PatientDetailsProfile : Profile
    {
        /// <summary>
        /// Initialise a new instance of <see cref="PatientDetailsProfile"/>.
        /// </summary>
        public PatientDetailsProfile()
        {
            CreateMap<PutPatientDetailsRequest, PatientDetails>();
            CreateMap<PatientDetails, Examination>()
                .ForMember(examination => examination.ExaminationId, opt => opt.Ignore())
                .ForMember(examination => examination.CaseCompleted, opt => opt.Ignore())
                .ForMember(examination => examination.MedicalExaminerOfficeResponsibleName, opt => opt.Ignore())
                .ForMember(examination => examination.UrgencySort, opt => opt.Ignore())
                .ForMember(examination => examination.LastAdmission, opt => opt.Ignore())
                .ForMember(examination => examination.MedicalTeam, opt => opt.Ignore())
                .ForMember(examination => examination.CaseBreakdown, opt => opt.Ignore())
                .ForMember(examination => examination.HaveUnknownBasicDetails, opt => opt.Ignore())
                .ForMember(examination => examination.AdmissionNotesHaveBeenAdded, opt => opt.Ignore())
                .ForMember(examination => examination.ReadyForMEScrutiny, opt => opt.Ignore())
                .ForMember(examination => examination.Unassigned, opt => opt.Ignore())
                .ForMember(examination => examination.HaveBeenScrutinisedByME, opt => opt.Ignore())
                .ForMember(examination => examination.PendingAdditionalDetails, opt => opt.Ignore())
                .ForMember(examination => examination.PendingAdmissionNotes, opt => opt.Ignore())
                .ForMember(examination => examination.PendingDiscussionWithQAP, opt => opt.Ignore())
                .ForMember(examination => examination.PendingDiscussionWithRepresentative, opt => opt.Ignore())
                .ForMember(examination => examination.PendingScrutinyNotes, opt => opt.Ignore())
                .ForMember(examination => examination.HaveFinalCaseOutcomesOutstanding, opt => opt.Ignore())
                .ForMember(examination => examination.NationalLocationId, opt => opt.Ignore())
                .ForMember(examination => examination.RegionLocationId, opt => opt.Ignore())
                .ForMember(examination => examination.TrustLocationId, opt => opt.Ignore())
                .ForMember(examination => examination.SiteLocationId, opt => opt.Ignore())
                .ForMember(examination => examination.LastModifiedBy, opt => opt.Ignore())
                .ForMember(examination => examination.ModifiedAt, opt => opt.Ignore())
                .ForMember(examination => examination.CreatedAt, opt => opt.Ignore())
                .ForMember(examination => examination.CreatedBy, opt => opt.Ignore())
                .ForMember(examination => examination.DeletedAt, opt => opt.Ignore())
                .ForMember(examination => examination.ConfirmationOfScrutinyCompletedAt, opt => opt.Ignore())
                .ForMember(examination => examination.ConfirmationOfScrutinyCompletedBy, opt => opt.Ignore())
                .ForMember(examination => examination.CoronerReferralSent, opt => opt.Ignore())
                .ForMember(examination => examination.ScrutinyConfirmed, opt => opt.Ignore())
                .ForMember(examination => examination.OutstandingCaseItemsCompleted, opt => opt.Ignore())
                .ForMember(examination => examination.CaseOutcome, opt => opt.Ignore())
                .ForMember(examination => examination.DateCaseClosed, opt => opt.Ignore())
                .ForMember(examination => examination.VoidedDate, opt => opt.Ignore())
                .ForMember(examination => examination.VoidReason, opt => opt.Ignore())
                .ForMember(examination => examination.VoidUserId, opt => opt.Ignore())
                .ForMember(examination => examination.IsVoid, opt => opt.Ignore())
                .ForMember(examination => examination.Version, opt => opt.Ignore());
        }
    }
}