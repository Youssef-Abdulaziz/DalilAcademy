using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalilAcademy.Core.DTOs
{
    public record CreateEnrollmentDto(int StudentId, int CourseId);

    public record EnrollmentResponseDto(int Id, int StudentId, int CourseId, DateTime EnrolledAt);
}
