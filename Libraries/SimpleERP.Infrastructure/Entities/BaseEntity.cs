using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SimpleERP.Infrastructure.Entities
{
    public interface IEntity
    {
    }

    public abstract class BaseEntity<TKey> : IEntity
    {
        public virtual TKey Id { get; set; }
    }

    public interface IVersionEntity
    {
        byte[] RowVersion { get; set; }
        string LastModifier { get; set; }
    }

    public interface IReadEntity
    {
        bool Hidden { get; set; }
        bool Readonly { get; set; }
        bool Deleted { get; set; }
    }

    public abstract class ExtraEntity<TKey> : IEntity, IVersionEntity, IReadEntity
    {
        public virtual TKey Id { get; set; }
        public virtual bool Hidden { get; set; }
        public virtual bool Readonly { get; set; }
        public virtual bool Deleted { get; set; }
        [Timestamp]
        public virtual byte[] RowVersion { get; set; }
        public virtual string LastModifier { get; set; }
    }
}
