#region Using declarations
using NinjaTrader.Gui;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Windows.Media;
using System.Xml.Serialization;
#endregion

namespace NinjaTrader.NinjaScript.Indicators
{
    public class VolmaDelta : Indicator
    {
        public const string GROUP_NAME_GENERAL = "1. General";
        public const string GROUP_NAME_VOLMA_DELTA = "2. Volma Delta";
        public const string GROUP_NAME_PLOTS = "Plots";

        private VOLMA _volma;
        private VOLMA _fastVolma;

        #region General Properties

        [NinjaScriptProperty]
        [Display(Name = "Version", Description = "Volma Delta version.", Order = 0, GroupName = GROUP_NAME_GENERAL)]
        [ReadOnly(true)]
        public string Version => "1.0.0";

        #endregion

        #region Volma Delta Properties

        [NinjaScriptProperty]
        [Display(Name = "Fast Period", GroupName = GROUP_NAME_VOLMA_DELTA, Order = 0)]
        public int FastPeriod { get; set; }

        [NinjaScriptProperty]
        [Display(Name = "Period", GroupName = GROUP_NAME_VOLMA_DELTA, Order = 1)]
        public int Period { get; set; }

        [NinjaScriptProperty]
        [Display(Name = "Override Plot Colors", GroupName = GROUP_NAME_VOLMA_DELTA, Order = 2)]
        public bool OverridePlotColors { get; set; }

        [NinjaScriptProperty]
        [Display(Name = "Custom Positive Color", GroupName = GROUP_NAME_VOLMA_DELTA, Order = 3)]
        [XmlIgnore]
        public Brush CustomPositiveColor { get; set; }

        [NinjaScriptProperty]
        [Display(Name = "Custom Negative Color", GroupName = GROUP_NAME_VOLMA_DELTA, Order = 4)]
        [XmlIgnore]
        public Brush CustomNegativeColor { get; set; }

        #endregion

        #region Plots Properties

        [Browsable(false)]
        [XmlIgnore]
        [Display(Name = "Volma Delta", GroupName = GROUP_NAME_PLOTS, Order = 0)]
        public Series<double> VolmaDeltaSeries
        {
            get { return Values[0]; }
        }

        #endregion

        protected override void OnStateChange()
        {
            if (State == State.SetDefaults)
            {
                Description = @"Indicator to display the delta between fast VOLMA and VOLMA.";
                Name = "_VolmaDelta";
                Calculate = Calculate.OnBarClose;
                IsOverlay = false;
                DisplayInDataBox = true;
                DrawOnPricePanel = true;
                DrawHorizontalGridLines = true;
                DrawVerticalGridLines = true;
                PaintPriceMarkers = true;
                ScaleJustification = NinjaTrader.Gui.Chart.ScaleJustification.Right;
                IsSuspendedWhileInactive = true;

                FastPeriod = 3;
                Period = 10;
                OverridePlotColors = false;
                CustomPositiveColor = Brushes.DarkGreen;
                CustomNegativeColor = Brushes.DarkRed;

                AddPlot(new Stroke(Brushes.DarkCyan, 2), PlotStyle.Bar, "Volma Delta");
            }
            else if (State == State.DataLoaded)
            {
                _fastVolma = VOLMA(FastPeriod);
                _volma = VOLMA(Period);
            }
        }

        protected override void OnBarUpdate()
        {
            if (CurrentBar < Math.Max(Period, FastPeriod))
            {
                VolmaDeltaSeries.Reset();
                return;
            }

            double volmaDiff = _fastVolma[0] - _volma[0];
            VolmaDeltaSeries[0] = volmaDiff;

            if (OverridePlotColors)
            {
                PlotBrushes[0][0] = (volmaDiff >= 0) ? CustomPositiveColor : CustomNegativeColor;
            }
        }
    }
}

#region NinjaScript generated code. Neither change nor remove.

namespace NinjaTrader.NinjaScript.Indicators
{
    public partial class Indicator : NinjaTrader.Gui.NinjaScript.IndicatorRenderBase
    {
        private VolmaDelta[] cacheVolmaDelta;
        public VolmaDelta VolmaDelta(int fastPeriod, int period, bool overridePlotColors, Brush customPositiveColor, Brush customNegativeColor)
        {
            return VolmaDelta(Input, fastPeriod, period, overridePlotColors, customPositiveColor, customNegativeColor);
        }

        public VolmaDelta VolmaDelta(ISeries<double> input, int fastPeriod, int period, bool overridePlotColors, Brush customPositiveColor, Brush customNegativeColor)
        {
            if (cacheVolmaDelta != null)
                for (int idx = 0; idx < cacheVolmaDelta.Length; idx++)
                    if (cacheVolmaDelta[idx] != null && cacheVolmaDelta[idx].FastPeriod == fastPeriod && cacheVolmaDelta[idx].Period == period && cacheVolmaDelta[idx].OverridePlotColors == overridePlotColors && cacheVolmaDelta[idx].CustomPositiveColor == customPositiveColor && cacheVolmaDelta[idx].CustomNegativeColor == customNegativeColor && cacheVolmaDelta[idx].EqualsInput(input))
                        return cacheVolmaDelta[idx];
            return CacheIndicator<VolmaDelta>(new VolmaDelta() { FastPeriod = fastPeriod, Period = period, OverridePlotColors = overridePlotColors, CustomPositiveColor = customPositiveColor, CustomNegativeColor = customNegativeColor }, input, ref cacheVolmaDelta);
        }
    }
}

namespace NinjaTrader.NinjaScript.MarketAnalyzerColumns
{
    public partial class MarketAnalyzerColumn : MarketAnalyzerColumnBase
    {
        public Indicators.VolmaDelta VolmaDelta(int fastPeriod, int period, bool overridePlotColors, Brush customPositiveColor, Brush customNegativeColor)
        {
            return indicator.VolmaDelta(Input, fastPeriod, period, overridePlotColors, customPositiveColor, customNegativeColor);
        }

        public Indicators.VolmaDelta VolmaDelta(ISeries<double> input, int fastPeriod, int period, bool overridePlotColors, Brush customPositiveColor, Brush customNegativeColor)
        {
            return indicator.VolmaDelta(input, fastPeriod, period, overridePlotColors, customPositiveColor, customNegativeColor);
        }
    }
}

namespace NinjaTrader.NinjaScript.Strategies
{
    public partial class Strategy : NinjaTrader.Gui.NinjaScript.StrategyRenderBase
    {
        public Indicators.VolmaDelta VolmaDelta(int fastPeriod, int period, bool overridePlotColors, Brush customPositiveColor, Brush customNegativeColor)
        {
            return indicator.VolmaDelta(Input, fastPeriod, period, overridePlotColors, customPositiveColor, customNegativeColor);
        }

        public Indicators.VolmaDelta VolmaDelta(ISeries<double> input, int fastPeriod, int period, bool overridePlotColors, Brush customPositiveColor, Brush customNegativeColor)
        {
            return indicator.VolmaDelta(input, fastPeriod, period, overridePlotColors, customPositiveColor, customNegativeColor);
        }
    }
}

#endregion
