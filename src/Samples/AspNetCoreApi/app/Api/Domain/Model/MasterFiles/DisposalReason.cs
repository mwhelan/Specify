using ApiTemplate.Api.Domain.Common;

namespace ApiTemplate.Api.Domain.Model.MasterFiles
{
    public class DisposalReason : MasterFile
    {
        public string Reason { get; protected set; }

        public string DisposalReasonDescription { get; protected set; }

        protected DisposalReason() { }

        public DisposalReason(string reason, string disposalReasonDescription)
        {
            Reason = reason;
            DisposalReasonDescription = disposalReasonDescription;
        }
    }
}