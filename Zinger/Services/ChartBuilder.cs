using System;
using System.Data;
using System.Linq;
using System.Windows.Forms.DataVisualization.Charting;
using Zinger.Models;

namespace Zinger.Services
{
    internal static class ChartBuilder
    {
        public static void Execute(DataTable data, ChartConfiguration config, Chart chart)
        {            
            chart.Series.Clear();

            foreach (var seriesGrp in data.AsEnumerable().GroupBy(row => row.Field<string>(config.SeriesColumn)))
            {
                var series = chart.Series.Add(seriesGrp.Key);
                series.ChartType = config.ChartType;

                foreach (DataRow valueRow in seriesGrp)
                {
                    var point = new DataPoint(series);
                    point.XValue = (config.ValueType.Equals("double")) ? 
                        valueRow.Field<double>(config.ValueColumn) : 
                        GetXValue(valueRow, config.ValueColumn, config.ValueType);
                    point.Label = valueRow.Field<string>(config.LabelColumn);
                    series.Points.Add(point);
                }
            }
        }

        private static double GetXValue(DataRow valueRow, string valueColumn, string valueType)
        {
            var lowerType = valueType.ToLower();

            return
                (lowerType.Equals("int")) ? Convert.ToDouble(valueRow.Field<int>(valueColumn) ):
                (lowerType.Equals("decimal")) ? Convert.ToDouble(valueRow.Field<decimal>(valueColumn)) :
                throw new Exception($"Unsupported value type: {valueType}");
        }
    }
}
