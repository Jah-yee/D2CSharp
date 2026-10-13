using System;
using System.Globalization;
using System.Sysutils;
using System.Types;
using static System.SystemInterface;
using static System.Sysutils.SysutilsInterface;
using static System.Types.TypesInterface;

namespace System
{
namespace Dateutils
{

/*
  D2CSharp Runtime Library (RTL)

  C# runtime implementation corresponding to the declaration-oriented
  System.DateUtils MockRTL unit.

  Copyright (c) 2026 Dr. Detlef Meyer-Eltz, t2t-soft
  SPDX-License-Identifier: Apache-2.0

  Licensed under the Apache License, Version 2.0 (the "License");
  you may not use this file except in compliance with the License.
  You may obtain a copy of the License at

      https://www.apache.org/licenses/LICENSE-2.0

  Unless required by applicable law or agreed to in writing, software
  distributed under the License is distributed on an "AS IS" BASIS,
  WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
  See the License for the specific language governing permissions and
  limitations under the License.

  Part of the D2CSharp project by t2t-soft. See LICENSE and NOTICE.
*/

public static partial class DateutilsInterface
{
    public class EDateTimeException : DException
    {
        public EDateTimeException(string Msg) : base(Msg) {}
        public EDateTimeException(string Msg, params TVarRec[] Args) : base(Msg, Args) {}
        public EDateTimeException(string Msg, TVarRec[] Args, int AHelpContext) : base(Msg, Args, AHelpContext) {}
        public EDateTimeException(string Msg, int AHelpContext) : base(Msg, AHelpContext) {}
        public EDateTimeException(uint Ident) : base(Ident) {}
        public EDateTimeException(Pointer<TResStringRec> ResStringRec) : base(ResStringRec) {}
        public EDateTimeException(uint Ident, params TVarRec[] Args) : base(Ident, Args) {}
        public EDateTimeException(Pointer<TResStringRec> ResStringRec, params TVarRec[] Args) : base(ResStringRec, Args) {}
        public EDateTimeException(Pointer<TResStringRec> ResStringRec, TVarRec[] Args, int AHelpContext) : base(ResStringRec, Args, AHelpContext) {}
        public EDateTimeException(uint Ident, TVarRec[] Args, int AHelpContext) : base(Ident, Args, AHelpContext) {}
        public EDateTimeException(uint Ident, int AHelpContext) : base(Ident, AHelpContext) {}
        public EDateTimeException(Pointer<TResStringRec> ResStringRec, int AHelpContext) : base(ResStringRec, AHelpContext) {}
    }

    public const int DaysPerWeek = 7;
    public const int WeeksPerFortnight = 2;
    public const int MonthsPerYear = 12;
    public const int YearsPerDecade = 10;
    public const int YearsPerCentury = 100;
    public const int YearsPerMillennium = 1000;
    public const int HoursPerDay = 24;
    public const int MinsPerHour = 60;
    public const int SecsPerMin = 60;
    public const int MSecsPerSec = 1000;
    public const int MinsPerDay = HoursPerDay * MinsPerHour;
    public const int SecsPerHour = MinsPerHour * SecsPerMin;
    public const int SecsPerDay = MinsPerDay * SecsPerMin;
    public const int MSecsPerMin = SecsPerMin * MSecsPerSec;
    public const int MSecsPerHour = SecsPerHour * MSecsPerSec;
    public const int MSecsPerDay = SecsPerDay * MSecsPerSec;
    public const int DaysPerYear = 365;
    public const double ApproxDaysPerMonth = 30.4375D;
    public const double ApproxDaysPerYear = 365.25D;
    public const double OneHour = 1.0D / HoursPerDay;
    public const double OneMinute = 1.0D / MinsPerDay;
    public const double OneSecond = 1.0D / SecsPerDay;
    public const double OneMillisecond = 1.0D / MSecsPerDay;
    public const int RecodeLeaveFieldAsIs = 65535;

    public const int DayMonday = 1;
    public const int DayTuesday = 2;
    public const int DayWednesday = 3;
    public const int DayThursday = 4;
    public const int DayFriday = 5;
    public const int DaySaturday = 6;
    public const int DaySunday = 7;

    public const int MonthJanuary = 1;
    public const int MonthFebruary = 2;
    public const int MonthMarch = 3;
    public const int MonthApril = 4;
    public const int MonthMay = 5;
    public const int MonthJune = 6;
    public const int MonthJuly = 7;
    public const int MonthAugust = 8;
    public const int MonthSeptember = 9;
    public const int MonthOctober = 10;
    public const int MonthNovember = 11;
    public const int MonthDecember = 12;

    public enum TDateTimePart
    {
        dtpYear,
        dtpMonth,
        dtpWeek,
        dtpDay,
        dtpHour,
        dtpMinute,
        dtpSecond,
        dtpMilliSecond
    }

    public enum TISO8601ToDateOption
    {
        ioReturnUTC,
        ioNoTZIsLocal
    }

    private const double JulianDateAtOADateZero = 2415018.5D;
    private const double ModifiedJulianDateAtOADateZero = 15018.0D;

    private static global::System.DateTime DT(TDateTime value) => value.ToDateTime();
    private static TDateTime TD(global::System.DateTime value) => new TDateTime(value);

    private static int IsoDay(global::System.DateTime value)
    {
        int day = (int)value.DayOfWeek;
        return day == 0 ? 7 : day;
    }

    private static global::System.DateTime MondayOfWeek(global::System.DateTime value) =>
        value.Date.AddDays(1 - IsoDay(value));

    private static global::System.DateTime FirstIsoWeekMondayOfMonth(int year, int month)
    {
        global::System.DateTime fourth = new global::System.DateTime(year, month, 4);
        return MondayOfWeek(fourth);
    }

    private static void IsoMonthWeek(global::System.DateTime value, out int year, out int month, out int week)
    {
        global::System.DateTime monday = MondayOfWeek(value);
        global::System.DateTime thursday = monday.AddDays(3);
        year = thursday.Year;
        month = thursday.Month;
        global::System.DateTime first = FirstIsoWeekMondayOfMonth(year, month);
        week = ((monday - first).Days / 7) + 1;
    }

    private static global::System.DateTime EncodeIsoMonthWeek(int year, int month, int week, int isoDay)
    {
        if (year < 1 || year > 9999 || month < 1 || month > 12 ||
            week < 1 || week > 6 || isoDay < 1 || isoDay > 7)
            throw new EDateTimeException("Invalid ISO month/week date.");

        global::System.DateTime result =
            FirstIsoWeekMondayOfMonth(year, month).AddDays((week - 1) * 7 + isoDay - 1);

        IsoMonthWeek(result, out int ownerYear, out int ownerMonth, out int ownerWeek);
        if (ownerYear != year || ownerMonth != month || ownerWeek != week)
            throw new EDateTimeException("Invalid ISO month/week date.");

        return result;
    }

    private static bool TryDateTime(
        int year, int month, int day, int hour, int minute, int second, int millisecond,
        out global::System.DateTime result)
    {
        try
        {
            result = new global::System.DateTime(year, month, day, hour, minute, second, millisecond);
            return true;
        }
        catch
        {
            result = default;
            return false;
        }
    }

    private static ushort RecodePart(ushort oldValue, ushort newValue) =>
        newValue == RecodeLeaveFieldAsIs ? oldValue : newValue;

    private static TDateTime RecodeCore(
        TDateTime value,
        ushort year, ushort month, ushort day,
        ushort hour, ushort minute, ushort second, ushort millisecond)
    {
        global::System.DateTime d = DT(value);
        int y = RecodePart((ushort)d.Year, year);
        int m = RecodePart((ushort)d.Month, month);
        int da = RecodePart((ushort)d.Day, day);
        int h = RecodePart((ushort)d.Hour, hour);
        int mi = RecodePart((ushort)d.Minute, minute);
        int s = RecodePart((ushort)d.Second, second);
        int ms = RecodePart((ushort)d.Millisecond, millisecond);

        if (!TryDateTime(y, m, da, h, mi, s, ms, out global::System.DateTime result))
            throw new EDateTimeException("Invalid recoded date/time.");

        return TD(result);
    }

    private static double AbsoluteDays(TDateTime a, TDateTime b) =>
        global::System.Math.Abs(DT(a).Subtract(DT(b)).TotalDays);

    private static int Compare(double a, double b) =>
        a < b ? LessThanValue : (a > b ? GreaterThanValue : EqualsValue);

    private static long TruncPositive(double value) => (long)global::System.Math.Truncate(global::System.Math.Abs(value));

    public static TDateTime DateOf(TDateTime AValue) => TD(DT(AValue).Date);

    public static TDateTime TimeOf(TDateTime AValue) =>
        new TDateTime(DT(AValue).TimeOfDay.TotalDays);

    public static bool IsInLeapYear(TDateTime AValue) =>
        global::System.DateTime.IsLeapYear(DT(AValue).Year);

    public static bool IsPM(TDateTime AValue) => DT(AValue).Hour >= 12;
    public static bool IsAM(TDateTime AValue) => DT(AValue).Hour < 12;
    public static bool IsToday(TDateTime AValue) => DT(AValue).Date == global::System.DateTime.Today;
    public static bool IsSameDay(TDateTime AValue, TDateTime ABasis) => DT(AValue).Date == DT(ABasis).Date;

    public static bool IsSameMonth(TDateTime AValue, TDateTime ABasis)
    {
        global::System.DateTime a = DT(AValue);
        global::System.DateTime b = DT(ABasis);
        return a.Year == b.Year && a.Month == b.Month;
    }

    public static ushort YearOf(TDateTime AValue) => (ushort)DT(AValue).Year;
    public static ushort MonthOf(TDateTime AValue) => (ushort)DT(AValue).Month;
    public static ushort WeekOf(TDateTime AValue) => WeekOfTheYear(AValue);
    public static ushort DayOf(TDateTime AValue) => (ushort)DT(AValue).Day;
    public static ushort HourOf(TDateTime AValue) => (ushort)DT(AValue).Hour;
    public static ushort MinuteOf(TDateTime AValue) => (ushort)DT(AValue).Minute;
    public static ushort SecondOf(TDateTime AValue) => (ushort)DT(AValue).Second;
    public static ushort MilliSecondOf(TDateTime AValue) => (ushort)DT(AValue).Millisecond;

    public static ushort WeekOfTheYear(TDateTime AValue) =>
        (ushort)global::System.Globalization.ISOWeek.GetWeekOfYear(DT(AValue));

    public static ushort WeekOfTheYear(TDateTime AValue, ref ushort AYear)
    {
        global::System.DateTime d = DT(AValue);
        AYear = (ushort)global::System.Globalization.ISOWeek.GetYear(d);
        return (ushort)global::System.Globalization.ISOWeek.GetWeekOfYear(d);
    }

    public static ushort MonthOfTheYear(TDateTime AValue) => MonthOf(AValue);
    public static ushort DayOfTheYear(TDateTime AValue) => (ushort)DT(AValue).DayOfYear;
    public static ushort DayOfTheMonth(TDateTime AValue) => DayOf(AValue);
    public static ushort DayOfTheWeek(TDateTime AValue) => (ushort)IsoDay(DT(AValue));

    public static ushort WeekOfTheMonth(TDateTime AValue)
    {
        IsoMonthWeek(DT(AValue), out _, out _, out int week);
        return (ushort)week;
    }

    public static ushort NthDayOfWeek(TDateTime AValue) =>
        (ushort)(((DT(AValue).Day - 1) / 7) + 1);

    public static ushort HourOfTheYear(TDateTime AValue)
    {
        global::System.DateTime d = DT(AValue);
        return (ushort)((d.DayOfYear - 1) * HoursPerDay + d.Hour);
    }

    public static ushort HourOfTheMonth(TDateTime AValue)
    {
        global::System.DateTime d = DT(AValue);
        return (ushort)((d.Day - 1) * HoursPerDay + d.Hour);
    }

    public static ushort HourOfTheWeek(TDateTime AValue)
    {
        global::System.DateTime d = DT(AValue);
        return (ushort)((IsoDay(d) - 1) * HoursPerDay + d.Hour);
    }

    public static ushort HourOfTheDay(TDateTime AValue) => HourOf(AValue);

    public static uint MinuteOfTheYear(TDateTime AValue) =>
        (uint)(HourOfTheYear(AValue) * MinsPerHour + DT(AValue).Minute);

    public static ushort MinuteOfTheMonth(TDateTime AValue) =>
        (ushort)(HourOfTheMonth(AValue) * MinsPerHour + DT(AValue).Minute);

    public static ushort MinuteOfTheWeek(TDateTime AValue) =>
        (ushort)(HourOfTheWeek(AValue) * MinsPerHour + DT(AValue).Minute);

    public static ushort MinuteOfTheDay(TDateTime AValue) =>
        (ushort)(DT(AValue).Hour * MinsPerHour + DT(AValue).Minute);

    public static ushort MinuteOfTheHour(TDateTime AValue) => MinuteOf(AValue);

    public static uint SecondOfTheYear(TDateTime AValue) =>
        MinuteOfTheYear(AValue) * SecsPerMin + (uint)DT(AValue).Second;

    public static uint SecondOfTheMonth(TDateTime AValue) =>
        (uint)MinuteOfTheMonth(AValue) * SecsPerMin + (uint)DT(AValue).Second;

    public static uint SecondOfTheWeek(TDateTime AValue) =>
        (uint)MinuteOfTheWeek(AValue) * SecsPerMin + (uint)DT(AValue).Second;

    public static uint SecondOfTheDay(TDateTime AValue) =>
        (uint)MinuteOfTheDay(AValue) * SecsPerMin + (uint)DT(AValue).Second;

    public static ushort SecondOfTheHour(TDateTime AValue) =>
        (ushort)(DT(AValue).Minute * SecsPerMin + DT(AValue).Second);

    public static ushort SecondOfTheMinute(TDateTime AValue) => SecondOf(AValue);

    public static long MilliSecondOfTheYear(TDateTime AValue) =>
        (long)SecondOfTheYear(AValue) * MSecsPerSec + DT(AValue).Millisecond;

    public static uint MilliSecondOfTheMonth(TDateTime AValue) =>
        SecondOfTheMonth(AValue) * MSecsPerSec + (uint)DT(AValue).Millisecond;

    public static uint MilliSecondOfTheWeek(TDateTime AValue) =>
        SecondOfTheWeek(AValue) * MSecsPerSec + (uint)DT(AValue).Millisecond;

    public static uint MilliSecondOfTheDay(TDateTime AValue) =>
        SecondOfTheDay(AValue) * MSecsPerSec + (uint)DT(AValue).Millisecond;

    public static uint MilliSecondOfTheHour(TDateTime AValue) =>
        (uint)SecondOfTheHour(AValue) * MSecsPerSec + (uint)DT(AValue).Millisecond;

    public static ushort MilliSecondOfTheMinute(TDateTime AValue) =>
        (ushort)(DT(AValue).Second * MSecsPerSec + DT(AValue).Millisecond);

    public static ushort MilliSecondOfTheSecond(TDateTime AValue) => MilliSecondOf(AValue);

    public static ushort WeeksInAYear(ushort AYear)
    {
        if (AYear < 1 || AYear > 9999)
            throw new EDateTimeException("Invalid year.");
        return (ushort)global::System.Globalization.ISOWeek.GetWeeksInYear(AYear);
    }

    public static ushort WeeksInYear(TDateTime AValue) => WeeksInAYear(YearOf(AValue));

    public static ushort DaysInAYear(ushort AYear)
    {
        if (AYear < 1 || AYear > 9999)
            throw new EDateTimeException("Invalid year.");
        return (ushort)(global::System.DateTime.IsLeapYear(AYear) ? 366 : 365);
    }

    public static ushort DaysInYear(TDateTime AValue) => DaysInAYear(YearOf(AValue));

    public static ushort DaysInAMonth(ushort AYear, ushort AMonth)
    {
        try
        {
            return (ushort)global::System.DateTime.DaysInMonth(AYear, AMonth);
        }
        catch
        {
            throw new EDateTimeException("Invalid year or month.");
        }
    }

    public static ushort DaysInMonth(TDateTime AValue) =>
        DaysInAMonth(YearOf(AValue), MonthOf(AValue));

    public static TDateTime EncodeDateDay(ushort AYear, ushort ADayOfYear)
    {
        if (!TryEncodeDateDay(AYear, ADayOfYear, out TDateTime result))
            throw new EDateTimeException("Invalid day of year.");
        return result;
    }

    public static bool TryEncodeDateDay(ushort AYear, ushort ADayOfYear, out TDateTime AValue)
    {
        AValue = default;
        if (AYear < 1 || AYear > 9999 || ADayOfYear < 1 || ADayOfYear > DaysInAYear(AYear))
            return false;
        AValue = TD(new global::System.DateTime(AYear, 1, 1).AddDays(ADayOfYear - 1));
        return true;
    }

    public static void DecodeDateDay(TDateTime AValue, out ushort AYear, out ushort ADayOfYear)
    {
        global::System.DateTime d = DT(AValue);
        AYear = (ushort)d.Year;
        ADayOfYear = (ushort)d.DayOfYear;
    }

    public static TDateTime EncodeDateWeek(ushort AYear, ushort AWeekOfYear, ushort ADayOfWeek = 1)
    {
        if (!TryEncodeDateWeek(AYear, AWeekOfYear, out TDateTime result, ADayOfWeek))
            throw new EDateTimeException("Invalid ISO week date.");
        return result;
    }

    public static bool TryEncodeDateWeek(ushort AYear, ushort AWeekOfYear, out TDateTime AValue, ushort ADayOfWeek = 1)
    {
        AValue = default;
        if (AYear < 1 || AYear > 9999 || ADayOfWeek < 1 || ADayOfWeek > 7)
            return false;
        if (AWeekOfYear < 1 || AWeekOfYear > global::System.Globalization.ISOWeek.GetWeeksInYear(AYear))
            return false;
        try
        {
            AValue = TD(global::System.Globalization.ISOWeek.ToDateTime(
                AYear, AWeekOfYear, (global::System.DayOfWeek)(ADayOfWeek == 7 ? 0 : ADayOfWeek)));
            return true;
        }
        catch
        {
            return false;
        }
    }

    public static void DecodeDateWeek(TDateTime AValue, out ushort AYear, out ushort AWeekOfYear, out ushort ADayOfWeek)
    {
        global::System.DateTime d = DT(AValue);
        AYear = (ushort)global::System.Globalization.ISOWeek.GetYear(d);
        AWeekOfYear = (ushort)global::System.Globalization.ISOWeek.GetWeekOfYear(d);
        ADayOfWeek = (ushort)IsoDay(d);
    }

    public static TDateTime EncodeDateMonthWeek(ushort AYear, ushort AMonth, ushort AWeekOfMonth, ushort ADayOfWeek)
    {
        if (!TryEncodeDateMonthWeek(AYear, AMonth, AWeekOfMonth, ADayOfWeek, out TDateTime result))
            throw new EDateTimeException("Invalid ISO month/week date.");
        return result;
    }

    public static bool TryEncodeDateMonthWeek(ushort AYear, ushort AMonth, ushort AWeekOfMonth, ushort ADayOfWeek, out TDateTime AValue)
    {
        AValue = default;
        try
        {
            AValue = TD(EncodeIsoMonthWeek(AYear, AMonth, AWeekOfMonth, ADayOfWeek));
            return true;
        }
        catch
        {
            return false;
        }
    }

    public static void DecodeDateMonthWeek(TDateTime AValue, out ushort AYear, out ushort AMonth, out ushort AWeekOfMonth, out ushort ADayOfWeek)
    {
        global::System.DateTime d = DT(AValue);
        IsoMonthWeek(d, out int y, out int m, out int w);
        AYear = (ushort)y;
        AMonth = (ushort)m;
        AWeekOfMonth = (ushort)w;
        ADayOfWeek = (ushort)IsoDay(d);
    }

    public static TDateTime EncodeDayOfWeekInMonth(ushort AYear, ushort AMonth, ushort ANthDayOfWeek, ushort ADayOfWeek)
    {
        if (!TryEncodeDayOfWeekInMonth(AYear, AMonth, ANthDayOfWeek, ADayOfWeek, out TDateTime result))
            throw new EDateTimeException("Invalid nth weekday in month.");
        return result;
    }

    public static bool TryEncodeDayOfWeekInMonth(ushort AYear, ushort AMonth, ushort ANthDayOfWeek, ushort ADayOfWeek, out TDateTime AValue)
    {
        AValue = default;
        if (AYear < 1 || AYear > 9999 || AMonth < 1 || AMonth > 12 ||
            ANthDayOfWeek < 1 || ANthDayOfWeek > 5 || ADayOfWeek < 1 || ADayOfWeek > 7)
            return false;

        global::System.DateTime first = new global::System.DateTime(AYear, AMonth, 1);
        int delta = (ADayOfWeek - IsoDay(first) + 7) % 7;
        global::System.DateTime result = first.AddDays(delta + (ANthDayOfWeek - 1) * 7);
        if (result.Month != AMonth)
            return false;

        AValue = TD(result);
        return true;
    }

    public static void DecodeDayOfWeekInMonth(TDateTime AValue, out ushort AYear, out ushort AMonth, out ushort ANthDayOfWeek, out ushort ADayOfWeek)
    {
        global::System.DateTime d = DT(AValue);
        AYear = (ushort)d.Year;
        AMonth = (ushort)d.Month;
        ANthDayOfWeek = (ushort)(((d.Day - 1) / 7) + 1);
        ADayOfWeek = (ushort)IsoDay(d);
    }

    public static TDateTime EncodeDateTime(ushort AYear, ushort AMonth, ushort ADay, ushort AHour, ushort AMinute, ushort ASecond, ushort AMilliSecond)
    {
        if (!TryEncodeDateTime(AYear, AMonth, ADay, AHour, AMinute, ASecond, AMilliSecond, out TDateTime result))
            throw new EDateTimeException("Invalid date/time.");
        return result;
    }

    public static bool TryEncodeDateTime(ushort AYear, ushort AMonth, ushort ADay, ushort AHour, ushort AMinute, ushort ASecond, ushort AMilliSecond, out TDateTime AValue)
    {
        AValue = default;
        if (!TryDateTime(AYear, AMonth, ADay, AHour, AMinute, ASecond, AMilliSecond, out global::System.DateTime result))
            return false;
        AValue = TD(result);
        return true;
    }

    public static void DecodeDateTime(TDateTime AValue, out ushort AYear, out ushort AMonth, out ushort ADay, out ushort AHour, out ushort AMinute, out ushort ASecond, out ushort AMilliSecond)
    {
        global::System.DateTime d = DT(AValue);
        AYear = (ushort)d.Year;
        AMonth = (ushort)d.Month;
        ADay = (ushort)d.Day;
        AHour = (ushort)d.Hour;
        AMinute = (ushort)d.Minute;
        ASecond = (ushort)d.Second;
        AMilliSecond = (ushort)d.Millisecond;
    }

    public static TDateTime StartOfTheYear(TDateTime AValue) => StartOfAYear(YearOf(AValue));
    public static TDateTime EndOfTheYear(TDateTime AValue) => EndOfAYear(YearOf(AValue));
    public static TDateTime StartOfAYear(ushort AYear) => TD(new global::System.DateTime(AYear, 1, 1));
    public static TDateTime EndOfAYear(ushort AYear) => TD(new global::System.DateTime(AYear, 12, 31, 23, 59, 59, 999));

    public static TDateTime StartOfTheMonth(TDateTime AValue) =>
        StartOfAMonth(YearOf(AValue), MonthOf(AValue));

    public static TDateTime EndOfTheMonth(TDateTime AValue) =>
        EndOfAMonth(YearOf(AValue), MonthOf(AValue));

    public static TDateTime StartOfAMonth(ushort AYear, ushort AMonth) =>
        TD(new global::System.DateTime(AYear, AMonth, 1));

    public static TDateTime EndOfAMonth(ushort AYear, ushort AMonth) =>
        TD(new global::System.DateTime(AYear, AMonth, global::System.DateTime.DaysInMonth(AYear, AMonth), 23, 59, 59, 999));

    public static TDateTime StartOfTheWeek(TDateTime AValue) => TD(MondayOfWeek(DT(AValue)));

    public static TDateTime EndOfTheWeek(TDateTime AValue) =>
        TD(MondayOfWeek(DT(AValue)).AddDays(6).AddHours(23).AddMinutes(59).AddSeconds(59).AddMilliseconds(999));

    public static TDateTime StartOfAWeek(ushort AYear, ushort AWeekOfYear, ushort ADayOfWeek = 1) =>
        EncodeDateWeek(AYear, AWeekOfYear, ADayOfWeek);

    public static TDateTime EndOfAWeek(ushort AYear, ushort AWeekOfYear, ushort ADayOfWeek = 7)
    {
        global::System.DateTime d = DT(EncodeDateWeek(AYear, AWeekOfYear, ADayOfWeek));
        return TD(d.Date.AddHours(23).AddMinutes(59).AddSeconds(59).AddMilliseconds(999));
    }

    public static TDateTime StartOfTheDay(TDateTime AValue) => TD(DT(AValue).Date);

    public static TDateTime EndOfTheDay(TDateTime AValue) =>
        TD(DT(AValue).Date.AddDays(1).AddMilliseconds(-1));

    public static TDateTime StartOfADay(ushort AYear, ushort AMonth, ushort ADay) =>
        TD(new global::System.DateTime(AYear, AMonth, ADay));

    public static TDateTime StartOfADay(ushort AYear, ushort ADayOfYear) =>
        EncodeDateDay(AYear, ADayOfYear);

    public static TDateTime EndOfADay(ushort AYear, ushort AMonth, ushort ADay) =>
        TD(new global::System.DateTime(AYear, AMonth, ADay).AddDays(1).AddMilliseconds(-1));

    public static TDateTime EndOfADay(ushort AYear, ushort ADayOfYear) =>
        EndOfTheDay(EncodeDateDay(AYear, ADayOfYear));

    public static TDateTime Today() => TDateTime.Today;
    public static TDateTime Yesterday() => TDateTime.Today.AddDays(-1);
    public static TDateTime Tomorrow() => TDateTime.Today.AddDays(1);

    public static TDateTime IncYear(TDateTime AValue, int ANumberOfYears = 1) =>
        TD(DT(AValue).AddYears(ANumberOfYears));

    public static TDateTime IncWeek(TDateTime AValue, int ANumberOfWeeks = 1) =>
        AValue.AddDays((double)ANumberOfWeeks * DaysPerWeek);

    public static TDateTime IncDay(TDateTime AValue, int ANumberOfDays = 1) =>
        AValue.AddDays(ANumberOfDays);

    public static TDateTime IncHour(TDateTime AValue, long ANumberOfHours = 1) =>
        AValue.AddHours(ANumberOfHours);

    public static TDateTime IncMinute(TDateTime AValue, long ANumberOfMinutes = 1) =>
        AValue.AddMinutes(ANumberOfMinutes);

    public static TDateTime IncSecond(TDateTime AValue, long ANumberOfSeconds = 1) =>
        AValue.AddSeconds(ANumberOfSeconds);

    public static TDateTime IncMilliSecond(TDateTime AValue, long ANumberOfMilliSeconds = 1) =>
        AValue.AddMilliseconds(ANumberOfMilliSeconds);

    public static TDateTime RecodeYear(TDateTime AValue, ushort AYear) =>
        RecodeCore(AValue, AYear, RecodeLeaveFieldAsIs, RecodeLeaveFieldAsIs, RecodeLeaveFieldAsIs, RecodeLeaveFieldAsIs, RecodeLeaveFieldAsIs, RecodeLeaveFieldAsIs);

    public static TDateTime RecodeMonth(TDateTime AValue, ushort AMonth) =>
        RecodeCore(AValue, RecodeLeaveFieldAsIs, AMonth, RecodeLeaveFieldAsIs, RecodeLeaveFieldAsIs, RecodeLeaveFieldAsIs, RecodeLeaveFieldAsIs, RecodeLeaveFieldAsIs);

    public static TDateTime RecodeDay(TDateTime AValue, ushort ADay) =>
        RecodeCore(AValue, RecodeLeaveFieldAsIs, RecodeLeaveFieldAsIs, ADay, RecodeLeaveFieldAsIs, RecodeLeaveFieldAsIs, RecodeLeaveFieldAsIs, RecodeLeaveFieldAsIs);

    public static TDateTime RecodeHour(TDateTime AValue, ushort AHour) =>
        RecodeCore(AValue, RecodeLeaveFieldAsIs, RecodeLeaveFieldAsIs, RecodeLeaveFieldAsIs, AHour, RecodeLeaveFieldAsIs, RecodeLeaveFieldAsIs, RecodeLeaveFieldAsIs);

    public static TDateTime RecodeMinute(TDateTime AValue, ushort AMinute) =>
        RecodeCore(AValue, RecodeLeaveFieldAsIs, RecodeLeaveFieldAsIs, RecodeLeaveFieldAsIs, RecodeLeaveFieldAsIs, AMinute, RecodeLeaveFieldAsIs, RecodeLeaveFieldAsIs);

    public static TDateTime RecodeSecond(TDateTime AValue, ushort ASecond) =>
        RecodeCore(AValue, RecodeLeaveFieldAsIs, RecodeLeaveFieldAsIs, RecodeLeaveFieldAsIs, RecodeLeaveFieldAsIs, RecodeLeaveFieldAsIs, ASecond, RecodeLeaveFieldAsIs);

    public static TDateTime RecodeMilliSecond(TDateTime AValue, ushort AMilliSecond) =>
        RecodeCore(AValue, RecodeLeaveFieldAsIs, RecodeLeaveFieldAsIs, RecodeLeaveFieldAsIs, RecodeLeaveFieldAsIs, RecodeLeaveFieldAsIs, RecodeLeaveFieldAsIs, AMilliSecond);

    public static TDateTime RecodeDate(TDateTime AValue, ushort AYear, ushort AMonth, ushort ADay) =>
        RecodeCore(AValue, AYear, AMonth, ADay, RecodeLeaveFieldAsIs, RecodeLeaveFieldAsIs, RecodeLeaveFieldAsIs, RecodeLeaveFieldAsIs);

    public static TDateTime RecodeTime(TDateTime AValue, ushort AHour, ushort AMinute, ushort ASecond, ushort AMilliSecond) =>
        RecodeCore(AValue, RecodeLeaveFieldAsIs, RecodeLeaveFieldAsIs, RecodeLeaveFieldAsIs, AHour, AMinute, ASecond, AMilliSecond);

    public static TDateTime RecodeDateTime(TDateTime AValue, ushort AYear, ushort AMonth, ushort ADay, ushort AHour, ushort AMinute, ushort ASecond, ushort AMilliSecond) =>
        RecodeCore(AValue, AYear, AMonth, ADay, AHour, AMinute, ASecond, AMilliSecond);

    public static bool TryRecodeDateTime(TDateTime AValue, ushort AYear, ushort AMonth, ushort ADay, ushort AHour, ushort AMinute, ushort ASecond, ushort AMilliSecond, out TDateTime AResult)
    {
        try
        {
            AResult = RecodeDateTime(AValue, AYear, AMonth, ADay, AHour, AMinute, ASecond, AMilliSecond);
            return true;
        }
        catch
        {
            AResult = default;
            return false;
        }
    }

    public static bool SameDateTime(TDateTime A, TDateTime B) =>
        DT(A) == DT(B);

    public static bool SameDate(TDateTime A, TDateTime B) =>
        DT(A).Date == DT(B).Date;

    public static bool SameTime(TDateTime A, TDateTime B) =>
        DT(A).TimeOfDay == DT(B).TimeOfDay;

    public static int CompareDateTime(TDateTime A, TDateTime B) =>
        Compare(A.ToDouble(), B.ToDouble());

    public static int CompareDate(TDateTime A, TDateTime B) =>
        Compare(DT(A).Date.ToOADate(), DT(B).Date.ToOADate());

    public static int CompareTime(TDateTime A, TDateTime B) =>
        Compare(DT(A).TimeOfDay.TotalDays, DT(B).TimeOfDay.TotalDays);

    public static int YearsBetween(TDateTime ANow, TDateTime AThen) =>
        (int)global::System.Math.Truncate(YearSpan(ANow, AThen));

    public static int MonthsBetween(TDateTime ANow, TDateTime AThen) =>
        (int)global::System.Math.Truncate(MonthSpan(ANow, AThen));

    public static int WeeksBetween(TDateTime ANow, TDateTime AThen) =>
        (int)global::System.Math.Truncate(WeekSpan(ANow, AThen));

    public static int DaysBetween(TDateTime ANow, TDateTime AThen) =>
        (int)global::System.Math.Truncate(DaySpan(ANow, AThen));

    public static long HoursBetween(TDateTime ANow, TDateTime AThen) =>
        TruncPositive(HourSpan(ANow, AThen));

    public static long MinutesBetween(TDateTime ANow, TDateTime AThen) =>
        TruncPositive(MinuteSpan(ANow, AThen));

    public static long SecondsBetween(TDateTime ANow, TDateTime AThen) =>
        TruncPositive(SecondSpan(ANow, AThen));

    public static long MilliSecondsBetween(TDateTime ANow, TDateTime AThen) =>
        TruncPositive(MilliSecondSpan(ANow, AThen));

    public static double YearSpan(TDateTime ANow, TDateTime AThen) =>
        DaySpan(ANow, AThen) / ApproxDaysPerYear;

    public static double MonthSpan(TDateTime ANow, TDateTime AThen) =>
        DaySpan(ANow, AThen) / ApproxDaysPerMonth;

    public static double WeekSpan(TDateTime ANow, TDateTime AThen) =>
        DaySpan(ANow, AThen) / DaysPerWeek;

    public static double DaySpan(TDateTime ANow, TDateTime AThen) =>
        AbsoluteDays(ANow, AThen);

    public static double HourSpan(TDateTime ANow, TDateTime AThen) =>
        DaySpan(ANow, AThen) * HoursPerDay;

    public static double MinuteSpan(TDateTime ANow, TDateTime AThen) =>
        DaySpan(ANow, AThen) * MinsPerDay;

    public static double SecondSpan(TDateTime ANow, TDateTime AThen) =>
        DaySpan(ANow, AThen) * SecsPerDay;

    public static double MilliSecondSpan(TDateTime ANow, TDateTime AThen) =>
        DaySpan(ANow, AThen) * MSecsPerDay;

    public static bool WithinPastYears(TDateTime ANow, TDateTime AThen, int AYears) =>
        YearsBetween(ANow, AThen) <= global::System.Math.Abs(AYears);

    public static bool WithinPastMonths(TDateTime ANow, TDateTime AThen, int AMonths) =>
        MonthsBetween(ANow, AThen) <= global::System.Math.Abs(AMonths);

    public static bool WithinPastWeeks(TDateTime ANow, TDateTime AThen, int AWeeks) =>
        WeeksBetween(ANow, AThen) <= global::System.Math.Abs(AWeeks);

    public static bool WithinPastDays(TDateTime ANow, TDateTime AThen, int ADays) =>
        DaysBetween(ANow, AThen) <= global::System.Math.Abs(ADays);

    public static bool WithinPastHours(TDateTime ANow, TDateTime AThen, long AHours) =>
        HoursBetween(ANow, AThen) <= global::System.Math.Abs(AHours);

    public static bool WithinPastMinutes(TDateTime ANow, TDateTime AThen, long AMinutes) =>
        MinutesBetween(ANow, AThen) <= global::System.Math.Abs(AMinutes);

    public static bool WithinPastSeconds(TDateTime ANow, TDateTime AThen, long ASeconds) =>
        SecondsBetween(ANow, AThen) <= global::System.Math.Abs(ASeconds);

    public static bool WithinPastMilliSeconds(TDateTime ANow, TDateTime AThen, long AMilliSeconds) =>
        MilliSecondsBetween(ANow, AThen) <= global::System.Math.Abs(AMilliSeconds);

    public static bool IsValidDate(ushort AYear, ushort AMonth, ushort ADay) =>
        TryDateTime(AYear, AMonth, ADay, 0, 0, 0, 0, out _);

    public static bool IsValidDateDay(ushort AYear, ushort ADayOfYear)
    {
        if (AYear < 1 || AYear > 9999)
            return false;
        return ADayOfYear >= 1 && ADayOfYear <= (global::System.DateTime.IsLeapYear(AYear) ? 366 : 365);
    }

    public static bool IsValidDateWeek(ushort AYear, ushort AWeekOfYear, ushort ADayOfWeek)
    {
        if (AYear < 1 || AYear > 9999 || ADayOfWeek < 1 || ADayOfWeek > 7)
            return false;
        return AWeekOfYear >= 1 && AWeekOfYear <= global::System.Globalization.ISOWeek.GetWeeksInYear(AYear);
    }

    public static bool IsValidDateMonthWeek(ushort AYear, ushort AMonth, ushort AWeekOfMonth, ushort ADayOfWeek)
    {
        try
        {
            EncodeIsoMonthWeek(AYear, AMonth, AWeekOfMonth, ADayOfWeek);
            return true;
        }
        catch
        {
            return false;
        }
    }

    public static bool IsValidTime(ushort AHour, ushort AMinute, ushort ASecond, ushort AMilliSecond) =>
        AHour <= 23 && AMinute <= 59 && ASecond <= 59 && AMilliSecond <= 999;

    public static bool IsValidDateTime(ushort AYear, ushort AMonth, ushort ADay, ushort AHour, ushort AMinute, ushort ASecond, ushort AMilliSecond) =>
        TryDateTime(AYear, AMonth, ADay, AHour, AMinute, ASecond, AMilliSecond, out _);

    public static double DateTimeToJulianDate(TDateTime AValue) =>
        AValue.ToDouble() + JulianDateAtOADateZero;

    public static TDateTime JulianDateToDateTime(double AValue)
    {
        if (!TryJulianDateToDateTime(AValue, out TDateTime result))
            throw new EDateTimeException("Invalid Julian date.");
        return result;
    }

    public static bool TryJulianDateToDateTime(double AValue, out TDateTime ADateTime)
    {
        ADateTime = default;
        try
        {
            double oa = AValue - JulianDateAtOADateZero;
            ADateTime = TD(global::System.DateTime.FromOADate(oa));
            return true;
        }
        catch
        {
            return false;
        }
    }

    public static double DateTimeToModifiedJulianDate(TDateTime AValue) =>
        AValue.ToDouble() + ModifiedJulianDateAtOADateZero;

    public static TDateTime ModifiedJulianDateToDateTime(double AValue)
    {
        if (!TryModifiedJulianDateToDateTime(AValue, out TDateTime result))
            throw new EDateTimeException("Invalid modified Julian date.");
        return result;
    }

    public static bool TryModifiedJulianDateToDateTime(double AValue, out TDateTime ADateTime)
    {
        ADateTime = default;
        try
        {
            double oa = AValue - ModifiedJulianDateAtOADateZero;
            ADateTime = TD(global::System.DateTime.FromOADate(oa));
            return true;
        }
        catch
        {
            return false;
        }
    }

    public static long DateTimeToUnix(TDateTime AValue, bool AInputIsUTC = true)
    {
        global::System.DateTime d = DT(AValue);
        global::System.DateTime utc = AInputIsUTC
            ? global::System.DateTime.SpecifyKind(d, global::System.DateTimeKind.Utc)
            : global::System.TimeZoneInfo.ConvertTimeToUtc(
                global::System.DateTime.SpecifyKind(d, global::System.DateTimeKind.Local));

        return new global::System.DateTimeOffset(utc).ToUnixTimeSeconds();
    }

    public static TDateTime UnixToDateTime(long AValue, bool AReturnUTC = true)
    {
        global::System.DateTimeOffset dto = global::System.DateTimeOffset.FromUnixTimeSeconds(AValue);
        global::System.DateTime d = AReturnUTC ? dto.UtcDateTime : dto.LocalDateTime;
        return TD(global::System.DateTime.SpecifyKind(d, global::System.DateTimeKind.Unspecified));
    }

    public static string DateToISO8601(TDateTime ADate, bool AInputIsUTC = true)
    {
        global::System.DateTime d = DT(ADate);
        if (AInputIsUTC)
        {
            d = global::System.DateTime.SpecifyKind(d, global::System.DateTimeKind.Utc);
            return d.ToString("yyyy-MM-dd'T'HH:mm:ss.fff'Z'", global::System.Globalization.CultureInfo.InvariantCulture);
        }

        d = global::System.DateTime.SpecifyKind(d, global::System.DateTimeKind.Unspecified);
        global::System.TimeSpan offset = global::System.TimeZoneInfo.Local.GetUtcOffset(d);
        global::System.DateTimeOffset dto = new global::System.DateTimeOffset(d, offset);
        return dto.ToString("yyyy-MM-dd'T'HH:mm:ss.fffzzz", global::System.Globalization.CultureInfo.InvariantCulture);
    }

    public static TDateTime ISO8601ToDate(string AISODate, bool AReturnUTC = true)
    {
        TSet options = new TSet();
        if (AReturnUTC)
            options.Include((int)TISO8601ToDateOption.ioReturnUTC);
        options.Include((int)TISO8601ToDateOption.ioNoTZIsLocal);
        return ISO8601ToDate(AISODate, options);
    }

    public static TDateTime ISO8601ToDate(string AISODate, TSet AOptions)
    {
        if (string.IsNullOrWhiteSpace(AISODate))
            throw new EDateTimeException("Invalid ISO-8601 date.");

        bool returnUtc = AOptions != null && AOptions.Contains((int)TISO8601ToDateOption.ioReturnUTC);
        bool noTzIsLocal = AOptions != null && AOptions.Contains((int)TISO8601ToDateOption.ioNoTZIsLocal);

        const global::System.Globalization.DateTimeStyles Styles =
            global::System.Globalization.DateTimeStyles.AllowWhiteSpaces;

        bool hasZone =
            AISODate.EndsWith("Z", global::System.StringComparison.OrdinalIgnoreCase) ||
            global::System.Text.RegularExpressions.Regex.IsMatch(
                AISODate, @"[+\-]\d{2}:\d{2}$", global::System.Text.RegularExpressions.RegexOptions.CultureInvariant);

        if (hasZone)
        {
            if (!global::System.DateTimeOffset.TryParse(
                    AISODate,
                    global::System.Globalization.CultureInfo.InvariantCulture,
                    Styles,
                    out global::System.DateTimeOffset dto))
                throw new EDateTimeException("Invalid ISO-8601 date.");

            global::System.DateTime d = returnUtc ? dto.UtcDateTime : dto.LocalDateTime;
            return TD(global::System.DateTime.SpecifyKind(d, global::System.DateTimeKind.Unspecified));
        }

        if (!global::System.DateTime.TryParse(
                AISODate,
                global::System.Globalization.CultureInfo.InvariantCulture,
                Styles,
                out global::System.DateTime plain))
            throw new EDateTimeException("Invalid ISO-8601 date.");

        plain = global::System.DateTime.SpecifyKind(
            plain,
            noTzIsLocal ? global::System.DateTimeKind.Local : global::System.DateTimeKind.Utc);

        if (returnUtc && plain.Kind == global::System.DateTimeKind.Local)
            plain = plain.ToUniversalTime();
        else if (!returnUtc && plain.Kind == global::System.DateTimeKind.Utc)
            plain = plain.ToLocalTime();

        return TD(global::System.DateTime.SpecifyKind(plain, global::System.DateTimeKind.Unspecified));
    }
}

file static class DateutilsImplementation
{
}

} // namespace Dateutils
} // namespace System
