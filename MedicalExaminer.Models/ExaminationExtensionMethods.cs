﻿using System.Linq;
using MedicalExaminer.Models.Enums;

namespace MedicalExaminer.Models
{
    public static class ExaminationExtensionMethods
    {
        public static Examination MyUsersView(this Examination examination, MeUser myUser)
        {
            examination.CaseBreakdown = GetUserEvents(examination.CaseBreakdown, myUser);
            return examination;
        }

        public static Examination AddEvent(this Examination examination, IEvent theEvent)
        {
           switch (theEvent.EventType)
           {
                case EventType.Other:
                    var otherEventContainer = examination.CaseBreakdown.OtherEvents;

                    if (otherEventContainer == null)
                    {
                        otherEventContainer = new OtherEventContainer();
                    }

                    otherEventContainer.Add((OtherEvent)theEvent);
                    break;
           }

            examination = UpdateCaseStatus(examination);
            return examination;
        }

        private static Examination UpdateCaseStatus(Examination examination)
        {
            return examination;
        }

        private static CaseBreakDown GetUserEvents(CaseBreakDown caseBreakDown, MeUser myUser)
        {
            if(caseBreakDown == null)
            {
                return null;
            }

            caseBreakDown.OtherEvents = GetEvents<OtherEvent>(caseBreakDown.OtherEvents, myUser);
            return caseBreakDown;
        }

        private static BaseEventContainter<T> GetEvents<T>(IEventContainer<IEvent> otherEvents, MeUser myUser) where T : IEvent
        {
            var usersDrafts = otherEvents.Drafts.Where(draft => draft.UserId == myUser.UserId);
            otherEvents.Drafts = usersDrafts.ToList();
            return (BaseEventContainter<T>)otherEvents;
        }
    }
}
