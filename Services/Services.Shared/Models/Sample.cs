using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Shared.Models
{
    public class Sample
    {
        public int DeviceId { get; set; }
        public DateTime SampleDate { get; set; }
        public List<SampleUnit> Samples { get; set; }
        public Sample()
        {
            Samples = new();
        }
    }

    public class SampleUnit
    {
        public string DisplayName { get; set; }
        public string Value { get; set; }   
        public string Units { get; set; }

    }

    public class TemperatureSampleUnit: SampleUnit
    {
        public TemperatureSampleUnit()
        {
            DisplayName = "Temperature";
            Units = "c";

        }
    }

    public class HumiditySampleUnit: SampleUnit
    {
        public HumiditySampleUnit()
        {
            DisplayName = "Humidity";
            Units = "%";
        }
    }

}
