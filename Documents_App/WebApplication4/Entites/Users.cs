using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication4.Entites
{
    public class Users : BaseEntity
    {
        public Guid IdUser { get; set; } = new Guid();

        public String UserName { get; set; }

        public Guid IdTable { get; set; } = new Guid();

        public String TableName { get; set; }
    }
}
