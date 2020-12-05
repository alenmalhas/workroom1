using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace Interview20201204.Models
{
    public class Shift : IComparable<Shift>
    {
        public string Employer { get; set; }
        public string Location { get; set; }
        public int start_time { get; set; }
        public int end_time { get; set; }
        public int index { get; set; }

        public int CompareTo([AllowNull] Shift other)
        {
            if (null == other)
                return 1;// this is greater then other, when other is null.
            else if (ReferenceEquals(this, other))
                return 0;// same object ref, equals to itself.

            var thisKey = this.GetSortKey();

            var otherKey = other.GetSortKey();

            var comparisionValue = thisKey.CompareTo(otherKey);


            Debug.WriteLine(
                $"This: {thisKey} this.GetHashCode: {this.GetHashCode()} index: {this.index}" + Environment.NewLine +
                $"Other: {otherKey} other.GetHashCode: {other.GetHashCode()} index: {other.index}" + Environment.NewLine +
                $"---=> ComparisionValue: {comparisionValue}" + Environment.NewLine
                );

            return comparisionValue;
        }

        protected virtual string GetSortKey()
        {
            return $"{Employer} {Location}";
        }
    }

}
