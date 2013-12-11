namespace Specify.Samples.CommandProcessing
{
    public class ValidatingFailure
    {
        /// <summary>
        /// Creates a new validation failure.
        /// </summary>
        public ValidatingFailure(string propertyName, string error)
            : this(propertyName, error, null)
        {
        }

        /// <summary>
        /// Creates a new ValidatingFailure.
        /// </summary>
        public ValidatingFailure(string propertyName, string error, object attemptedValue)
        {
            PropertyName = propertyName;
            ErrorMessage = error;
            AttemptedValue = attemptedValue;
        }

        /// <summary>
        /// The name of the property.
        /// </summary>
        public string PropertyName { get; private set; }

        /// <summary>
        /// The error message
        /// </summary>
        public string ErrorMessage { get; private set; }

        /// <summary>
        /// The property value that caused the failure.
        /// </summary>
        public object AttemptedValue { get; private set; }

        /// <summary>
        /// Custom state associated with the failure.
        /// </summary>
        public object CustomState { get; set; }

        /// <summary>
        /// Creates a textual representation of the failure.
        /// </summary>
        public override string ToString()
        {
            string message = ErrorMessage;
            if (!string.IsNullOrEmpty(PropertyName))
                message = string.Format("{0} Property Name: {1}", message, PropertyName);
            if (AttemptedValue != null)
            {
                if (!string.IsNullOrEmpty(AttemptedValue.ToString()))
                    message = string.Format("{0} Attempted Value: {1}", message, AttemptedValue.ToString());
            }
            return message;
            //return ErrorMessage;
        }
    }
}