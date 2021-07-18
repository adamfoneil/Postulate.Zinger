using System.Windows.Forms.DataVisualization.Charting;

namespace Zinger.Models
{
    public class ChartConfiguration
    {
        public SeriesChartType ChartType { get; set; }
        /// <summary>
        /// must be convertable to string
        /// </summary>
        public string SeriesColumn { get; set; }
        /// <summary>
        /// must be convertible to string
        /// </summary>
        public string LabelColumn { get; set; }
        /// <summary>
        /// must be convertable to ValueType
        /// </summary>
        public string ValueColumn { get; set; }

        public string ValueType { get; set; } = "double";
    }
}
