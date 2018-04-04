using System;

namespace Witcherius.BL.DataTransferObjects.Common
{
    public class BaseDto
    {
        public Guid Id { get; set; }

        public override bool Equals(object obj)
        {
            if (!(obj is BaseDto))
            {
                return false;
            }

            var other = (BaseDto) obj;
            return Id.Equals(other.Id);
        }

        protected bool Equals(BaseDto other)
        {
            return Id.Equals(other.Id);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
