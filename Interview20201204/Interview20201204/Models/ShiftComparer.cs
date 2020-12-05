using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Interview20201204.Models
{
    public class ShiftComparer : IComparer<Shift>
    {
        private readonly Random _random = new Random();
        private readonly bool _preferSecondWhenFullMismatch;
        private readonly StringBuilder _comparisionLog = new StringBuilder();

        private readonly List<string> _companyPriorityList = new List<string>();
        private readonly List<string> _locationList = new List<string>();

        public ShiftComparer(bool? preferSecondWhenFullMismatch = null)
        {
            // we could use following instead Random: DateTime.Now.Ticks % 2 == 0 
            _preferSecondWhenFullMismatch = preferSecondWhenFullMismatch
                ?? _random.Next(2) == 0 ? true : false;
        }

        public string GetComparisionLog()
        {
            var log = _comparisionLog.ToString();
            _comparisionLog.Clear();
            return log;
        }

        public int Compare([AllowNull] Shift x, [AllowNull] Shift y)
        {
            return CompareGroupByCompanyLocationAnyMismatchWithMemory(x, y, _preferSecondWhenFullMismatch);
            //return CompareGroupByCompanyLocation(x, y, _preferSecondWhenFullMismatch);
        }

        /// <summary>
        /// This method does not care about employer or location if the shifts are not in a group, i.e: employer and location both mismatches.
        /// </summary>
        private int CompareGroupByCompanyLocationAnyMismatchWithMemory([AllowNull] Shift one, [AllowNull] Shift two, bool preferSecondWhenFullMismatch)
        {
            int comparisionValue;

            if (null == one)
            {
                comparisionValue = -1;
            }
            else if (null == two)
            {
                comparisionValue = 1;
            }
            else if (
                0 == one.Employer.CompareTo(two.Employer) &&
                0 == one.Location.CompareTo(two.Location)
                )
            {
                comparisionValue = 0;
            }
            else
            {
                bool oneIsGreaterThenTwo = IsOneGreaterThenTwo(
                    one,
                    two,
                    preferSecondWhenFullMismatch);

                comparisionValue = oneIsGreaterThenTwo ? 1 : -1;
            }

#if DEBUG
            var comparisionLogLine = Environment.NewLine +
                $"This: {JsonConvert.SerializeObject(one)}" + Environment.NewLine +
                $"Other: {JsonConvert.SerializeObject(two)}" + Environment.NewLine +
                $"---=> ComparisionValue: {comparisionValue} " + Environment.NewLine;

            Debug.WriteLine(comparisionLogLine);
            _comparisionLog.AppendLine(comparisionLogLine);
#endif

            return comparisionValue;
        }

        /// <summary>
        /// This method has a preferance to keep same employers or same location close to each other if the shifts are not in a group.
        /// </summary>
        /// <param name="preferSecondWhenFullMismatch">Prefer second shift if both employer and location do not match, else prefer first one.</param>
        private int CompareGroupByCompanyLocation([AllowNull] Shift one, [AllowNull] Shift two, bool preferSecondWhenFullMismatch)
        {
            int comparisionValue;

            if (null == one)
                comparisionValue = -1;
            else if (null == two)
                comparisionValue = 1;
            else if (
                0 == one.Employer.CompareTo(two.Employer) &&
                0 == one.Location.CompareTo(two.Location)
                )
                comparisionValue = 0;
            else if (
                0 == one.Employer.CompareTo(two.Employer) &&
                0 != one.Location.CompareTo(two.Location)
                )
                comparisionValue = one.Location.CompareTo(two.Location);
            else if (
                0 != one.Employer.CompareTo(two.Employer) &&
                0 == one.Location.CompareTo(two.Location)
                )
                comparisionValue = one.Employer.CompareTo(two.Employer);
            else
            {
                // Both employer and location are different: prefer based on the flag.
                bool oneIsGreaterThenTwo = IsOneGreaterThenTwo(
                    one,
                    two,
                    preferSecondWhenFullMismatch);

                comparisionValue = oneIsGreaterThenTwo ? 1 : -1;
            }

#if DEBUG
            var comparisionLogLine = Environment.NewLine +
                $"This: {JsonConvert.SerializeObject(one)}" + Environment.NewLine +
                $"Other: {JsonConvert.SerializeObject(two)}" + Environment.NewLine +
                $"---=> ComparisionValue: {comparisionValue} preferSecondWhenFullMismatch: {_preferSecondWhenFullMismatch}" + Environment.NewLine;

            Debug.WriteLine(comparisionLogLine);
            _comparisionLog.AppendLine(comparisionLogLine);
#endif

            return comparisionValue;
        }

        private bool IsOneGreaterThenTwo(Shift one, Shift two, bool preferSecondWhenFullMismatch)
        {
            AddToPriorityList(one, two, preferSecondWhenFullMismatch);

            bool oneIsGreaterThenTwo;

            if (preferSecondWhenFullMismatch)
            {   // Prefer two when mismatch
                oneIsGreaterThenTwo =
                     _companyPriorityList.IndexOf(two.Employer) < _companyPriorityList.IndexOf(one.Employer)
                    &&
                    _locationList.IndexOf(two.Location) < _locationList.IndexOf(one.Location);
            }
            else
            {   // Prefer one when mismatch
                oneIsGreaterThenTwo =
                     _companyPriorityList.IndexOf(one.Employer) < _companyPriorityList.IndexOf(two.Employer)
                    &&
                    _locationList.IndexOf(one.Location) < _locationList.IndexOf(two.Location);
            }

            return oneIsGreaterThenTwo;
        }

        private void AddToPriorityList(Shift one, Shift two, bool preferSecondWhenFullMismatch)
        {
            if (preferSecondWhenFullMismatch)
            {
                if (!_companyPriorityList.Contains(two.Employer))
                    _companyPriorityList.Add(two.Employer);
                if (!_companyPriorityList.Contains(one.Employer))
                    _companyPriorityList.Add(one.Employer);

                if (!_locationList.Contains(two.Location))
                    _locationList.Add(two.Location);
                if (!_locationList.Contains(one.Location))
                    _locationList.Add(one.Location);
            }
            else
            {
                if (!_companyPriorityList.Contains(one.Employer))
                    _companyPriorityList.Add(one.Employer);
                if (!_companyPriorityList.Contains(two.Employer))
                    _companyPriorityList.Add(two.Employer);

                if (!_locationList.Contains(one.Location))
                    _locationList.Add(one.Location);
                if (!_locationList.Contains(two.Location))
                    _locationList.Add(two.Location);
            }
        }
    }

}
