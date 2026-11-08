using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalilAcademy.Core.Exceptions
{
    
        public class NotFoundException(string message) : Exception(message);
        public class ConflictException(string message) : Exception(message);

}
