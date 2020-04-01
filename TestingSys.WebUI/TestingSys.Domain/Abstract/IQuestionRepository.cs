using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingSys.Domain.Entities;

namespace TestingSys.Domain.Abstract
{
    public interface IQuestionRepository:IDisposable
    {
        IEnumerable<Question> _Questions { get; set; }
    }
}
