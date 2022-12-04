using System;

namespace WitnessReports.Model.HttpException
{
    public class CustomNotFoundException : Exception
    {
        public CustomNotFoundException(string exception) : base(exception)
        {
        }
    }
}
