using ApiTemplate.Api.Domain.Common;

namespace ApiTemplate.Api.Domain.Model.MasterFiles
{
    public class WeatherType : MasterFile
    {
        public string WeatherTypeName { get; protected set; }
        public bool RequiredFlag { get; protected set; }
        protected WeatherType() { }

        public WeatherType(string weatherTypeName, bool requiredFlag)
        {
            WeatherTypeName = weatherTypeName;
            RequiredFlag = requiredFlag;
        }
    }
}
