using ShortenerUrl.Web.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShortenerUrl.Web.Infrastructure.Identity
{
    public class PafUser
    {
        public OkResponse OkResponse { get; set; }
        public string UserName { get; set; }
        public string Role { get; set; }
        public string Token { get; set; }
        public Guid Id { get; set; }
        public DateTime Expire { get; set; }

    }
}
