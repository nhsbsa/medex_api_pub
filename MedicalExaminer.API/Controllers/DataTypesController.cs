using System;
using System.Collections.Generic;
using AutoMapper;
using MedicalExaminer.Common.Queries.MELogger;
using MedicalExaminer.Common.Services;
using MedicalExaminer.Models;
using MedicalExaminer.Models.Enums;
using Microsoft.AspNetCore.Mvc;

namespace MedicalExaminer.API.Controllers
{
    /// <summary>
    /// Data Type Controller required for providing lists of types.
    /// </summary>
    [ApiVersion("1.0")]
    [Route("/v{api-version:apiVersion}/data_types")]
    [ApiController]
    public class DataTypesController : BaseController
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DataTypesController"/> class.
        /// </summary>
        /// <param name="logger">Instance of ILogger</param>
        /// <param name="mapper">Instance of IMapper</param>
        public DataTypesController(IAsyncQueryHandler<CreateMELoggerQuery, LogMessageActionDefault> logger, IMapper mapper)
            : base(logger, mapper)
        {
        }

        /// <summary>
        /// Returns all Status Bar Results
        /// </summary>
        /// <returns>Dictionary of Status Bar Result</returns>
        [HttpGet("status_bar_result")]
        public ActionResult GetStatusBarResult()
        {
            return Ok(GetDictionary(typeof(StatusBarResult)));
        }

        /// <summary>
        /// Returns all Personal Effects
        /// </summary>
        /// <returns>Dictionary of Personal Effects</returns>
        [HttpGet("personal_effects")]
        public ActionResult GetPersonalEffects()
        {
            return Ok(GetDictionary(typeof(PersonalEffects)));
        }

        /// <summary>
        /// Returns all Any Implants
        /// </summary>
        /// <returns>Dictionary of Any Implants</returns>
        [HttpGet("any_implants")]
        public ActionResult GetAnyImplants()
        {
            return Ok(GetDictionary(typeof(AnyImplants)));
        }

        /// <summary>
        /// Returns all Analysis Entry Type types
        /// </summary>
        /// <returns>Dictionary of Analysis Entry Type</returns>
        [HttpGet("analysis_entry_type")]
        public ActionResult GetAnalysisEntryTypes()
        {
            return Ok(GetDictionary(typeof(AnalysisEntryType)));
        }

        /// <summary>
        /// Returns all Overall Circumstances Of Death.
        /// </summary>
        /// <returns>Dictionary of Overall Circumstances Of Death.</returns>
        [HttpGet("overall_circumstances_of_death")]
        public ActionResult GetOverallCircumstancesOfDeath()
        {
            return Ok(GetDictionary(typeof(OverallCircumstancesOfDeath)));
        }

        /// <summary>
        /// Returns all Overall Outcome Of Pre-Scrutiny.
        /// </summary>
        /// <returns>Dictionary of Overall Outcome Of Pre-Scrutiny.</returns>
        [HttpGet("overall_outcome_of_prescrutiny")]
        public ActionResult GetOverallOutcomeOfPreScrutiny()
        {
            return Ok(GetDictionary(typeof(OverallOutcomeOfPreScrutiny)));
        }

        /// <summary>
        /// Returns all Clinical Governance Review.
        /// </summary>
        /// <returns>Dictionary of Clinical Governance Review.</returns>
        [HttpGet("clinical_governance_review")]
        public ActionResult GetClinicalGovernanceReview()
        {
            return Ok(GetDictionary(typeof(ClinicalGovernanceReview)));
        }

        /// <summary>
        /// Returns all Coroner Statuses
        /// </summary>
        /// <returns>Dictionary of Coroner Statuses</returns>
        [HttpGet("coroner_status")]
        public ActionResult GetCoronerStatuses()
        {
            return Ok(GetDictionary(typeof(CoronerStatus)));
        }

        /// <summary>
        /// Returns all Examination Genders
        /// </summary>
        /// <returns>Dictionary of Examination Genders</returns>
        [HttpGet("examination_genders")]
        public ActionResult GetExaminationGenders()
        {
            return Ok(GetDictionary(typeof(ExaminationGender)));
        }

        /// <summary>
        /// Returns all location types
        /// </summary>
        /// <returns>List of values for location types</returns>
        [HttpGet("location_types")]
        public ActionResult GetLocationTypes()
        {
            return Ok(GetDictionary(typeof(LocationType)));
        }

        /// <summary>
        /// Returns all options for the present at death value for a bereaved .
        /// </summary>
        /// <returns>List of values for present at death</returns>
        [HttpGet("present_at_death")]
        public ActionResult GetPresentAtDeath()
        {
            return Ok(GetDictionary(typeof(PresentAtDeath)));
        }

        /// <summary>
        /// Returns all options for the QAP Discussion Outcome value.
        /// </summary>
        /// <returns>List of values for QAP Discussion Outcome</returns>
        [HttpGet("qap_discussion_outcome")]
        public ActionResult GetQapDiscussionOutcome()
        {
            return Ok(GetDictionary(typeof(QapDiscussionOutcome)));
        }

        /// <summary>
        /// Returns all options for the informed at death value for a bereaved .
        /// </summary>
        /// <returns>List of values for informed at death</returns>
        [HttpGet("informed_at_death")]
        public ActionResult GetInformedAtDeath()
        {
            return Ok(GetDictionary(typeof(InformedAtDeath)));
        }

        /// <summary>
        /// Returns all options for the Bereaved Discussion Outcome value.
        /// </summary>
        /// <returns>List of values for Bereaved Discussion Outcome</returns>
        [HttpGet("bereaved_discussion_outcome")]
        public ActionResult GetBereavedDiscussionOutcome()
        {
            return Ok(GetDictionary(typeof(BereavedDiscussionOutcome)));
        }

        /// <summary>
        /// Returns all the data types and the values for mode of disposal.
        /// </summary>
        /// <returns>Dictionary of Modes of disposal</returns>
        [HttpGet("mode_of_disposal")]
        public ActionResult GetModesOfDisposal()
        {
            return Ok(GetDictionary(typeof(ModeOfDisposal)));
        }

        /// <summary>
        /// List of user roles
        /// </summary>
        /// <returns>List of user roles</returns>
        [HttpGet("user_roles")]
        public ActionResult GetUserRoles()
        {
            return Ok(GetDictionary(typeof(UserRoles)));
        }

        /// <summary>
        /// Returns all Event Type
        /// </summary>
        /// <returns>Dictionary of Event Types</returns>
        [HttpGet("event_type")]
        public ActionResult GetEventTypes()
        {
            return Ok(GetDictionary(typeof(EventType)));
        }

        /// <summary>
        /// Returns all Case Statuses
        /// </summary>
        /// <returns>Dictionary of Case Statuses</returns>
        [HttpGet("case_statuses")]
        public ActionResult GetCaseStatuses()
        {
            return Ok(GetDictionary(typeof(CaseStatus)));
        }

        /// <summary>
        /// Returns all Cremation Form Statuses
        /// </summary>
        /// <returns>Dictionary of Creation Form Statuses</returns>
        [HttpGet("cremation_form_statuses")]
        public ActionResult GetCremationFormStatuses()
        {
            return Ok(GetDictionary(typeof(CremationFormStatus)));
        }

        /// <summary>
        /// Returns all GP Notified
        /// </summary>
        /// <returns>Dictionary of GP Notified</returns>
        [HttpGet("gp_notified_statuses")]
        public ActionResult GetGPNotifiedStatuses()
        {
            return Ok(GetDictionary(typeof(GPNotified)));
        }

        /// <summary>
        /// Returns all Case Outcome Summaries
        /// </summary>
        /// <returns>Dictionary of Case Outcome summary</returns>
        [HttpGet("case_outcome_summary_statuses")]
        public ActionResult GetCaseOutcomeSummaryStatuses()
        {
            return Ok(GetDictionary(typeof(CaseOutcomeSummary)));
        }

        /// <summary>
        /// Returns all Validation Error Codes
        /// </summary>
        /// <returns>Dictionary of Validation Error codes</returns>
        [HttpGet("system_validation_errors")]
        public ActionResult GetSystemValidationErrors()
        {
            return Ok(GetDictionary(typeof(SystemValidationErrors)));
        }

        /// <summary>
        /// Returns all Routes of Admission
        /// </summary>
        /// <returns>Dictionary of Route of Admission</returns>
        [HttpGet("route_of_admission")]
        public ActionResult GetRouteOfAdmission()
        {
            return Ok(GetDictionary(typeof(RouteOfAdmission)));
        }

        /// <summary>
        /// Returns all QAP Discussion Statuses
        /// </summary>
        /// <returns>Dictionary of QAP Discussion Statuses</returns>
        [HttpGet("qap_discussion_status")]
        public ActionResult GetQapDiscussionStatues()
        {
            return Ok(GetDictionary(typeof(QAPDiscussionStatus)));
        }

        /// <summary>
        /// Returns all Pre Scrutiny Statuses
        /// </summary>
        /// <returns>Dictionary of Pre Scrutiny Statuses</returns>
        [HttpGet("pre_scrutiny_status")]
        public ActionResult GetPreScrutinyStatus()
        {
            return Ok(GetDictionary(typeof(PreScrutinyStatus)));
        }

        /// <summary>
        /// Returns all Pre Scrutiny Statuses
        /// </summary>
        /// <returns>Dictionary of Pre Scrutiny Statuses</returns>
        [HttpGet("case_open_closed")]
        public ActionResult GetCaseOpenClosed()
        {
            return Ok(GetDictionary(typeof(OpenClosedCases)));
        }

        /// <summary>
        /// Get Dictionary.
        /// </summary>
        /// <param name="enumeratorType">Type.</param>
        /// <returns>Dictionary with enum types and values.</returns>
        private Dictionary<string, int> GetDictionary(Type enumeratorType)
        {
            var dic = new Dictionary<string, int>();

            var values = Enum.GetValues(enumeratorType);

            foreach (var value in values)
            {
                dic.Add(value.ToString(),(int)value);
            }

            return dic;
        }
    }
}