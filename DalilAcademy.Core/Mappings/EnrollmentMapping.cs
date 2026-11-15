using DalilAcademy.Core.DTOs;
using DalilAcademy.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalilAcademy.Core.Mappings
{
    public static class EnrollmentMappings
    {
        public static EnrollmentResponseDto ToDto(this Enrollment e) =>
            new(e.Id, e.StudentId, e.CourseId, e.EnrolledAt, e.Status.ToString());

        public static Enrollment ToEntity(this CreateEnrollmentDto dto) =>
            new() { StudentId = dto.StudentId, CourseId = dto.CourseId };
    }
}
