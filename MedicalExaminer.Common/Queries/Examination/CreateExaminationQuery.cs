﻿namespace MedicalExaminer.Common.Queries.Examination
{
    public class CreateExaminationQuery : IQuery<Models.Examination>
    {
        public Models.Examination Examination { get; }
        public CreateExaminationQuery(Models.Examination examination)
        {
            Examination = examination;
        }
    }
}
