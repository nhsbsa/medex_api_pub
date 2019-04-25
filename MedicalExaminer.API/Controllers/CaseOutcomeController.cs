﻿using System;
using System.Threading.Tasks;
using AutoMapper;
using MedicalExaminer.API.Models.v1.CaseOutcome;
using MedicalExaminer.Common.Loggers;
using MedicalExaminer.Common.Queries.CaseOutcome;
using MedicalExaminer.Common.Queries.Examination;
using MedicalExaminer.Common.Queries.User;
using MedicalExaminer.Common.Services;
using MedicalExaminer.Models;
using Microsoft.AspNetCore.Mvc;

namespace MedicalExaminer.API.Controllers
{
    [ApiVersion("1.0")]
    [Route("/v{api-version:apiVersion}/examinations/{examinationId}")]
    [ApiController]
    public class CaseOutcomeController : AuthenticatedBaseController
    {
        IAsyncQueryHandler<CoronerReferralQuery, string> _coronerReferralService;
        IAsyncQueryHandler<CloseCaseQuery, string> _closeCaseService;

        public CaseOutcomeController(
            IMELogger logger,
            IMapper mapper,
            IAsyncQueryHandler<CoronerReferralQuery, string> coronerReferralService,
            IAsyncQueryHandler<CloseCaseQuery, string> closeCaseService,
            IAsyncQueryHandler<UserRetrievalByEmailQuery, MeUser> usersRetrievalByEmailService)
            : base(logger, mapper, usersRetrievalByEmailService)
        {
            _coronerReferralService = coronerReferralService;
            _closeCaseService = closeCaseService;
        }

        [HttpPut]
        [Route("confirmation_of_scrutiny")]
        public ActionResult<PutConfirmationOfScrutinyResponse> PutConfirmationOfScrutiny()
        {
            // TODO:  Implement
            return Ok(new PutConfirmationOfScrutinyResponse()
            {
                ScrutinyConfirmedOn = DateTime.Now
            });
        }

        [HttpPut]
        [Route("coroner_referral")]
        public async Task<ActionResult> PutCoronerReferral(string examinationId)
        {
            if (string.IsNullOrEmpty(examinationId))
            {
                return new BadRequestObjectResult(nameof(examinationId));
            }

            var user = await CurrentUser();

            var result = await _coronerReferralService.Handle(new CoronerReferralQuery(examinationId, user));

            return Ok();
        }

        [HttpPut]
        [Route("outstanding_case_items")]
        public ActionResult PutOutstandingCaseItems([FromBody] PutOutstandingCaseItemsRequest request)
        {
            // TODO:  Implement

            //  get examination to update

            //  map the putoutstandingcaseitemsrequest onto the examinationtoUpdate

            //  save the examination
            return Ok();
        }

        [HttpPut]
        [Route("close_case")]
        public async Task<ActionResult> PutCloseCase(string examinationId)
        {
            if (string.IsNullOrEmpty(examinationId))
            {
                return new BadRequestObjectResult(nameof(examinationId));
            }

            Guid examinationGuid;
            if (!Guid.TryParse(examinationId, out examinationGuid))
            {
                return new BadRequestObjectResult(nameof(examinationId));
            }

            var user = await CurrentUser();

            var result = await _closeCaseService.Handle(new CloseCaseQuery(examinationId, user));

            return Ok();
        }

        [HttpGet]
        [Route("case_outcome")]
        public ActionResult<GetCaseOutcomeResponse> GetCaseOutcome()
        {
            // TODO:  Implement
            return Ok(new GetCaseOutcomeResponse());
        }
    }
}
