using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication4.Entites
{
    public class Documents : BaseEntity
    {
        public Guid DocumentId { get; set; } = new Guid();

        public String DocumentName { get; set; }

        public byte[] Contents { get; set; }

        public String DocumentIntroNumber { get; set; }

        public String DocumentExternNumber { get; set; }

        public DateTime DateIntro { get; set; }
    }
}
