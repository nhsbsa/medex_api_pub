﻿using MedicalExaminer.Models.Enums;
using System;

namespace MedicalExaminer.Models
{
    public interface IEvent
    {
        EventType EventType { get; }

        string EventId { get; set; }

        bool IsFinal { get; }

        string UserId { get; set; }

        string UserFullName { get; set; }

        string UsersRole { get; set; }

        DateTime? Created { get; set; }
    }
}
