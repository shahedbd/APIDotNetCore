using System;

namespace Application.Core
{
    public class BaseEntity
    {
        public long ID { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public string CreationUser { get; set; }
        public string LastUpdateUser { get; set; }
    }
}
