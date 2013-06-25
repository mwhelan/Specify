using System.Collections.Generic;
using System.Text;

namespace Specify.Tests.Example.CommandProcessing
{
    public class ExecutionResult
    {
        readonly object _executedObject;

        public ExecutionResult(object executedObject)
        {
            _executedObject = executedObject;
            Errors = new List<ValidatingFailure>();
        }

        public bool IsSuccessful
        {
            get { return Errors.Count == 0; }
        }

        public List<ValidatingFailure> Errors { get; private set; }

        public void AddErrors(IEnumerable<ValidatingFailure> errors)
        {
            foreach (var error in errors)
            {
                Errors.Add(error);
            }
        }

        public string ErrorMessages
        {
            get
            {
                var sb = new StringBuilder();
                foreach (var error in Errors)
                {
                    sb.AppendLine(error.ToString());
                }
                return sb.ToString();
            }
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine(string.Format("Type: {0}", _executedObject.GetType().Name));
            sb.AppendLine(string.Format("Execution Status: {0}", IsSuccessful ? "Success" : "Failure"));
            if (!IsSuccessful)
            {
                sb.Append(ErrorMessages);
            }
            return sb.ToString();
        }
    }
}