using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using CGT_Project.Algorithms;
using CGT_Project.Data_Structures;
using CGT_ProjectWPF.Data_Structures;

namespace CGT_ProjectWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private BackgroundWorker bgWorker = new BackgroundWorker();
        private int minSize;
        private int maxSize;
        private int graphsCount;

        public MainWindow()
        {
            InitializeComponent();
            InitWorker();
        }

        private void InitWorker()
        {
            bgWorker = new BackgroundWorker();
            bgWorker.WorkerReportsProgress = true;
            bgWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(AlgorithmFinished);
            bgWorker.ProgressChanged += new ProgressChangedEventHandler(ProgressChanged);
            bgWorker.DoWork += new DoWorkEventHandler(StartAlgorithm);
        }

        private void MinChanged(object sender, TextChangedEventArgs e)
        {
            if (Int32.TryParse((sender as TextBox).Text, out minSize))
            {
                (sender as TextBox).Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));
                e.Handled = true;
            }
            else
            {
                (sender as TextBox).Background = new SolidColorBrush(Color.FromRgb(255, 0, 0));
                e.Handled = false;
            }
        }

        private void MaxChanged(object sender, TextChangedEventArgs e)
        {
            if (Int32.TryParse((sender as TextBox).Text, out maxSize))
            {
                (sender as TextBox).Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));
                e.Handled = true;
            }
            else
            {
                (sender as TextBox).Background = new SolidColorBrush(Color.FromRgb(255, 0, 0));
                e.Handled = false;
            }
        }

        private void NumChanged(object sender, TextChangedEventArgs e)
        {
            if (Int32.TryParse((sender as TextBox).Text, out graphsCount))
            {
                (sender as TextBox).Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));
                e.Handled = true;
            }
            else
            {
                (sender as TextBox).Background = new SolidColorBrush(Color.FromRgb(255, 0, 0));
                e.Handled = false;
            }
        }

        private void StartAlg_Click(object sender, RoutedEventArgs e)
        {
            bgWorker.RunWorkerAsync(LoadingInfo);
            LoadingGrid.Visibility = Visibility.Visible;
            StartAlg.IsEnabled = false;
        }


        public void StartAlgorithm(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker workerSender = sender as BackgroundWorker;
            var generatedGraphs = Graph.GenerateGraphs(graphsCount, minSize, maxSize, 1);
            AnalyzeData Data = new AnalyzeData();
            double i = 0;
            Stopwatch stopwatch = new Stopwatch();

            foreach (Graph graph in generatedGraphs)
            {
                stopwatch.Start();
                GreedyIndependentSetAlgorithm.ColorGraph(graph);
                stopwatch.Stop();
                Data.addISA_ChromSum(graph.ChromaticSum);
                Data.addISA_Time(stopwatch.ElapsedMilliseconds);

                graph.ClearColors();
                stopwatch.Reset();

                stopwatch.Start();
                LargestFirstAlgorithm.Coloring(graph);
                stopwatch.Stop();
                Data.addLF_ChromSum(graph.ChromaticSum);
                Data.addLF_Time(stopwatch.ElapsedMilliseconds);

                i++;
                int progress = (int)(100 * i / generatedGraphs.Count);
                workerSender.ReportProgress(progress, Data);
            }
        }
        public void ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            LoadingBar.Value = e.ProgressPercentage;
            LoadingInfo.Text = "Loading ... (" + e.ProgressPercentage + "%)";
            setResultData((AnalyzeData)e.UserState);
        }
        public void AlgorithmFinished(object sender, RunWorkerCompletedEventArgs e)
        {
            LoadingInfo.Text = "Finished";
            StartAlg.IsEnabled = true;
        }

        private void setResultData(AnalyzeData data)
        {
            //Assining all caluculated values to the correponding UI element
            LF_CromaMin.Text =      "Minimal Chromatic number: "        + data.LF_MinChromaticSum.ToString();
            LF_CromaMax.Text =      "Maximal Chromatic number: "     + data.LF_MaxChromaticSum.ToString();
            LF_CromaAvg.Text =      "Average Chromatic number: "     + data.getLF_ChromAvg().ToString();
            LF_CromaTotal.Text =    "Sum of all Chromatic numbers: " + data.LF_TotalChromaticSum.ToString();
            LF_timeMin.Text =       "Minimal time execution: "       + data.LF_MinTime.ToString() + "ms";
            LF_timeMax.Text =       "Maximal time execution: "    + data.LF_MaxTime + "ms";
            LF_timeAvg.Text =       "Average time execution: "    + data.getLF_TimeAvg().ToString() + "ms";

            ISA_CromaMin.Text = "Minimal Chromatic number: " + data.ISA_MinChromaticSum.ToString();
            ISA_CromaMax.Text = "Maximal Chromatic number: " + data.ISA_MaxChromaticSum.ToString();
            ISA_CromaAvg.Text = "Average Chromatic number: " + data.getISA_ChromAvg().ToString();
            ISA_CromaTotal.Text = "Sum of all Chromatic numbers: " + data.ISA_TotalChromaticSum.ToString();
            ISA_timeMin.Text = "Minimal time execution: " + data.ISA_MinTime.ToString()+"ms";
            ISA_timeMax.Text = "Maximal time execution: " + data.ISA_MaxTime + "ms";
            ISA_timeAvg.Text = "Average time execution: " + data.getISA_TimeAvg().ToString() + "ms";

            //Checking the values and coloring witch elelment is better
            Brush NoneColor = Brushes.Gray;
            Brush BetterColor = Brushes.Green;
            Brush WorstColor = Brushes.Red;
            if (data.LF_MinChromaticSum == data.ISA_MinChromaticSum)
            {
                LF_CromaMin.Foreground = NoneColor;
                ISA_CromaMin.Foreground = NoneColor;
            }
            else if (data.LF_MinChromaticSum < data.ISA_MinChromaticSum)
            {
                LF_CromaMin.Foreground = BetterColor;
                ISA_CromaMin.Foreground = WorstColor;
            }
            else
            {
                LF_CromaMin.Foreground = WorstColor;
                ISA_CromaMin.Foreground = BetterColor;
            }

            if (data.LF_MaxChromaticSum == data.ISA_MaxChromaticSum)
            {
                LF_CromaMax.Foreground = NoneColor;
                ISA_CromaMax.Foreground = NoneColor;
            }
            else if (data.LF_MaxChromaticSum < data.ISA_MaxChromaticSum)
            {
                LF_CromaMax.Foreground = BetterColor;
                ISA_CromaMax.Foreground = WorstColor;
            }
            else
            {
                LF_CromaMax.Foreground = WorstColor;
                ISA_CromaMax.Foreground = BetterColor;
            }

            if (data.getLF_ChromAvg() == data.getISA_ChromAvg())
            {
                LF_CromaAvg.Foreground = NoneColor;
                ISA_CromaAvg.Foreground = NoneColor;
            }
            else if (data.getLF_ChromAvg() < data.getISA_ChromAvg())
            {
                LF_CromaAvg.Foreground = BetterColor;
                ISA_CromaAvg.Foreground = WorstColor;
            }
            else
            {
                LF_CromaAvg.Foreground = WorstColor;
                ISA_CromaAvg.Foreground = BetterColor;
            }

            if (data.LF_TotalChromaticSum == data.ISA_TotalChromaticSum)
            {
                LF_CromaTotal.Foreground = NoneColor;
                ISA_CromaTotal.Foreground = NoneColor;
            }
            else if (data.LF_TotalChromaticSum < data.ISA_TotalChromaticSum)
            {
                LF_CromaTotal.Foreground = BetterColor;
                ISA_CromaTotal.Foreground = WorstColor;
            }
            else
            {
                LF_CromaTotal.Foreground = WorstColor;
                ISA_CromaTotal.Foreground = BetterColor;
            }

            if (data.LF_MinTime == data.ISA_MinTime)
            {
                LF_timeMin.Foreground = NoneColor;
                ISA_timeMin.Foreground = NoneColor;
            }
            else if (data.LF_MinTime < data.ISA_MinTime)
            {
                LF_timeMin.Foreground = BetterColor;
                ISA_timeMin.Foreground = WorstColor;
            }
            else
            {
                LF_timeMin.Foreground = WorstColor;
                ISA_timeMin.Foreground = BetterColor;
            }
            
            if (data.LF_MaxTime == data.ISA_MaxTime)
            {
                LF_timeMax.Foreground = NoneColor;
                ISA_timeMax.Foreground = NoneColor;
            }
            else if (data.LF_MaxTime < data.ISA_MaxTime)
            {
                LF_timeMax.Foreground = BetterColor;
                ISA_timeMax.Foreground = WorstColor;
            }
            else
            {
                LF_timeMax.Foreground = WorstColor;
                ISA_timeMax.Foreground = BetterColor;
            }

            if (data.getLF_TimeAvg() == data.getISA_TimeAvg())
            {
                LF_timeAvg.Foreground = NoneColor;
                ISA_timeAvg.Foreground = NoneColor;
            }
            else if (data.getLF_TimeAvg() < data.getISA_TimeAvg())
            {
                LF_timeAvg.Foreground = BetterColor;
                ISA_timeAvg.Foreground = WorstColor;
            }
            else
            {
                LF_timeAvg.Foreground = WorstColor;
                ISA_timeAvg.Foreground = BetterColor;
            }
        }
    }
}
