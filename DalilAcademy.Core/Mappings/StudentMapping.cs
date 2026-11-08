using DalilAcademy.Core.DTOs;
using DalilAcademy.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalilAcademy.Core.Mappings
{
    public static class StudentMappings
    {
        
        public static StudentResponseDto ToDto(this Student student)
        {
            return new StudentResponseDto(student.Id, student.FullName, student.Email);
        }

        
        public static Student ToEntity(this CreateStudentDto dto)
        {
            return new Student
            {
                FullName = dto.FullName,
                Email = dto.Email
            };
        }

        
        public static Student ToEntity(this UpdateStudentDto dto, int id)
        {
            return new Student
            {
                Id = id,
                FullName = dto.FullName,
                Email = dto.Email
            };
        }
    }
}
