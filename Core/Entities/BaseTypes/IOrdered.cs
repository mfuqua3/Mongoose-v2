

using Microsoft.EntityFrameworkCore;

namespace Mongoose.Core.Entities.BaseTypes
{
    public interface IOrdered
    {
        public int Number { get; set; }
    }
}