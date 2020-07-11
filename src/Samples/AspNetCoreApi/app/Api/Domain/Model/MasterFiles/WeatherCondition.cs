using ApiTemplate.Api.Domain.Common;

namespace ApiTemplate.Api.Domain.Model.MasterFiles
{
    public class WeatherCondition : MasterFile
    {
        public string Condition { get; protected set; }
        public virtual WeatherType WeatherType { get; set; }
        protected WeatherCondition() { }

        public WeatherCondition(string condition, WeatherType weatherType)
        {
            Condition = condition;
            WeatherType = weatherType;
        }
    }
}