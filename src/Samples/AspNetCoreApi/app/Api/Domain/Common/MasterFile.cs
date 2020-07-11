namespace ApiTemplate.Api.Domain.Common
{
    public abstract class MasterFile : Entity
    {
        public virtual bool ActiveFlag { get; protected set; }

        public virtual void MakeActive()
        {
            ActiveFlag = true;
        }

        public virtual void MakeInactive()
        {
            ActiveFlag = false;
        }
    }
}