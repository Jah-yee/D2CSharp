using System;
using System.Globalization;
using static System.Character.CharacterImplementation;
using static System.Character.CharacterInterface;
using static System.SystemInterface;

namespace System
{

namespace Character
{

/*
  D2CSharp Runtime Library (RTL)

  This Delphi source file provides a declaration-oriented mock of selected
  System.Character functionality required by D2CSharp.

  The file provides the C# runtime implementation corresponding to the
  declaration-oriented System.Character MockRTL unit.

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

public class CharacterInterface
{

    private static void ValidateScalar(uint value)
    {
        if (value > 0x10FFFFU || (value >= 0xD800U && value <= 0xDFFFU))
            throw new global::System.ArgumentOutOfRangeException(nameof(value));
    }

    private static string ScalarToString(uint value)
    {
        ValidateScalar(value);
        return global::System.Char.ConvertFromUtf32((int)value);
    }

    private static TUnicodeCategory MapCategory(global::System.Globalization.UnicodeCategory category)
    {
        switch (category)
        {
            case global::System.Globalization.UnicodeCategory.Control: return TUnicodeCategory.ucControl;
            case global::System.Globalization.UnicodeCategory.Format: return TUnicodeCategory.ucFormat;
            case global::System.Globalization.UnicodeCategory.OtherNotAssigned: return TUnicodeCategory.ucUnassigned;
            case global::System.Globalization.UnicodeCategory.PrivateUse: return TUnicodeCategory.ucPrivateUse;
            case global::System.Globalization.UnicodeCategory.Surrogate: return TUnicodeCategory.ucSurrogate;
            case global::System.Globalization.UnicodeCategory.LowercaseLetter: return TUnicodeCategory.ucLowercaseLetter;
            case global::System.Globalization.UnicodeCategory.ModifierLetter: return TUnicodeCategory.ucModifierLetter;
            case global::System.Globalization.UnicodeCategory.OtherLetter: return TUnicodeCategory.ucOtherLetter;
            case global::System.Globalization.UnicodeCategory.TitlecaseLetter: return TUnicodeCategory.ucTitlecaseLetter;
            case global::System.Globalization.UnicodeCategory.UppercaseLetter: return TUnicodeCategory.ucUppercaseLetter;
            case global::System.Globalization.UnicodeCategory.SpacingCombiningMark: return TUnicodeCategory.ucCombiningMark;
            case global::System.Globalization.UnicodeCategory.EnclosingMark: return TUnicodeCategory.ucEnclosingMark;
            case global::System.Globalization.UnicodeCategory.NonSpacingMark: return TUnicodeCategory.ucNonSpacingMark;
            case global::System.Globalization.UnicodeCategory.DecimalDigitNumber: return TUnicodeCategory.ucDecimalNumber;
            case global::System.Globalization.UnicodeCategory.LetterNumber: return TUnicodeCategory.ucLetterNumber;
            case global::System.Globalization.UnicodeCategory.OtherNumber: return TUnicodeCategory.ucOtherNumber;
            case global::System.Globalization.UnicodeCategory.ConnectorPunctuation: return TUnicodeCategory.ucConnectPunctuation;
            case global::System.Globalization.UnicodeCategory.DashPunctuation: return TUnicodeCategory.ucDashPunctuation;
            case global::System.Globalization.UnicodeCategory.ClosePunctuation: return TUnicodeCategory.ucClosePunctuation;
            case global::System.Globalization.UnicodeCategory.FinalQuotePunctuation: return TUnicodeCategory.ucFinalPunctuation;
            case global::System.Globalization.UnicodeCategory.InitialQuotePunctuation: return TUnicodeCategory.ucInitialPunctuation;
            case global::System.Globalization.UnicodeCategory.OtherPunctuation: return TUnicodeCategory.ucOtherPunctuation;
            case global::System.Globalization.UnicodeCategory.OpenPunctuation: return TUnicodeCategory.ucOpenPunctuation;
            case global::System.Globalization.UnicodeCategory.CurrencySymbol: return TUnicodeCategory.ucCurrencySymbol;
            case global::System.Globalization.UnicodeCategory.ModifierSymbol: return TUnicodeCategory.ucModifierSymbol;
            case global::System.Globalization.UnicodeCategory.MathSymbol: return TUnicodeCategory.ucMathSymbol;
            case global::System.Globalization.UnicodeCategory.OtherSymbol: return TUnicodeCategory.ucOtherSymbol;
            case global::System.Globalization.UnicodeCategory.LineSeparator: return TUnicodeCategory.ucLineSeparator;
            case global::System.Globalization.UnicodeCategory.ParagraphSeparator: return TUnicodeCategory.ucParagraphSeparator;
            case global::System.Globalization.UnicodeCategory.SpaceSeparator: return TUnicodeCategory.ucSpaceSeparator;
            default: return TUnicodeCategory.ucUnassigned;
        }
    }

    private static global::System.Globalization.UnicodeCategory GetCategory(uint value)
    {
        string s = ScalarToString(value);
        return global::System.Globalization.CharUnicodeInfo.GetUnicodeCategory(s, 0);
    }

    private static bool IsCategory(uint value, params global::System.Globalization.UnicodeCategory[] categories)
    {
        global::System.Globalization.UnicodeCategory c = GetCategory(value);
        foreach (global::System.Globalization.UnicodeCategory category in categories)
            if (c == category)
                return true;
        return false;
    }

    private static uint TransformScalar(uint value, bool upper, bool invariant)
    {
        string s = ScalarToString(value);
        string transformed = upper
            ? (invariant ? s.ToUpperInvariant() : s.ToUpper(global::System.Globalization.CultureInfo.CurrentCulture))
            : (invariant ? s.ToLowerInvariant() : s.ToLower(global::System.Globalization.CultureInfo.CurrentCulture));

        if (string.IsNullOrEmpty(transformed))
            return value;

        int scalar = global::System.Char.ConvertToUtf32(transformed, 0);
        return unchecked((uint)scalar);
    }


	public struct TCharacter
	{
		public static string ConvertFromUtf32(uint ConvertChar)
	{
		return ScalarToString(ConvertChar);
	}
		public static uint ConvertToUtf32(char HighSurrogate, char LowSurrogate)
	{
		return unchecked((uint)global::System.Char.ConvertToUtf32(HighSurrogate, LowSurrogate));
	}
		public static uint ConvertToUtf32(string S, int Index)
	{
		if (S == null) throw new global::System.ArgumentNullException(nameof(S));
		return unchecked((uint)global::System.Char.ConvertToUtf32(S, Index));
	}
		public static double GetNumericValue(char C)
	{
		return global::System.Globalization.CharUnicodeInfo.GetNumericValue(C);
	}
		public static double GetNumericValue(uint C)
	{
		string s = ScalarToString(C);
		return global::System.Globalization.CharUnicodeInfo.GetNumericValue(s, 0);
	}
		public static double GetNumericValue(string S, int Index)
	{
		if (S == null) throw new global::System.ArgumentNullException(nameof(S));
		return global::System.Globalization.CharUnicodeInfo.GetNumericValue(S, Index);
	}
		public static TUnicodeCategory GetUnicodeCategory(char C)
	{
		return MapCategory(global::System.Char.GetUnicodeCategory(C));
	}
		public static TUnicodeCategory GetUnicodeCategory(uint C)
	{
		return MapCategory(GetCategory(C));
	}
		public static TUnicodeCategory GetUnicodeCategory(string S, int Index)
	{
		if (S == null) throw new global::System.ArgumentNullException(nameof(S));
		return MapCategory(global::System.Globalization.CharUnicodeInfo.GetUnicodeCategory(S, Index));
	}
		public static bool IsControl(char C)
	{
		return global::System.Char.IsControl(C);
	}
		public static bool IsControl(uint C)
	{
		return IsCategory(C, global::System.Globalization.UnicodeCategory.Control);
	}
		public static bool IsControl(string S, int Index)
	{
		if (S == null) throw new global::System.ArgumentNullException(nameof(S));
		return global::System.Char.IsControl(S, Index);
	}
		public static bool IsDefined(char C)
	{
		return global::System.Char.GetUnicodeCategory(C) != global::System.Globalization.UnicodeCategory.OtherNotAssigned;
	}
		public static bool IsDefined(uint C)
	{
		return GetCategory(C) != global::System.Globalization.UnicodeCategory.OtherNotAssigned;
	}
		public static bool IsDefined(string S, int Index)
	{
		if (S == null) throw new global::System.ArgumentNullException(nameof(S));
		return global::System.Globalization.CharUnicodeInfo.GetUnicodeCategory(S, Index) != global::System.Globalization.UnicodeCategory.OtherNotAssigned;
	}
		public static bool IsDigit(char C)
	{
		return global::System.Char.IsDigit(C);
	}
		public static bool IsDigit(uint C)
	{
		return IsCategory(C, global::System.Globalization.UnicodeCategory.DecimalDigitNumber);
	}
		public static bool IsDigit(string S, int Index)
	{
		if (S == null) throw new global::System.ArgumentNullException(nameof(S));
		return global::System.Char.IsDigit(S, Index);
	}
		public static bool IsHighSurrogate(char C)
	{
		return global::System.Char.IsHighSurrogate(C);
	}
		public static bool IsHighSurrogate(string S, int Index)
	{
		if (S == null) throw new global::System.ArgumentNullException(nameof(S));
		return global::System.Char.IsHighSurrogate(S, Index);
	}
		public static bool IsLetter(char C)
	{
		return global::System.Char.IsLetter(C);
	}
		public static bool IsLetter(uint C)
	{
		return IsCategory(C, global::System.Globalization.UnicodeCategory.UppercaseLetter, global::System.Globalization.UnicodeCategory.LowercaseLetter, global::System.Globalization.UnicodeCategory.TitlecaseLetter, global::System.Globalization.UnicodeCategory.ModifierLetter, global::System.Globalization.UnicodeCategory.OtherLetter);
	}
		public static bool IsLetter(string S, int Index)
	{
		if (S == null) throw new global::System.ArgumentNullException(nameof(S));
		return global::System.Char.IsLetter(S, Index);
	}
		public static bool IsLetterOrDigit(char C)
	{
		return global::System.Char.IsLetterOrDigit(C);
	}
		public static bool IsLetterOrDigit(uint C)
	{
		return IsLetter(C) || IsDigit(C);
	}
		public static bool IsLetterOrDigit(string S, int Index)
	{
		if (S == null) throw new global::System.ArgumentNullException(nameof(S));
		return global::System.Char.IsLetterOrDigit(S, Index);
	}
		public static bool IsLowSurrogate(char C)
	{
		return global::System.Char.IsLowSurrogate(C);
	}
		public static bool IsLowSurrogate(string S, int Index)
	{
		if (S == null) throw new global::System.ArgumentNullException(nameof(S));
		return global::System.Char.IsLowSurrogate(S, Index);
	}
		public static bool IsLower(char C)
	{
		return global::System.Char.IsLower(C);
	}
		public static bool IsLower(uint C)
	{
		return IsCategory(C, global::System.Globalization.UnicodeCategory.LowercaseLetter);
	}
		public static bool IsLower(string S, int Index)
	{
		if (S == null) throw new global::System.ArgumentNullException(nameof(S));
		return global::System.Char.IsLower(S, Index);
	}
		public static bool IsNumber(char C)
	{
		return global::System.Char.IsNumber(C);
	}
		public static bool IsNumber(uint C)
	{
		return IsCategory(C, global::System.Globalization.UnicodeCategory.DecimalDigitNumber, global::System.Globalization.UnicodeCategory.LetterNumber, global::System.Globalization.UnicodeCategory.OtherNumber);
	}
		public static bool IsNumber(string S, int Index)
	{
		if (S == null) throw new global::System.ArgumentNullException(nameof(S));
		return global::System.Char.IsNumber(S, Index);
	}
		public static bool IsPunctuation(char C)
	{
		return global::System.Char.IsPunctuation(C);
	}
		public static bool IsPunctuation(uint C)
	{
		return IsCategory(C, global::System.Globalization.UnicodeCategory.ConnectorPunctuation, global::System.Globalization.UnicodeCategory.DashPunctuation, global::System.Globalization.UnicodeCategory.OpenPunctuation, global::System.Globalization.UnicodeCategory.ClosePunctuation, global::System.Globalization.UnicodeCategory.InitialQuotePunctuation, global::System.Globalization.UnicodeCategory.FinalQuotePunctuation, global::System.Globalization.UnicodeCategory.OtherPunctuation);
	}
		public static bool IsPunctuation(string S, int Index)
	{
		if (S == null) throw new global::System.ArgumentNullException(nameof(S));
		return global::System.Char.IsPunctuation(S, Index);
	}
		public static bool IsSeparator(char C)
	{
		return global::System.Char.IsSeparator(C);
	}
		public static bool IsSeparator(uint C)
	{
		return IsCategory(C, global::System.Globalization.UnicodeCategory.SpaceSeparator, global::System.Globalization.UnicodeCategory.LineSeparator, global::System.Globalization.UnicodeCategory.ParagraphSeparator);
	}
		public static bool IsSeparator(string S, int Index)
	{
		if (S == null) throw new global::System.ArgumentNullException(nameof(S));
		return global::System.Char.IsSeparator(S, Index);
	}
		public static bool IsSurrogate(char C)
	{
		return global::System.Char.IsSurrogate(C);
	}
		public static bool IsSurrogate(string S, int Index)
	{
		if (S == null) throw new global::System.ArgumentNullException(nameof(S));
		return global::System.Char.IsSurrogate(S, Index);
	}
		public static bool IsSurrogatePair(char HighSurrogate, char LowSurrogate)
	{
		return global::System.Char.IsSurrogatePair(HighSurrogate, LowSurrogate);
	}
		public static bool IsSurrogatePair(string S, int Index)
	{
		if (S == null) throw new global::System.ArgumentNullException(nameof(S));
		return global::System.Char.IsSurrogatePair(S, Index);
	}
		public static bool IsSymbol(char C)
	{
		return global::System.Char.IsSymbol(C);
	}
		public static bool IsSymbol(uint C)
	{
		return IsCategory(C, global::System.Globalization.UnicodeCategory.MathSymbol, global::System.Globalization.UnicodeCategory.CurrencySymbol, global::System.Globalization.UnicodeCategory.ModifierSymbol, global::System.Globalization.UnicodeCategory.OtherSymbol);
	}
		public static bool IsSymbol(string S, int Index)
	{
		if (S == null) throw new global::System.ArgumentNullException(nameof(S));
		return global::System.Char.IsSymbol(S, Index);
	}
		public static bool IsUpper(char C)
	{
		return global::System.Char.IsUpper(C);
	}
		public static bool IsUpper(uint C)
	{
		return IsCategory(C, global::System.Globalization.UnicodeCategory.UppercaseLetter);
	}
		public static bool IsUpper(string S, int Index)
	{
		if (S == null) throw new global::System.ArgumentNullException(nameof(S));
		return global::System.Char.IsUpper(S, Index);
	}
		public static bool IsWhiteSpace(char C)
	{
		return global::System.Char.IsWhiteSpace(C);
	}
		public static bool IsWhiteSpace(uint C)
	{
		string s = ScalarToString(C);
		return global::System.Char.IsWhiteSpace(s, 0);
	}
		public static bool IsWhiteSpace(string S, int Index)
	{
		if (S == null) throw new global::System.ArgumentNullException(nameof(S));
		return global::System.Char.IsWhiteSpace(S, Index);
	}
		public static char ToLower(char C)
	{
		return global::System.Char.ToLower(C, global::System.Globalization.CultureInfo.CurrentCulture);
	}
		public static uint ToLower(uint C)
	{
		return TransformScalar(C, false, false);
	}
		public static string ToLower(string S)
	{
		if (S == null) throw new global::System.ArgumentNullException(nameof(S));
		return S.ToLower(global::System.Globalization.CultureInfo.CurrentCulture);
	}
		public static char ToLowerInvariant(char C)
	{
		return global::System.Char.ToLowerInvariant(C);
	}
		public static uint ToLowerInvariant(uint C)
	{
		return TransformScalar(C, false, true);
	}
		public static char ToUpper(char C)
	{
		return global::System.Char.ToUpper(C, global::System.Globalization.CultureInfo.CurrentCulture);
	}
		public static uint ToUpper(uint C)
	{
		return TransformScalar(C, true, false);
	}
		public static string ToUpper(string S)
	{
		if (S == null) throw new global::System.ArgumentNullException(nameof(S));
		return S.ToUpper(global::System.Globalization.CultureInfo.CurrentCulture);
	}
		public static char ToUpperInvariant(char C)
	{
		return global::System.Char.ToUpperInvariant(C);
	}
		public static uint ToUpperInvariant(uint C)
	{
		return TransformScalar(C, true, true);
	}
		public static TCharacter CreateRecord(){return new TCharacter();}
	}	

/* Global compatibility routines */
  /*
    System.UCS4Char is a 32-bit Unicode scalar/code-point representation.
    It is repeated here as a translation-compatible fallback until the
    System MockRTL exposes UCS4Char directly.
  */
	public enum TUnicodeCategory {ucControl,
                       ucFormat,
                       ucUnassigned,
                       ucPrivateUse,
                       ucSurrogate,
                       ucLowercaseLetter,
                       ucModifierLetter,
                       ucOtherLetter,
                       ucTitlecaseLetter,
                       ucUppercaseLetter,
                       ucCombiningMark,
                       ucEnclosingMark,
                       ucNonSpacingMark,
                       ucDecimalNumber,
                       ucLetterNumber,
                       ucOtherNumber,
                       ucConnectPunctuation,
                       ucDashPunctuation,
                       ucClosePunctuation,
                       ucFinalPunctuation,
                       ucInitialPunctuation,
                       ucOtherPunctuation,
                       ucOpenPunctuation,
                       ucCurrencySymbol,
                       ucModifierSymbol,
                       ucMathSymbol,
                       ucOtherSymbol,
                       ucLineSeparator,
                       ucParagraphSeparator,
                       ucSpaceSeparator };


/* Global compatibility routines */
	public static string ConvertFromUtf32(uint ConvertChar)
	{
		return ScalarToString(ConvertChar);
	}
	public static uint ConvertToUtf32(char HighSurrogate, char LowSurrogate)
	{
		return unchecked((uint)global::System.Char.ConvertToUtf32(HighSurrogate, LowSurrogate));
	}
	public static uint ConvertToUtf32(string S, int Index)
	{
		if (S == null) throw new global::System.ArgumentNullException(nameof(S));
		return unchecked((uint)global::System.Char.ConvertToUtf32(S, Index));
	}
	public static double GetNumericValue(char C)
	{
		return global::System.Globalization.CharUnicodeInfo.GetNumericValue(C);
	}
	public static double GetNumericValue(uint C)
	{
		string s = ScalarToString(C);
		return global::System.Globalization.CharUnicodeInfo.GetNumericValue(s, 0);
	}
	public static double GetNumericValue(string S, int Index)
	{
		if (S == null) throw new global::System.ArgumentNullException(nameof(S));
		return global::System.Globalization.CharUnicodeInfo.GetNumericValue(S, Index);
	}
	public static TUnicodeCategory GetUnicodeCategory(char C)
	{
		return MapCategory(global::System.Char.GetUnicodeCategory(C));
	}
	public static TUnicodeCategory GetUnicodeCategory(uint C)
	{
		return MapCategory(GetCategory(C));
	}
	public static TUnicodeCategory GetUnicodeCategory(string S, int Index)
	{
		if (S == null) throw new global::System.ArgumentNullException(nameof(S));
		return MapCategory(global::System.Globalization.CharUnicodeInfo.GetUnicodeCategory(S, Index));
	}
	public static bool IsControl(char C)
	{
		return global::System.Char.IsControl(C);
	}
	public static bool IsControl(uint C)
	{
		return IsCategory(C, global::System.Globalization.UnicodeCategory.Control);
	}
	public static bool IsControl(string S, int Index)
	{
		if (S == null) throw new global::System.ArgumentNullException(nameof(S));
		return global::System.Char.IsControl(S, Index);
	}
	public static bool IsDefined(char C)
	{
		return global::System.Char.GetUnicodeCategory(C) != global::System.Globalization.UnicodeCategory.OtherNotAssigned;
	}
	public static bool IsDefined(uint C)
	{
		return GetCategory(C) != global::System.Globalization.UnicodeCategory.OtherNotAssigned;
	}
	public static bool IsDefined(string S, int Index)
	{
		if (S == null) throw new global::System.ArgumentNullException(nameof(S));
		return global::System.Globalization.CharUnicodeInfo.GetUnicodeCategory(S, Index) != global::System.Globalization.UnicodeCategory.OtherNotAssigned;
	}
	public static bool IsDigit(char C)
	{
		return global::System.Char.IsDigit(C);
	}
	public static bool IsDigit(uint C)
	{
		return IsCategory(C, global::System.Globalization.UnicodeCategory.DecimalDigitNumber);
	}
	public static bool IsDigit(string S, int Index)
	{
		if (S == null) throw new global::System.ArgumentNullException(nameof(S));
		return global::System.Char.IsDigit(S, Index);
	}
	public static bool IsLetter(char C)
	{
		return global::System.Char.IsLetter(C);
	}
	public static bool IsLetter(uint C)
	{
		return IsCategory(C, global::System.Globalization.UnicodeCategory.UppercaseLetter, global::System.Globalization.UnicodeCategory.LowercaseLetter, global::System.Globalization.UnicodeCategory.TitlecaseLetter, global::System.Globalization.UnicodeCategory.ModifierLetter, global::System.Globalization.UnicodeCategory.OtherLetter);
	}
	public static bool IsLetter(string S, int Index)
	{
		if (S == null) throw new global::System.ArgumentNullException(nameof(S));
		return global::System.Char.IsLetter(S, Index);
	}
	public static bool IsLetterOrDigit(char C)
	{
		return global::System.Char.IsLetterOrDigit(C);
	}
	public static bool IsLetterOrDigit(uint C)
	{
		return IsLetter(C) || IsDigit(C);
	}
	public static bool IsLetterOrDigit(string S, int Index)
	{
		if (S == null) throw new global::System.ArgumentNullException(nameof(S));
		return global::System.Char.IsLetterOrDigit(S, Index);
	}
	public static bool IsLower(char C)
	{
		return global::System.Char.IsLower(C);
	}
	public static bool IsLower(uint C)
	{
		return IsCategory(C, global::System.Globalization.UnicodeCategory.LowercaseLetter);
	}
	public static bool IsLower(string S, int Index)
	{
		if (S == null) throw new global::System.ArgumentNullException(nameof(S));
		return global::System.Char.IsLower(S, Index);
	}
	public static bool IsNumber(char C)
	{
		return global::System.Char.IsNumber(C);
	}
	public static bool IsNumber(uint C)
	{
		return IsCategory(C, global::System.Globalization.UnicodeCategory.DecimalDigitNumber, global::System.Globalization.UnicodeCategory.LetterNumber, global::System.Globalization.UnicodeCategory.OtherNumber);
	}
	public static bool IsNumber(string S, int Index)
	{
		if (S == null) throw new global::System.ArgumentNullException(nameof(S));
		return global::System.Char.IsNumber(S, Index);
	}
	public static bool IsPunctuation(char C)
	{
		return global::System.Char.IsPunctuation(C);
	}
	public static bool IsPunctuation(uint C)
	{
		return IsCategory(C, global::System.Globalization.UnicodeCategory.ConnectorPunctuation, global::System.Globalization.UnicodeCategory.DashPunctuation, global::System.Globalization.UnicodeCategory.OpenPunctuation, global::System.Globalization.UnicodeCategory.ClosePunctuation, global::System.Globalization.UnicodeCategory.InitialQuotePunctuation, global::System.Globalization.UnicodeCategory.FinalQuotePunctuation, global::System.Globalization.UnicodeCategory.OtherPunctuation);
	}
	public static bool IsPunctuation(string S, int Index)
	{
		if (S == null) throw new global::System.ArgumentNullException(nameof(S));
		return global::System.Char.IsPunctuation(S, Index);
	}
	public static bool IsSeparator(char C)
	{
		return global::System.Char.IsSeparator(C);
	}
	public static bool IsSeparator(uint C)
	{
		return IsCategory(C, global::System.Globalization.UnicodeCategory.SpaceSeparator, global::System.Globalization.UnicodeCategory.LineSeparator, global::System.Globalization.UnicodeCategory.ParagraphSeparator);
	}
	public static bool IsSeparator(string S, int Index)
	{
		if (S == null) throw new global::System.ArgumentNullException(nameof(S));
		return global::System.Char.IsSeparator(S, Index);
	}
	public static bool IsSymbol(char C)
	{
		return global::System.Char.IsSymbol(C);
	}
	public static bool IsSymbol(uint C)
	{
		return IsCategory(C, global::System.Globalization.UnicodeCategory.MathSymbol, global::System.Globalization.UnicodeCategory.CurrencySymbol, global::System.Globalization.UnicodeCategory.ModifierSymbol, global::System.Globalization.UnicodeCategory.OtherSymbol);
	}
	public static bool IsSymbol(string S, int Index)
	{
		if (S == null) throw new global::System.ArgumentNullException(nameof(S));
		return global::System.Char.IsSymbol(S, Index);
	}
	public static bool IsUpper(char C)
	{
		return global::System.Char.IsUpper(C);
	}
	public static bool IsUpper(uint C)
	{
		return IsCategory(C, global::System.Globalization.UnicodeCategory.UppercaseLetter);
	}
	public static bool IsUpper(string S, int Index)
	{
		if (S == null) throw new global::System.ArgumentNullException(nameof(S));
		return global::System.Char.IsUpper(S, Index);
	}
	public static bool IsWhiteSpace(char C)
	{
		return global::System.Char.IsWhiteSpace(C);
	}
	public static bool IsWhiteSpace(uint C)
	{
		string s = ScalarToString(C);
		return global::System.Char.IsWhiteSpace(s, 0);
	}
	public static bool IsWhiteSpace(string S, int Index)
	{
		if (S == null) throw new global::System.ArgumentNullException(nameof(S));
		return global::System.Char.IsWhiteSpace(S, Index);
	}
	public static bool IsHighSurrogate(char C)
	{
		return global::System.Char.IsHighSurrogate(C);
	}
	public static bool IsHighSurrogate(string S, int Index)
	{
		if (S == null) throw new global::System.ArgumentNullException(nameof(S));
		return global::System.Char.IsHighSurrogate(S, Index);
	}
	public static bool IsLowSurrogate(char C)
	{
		return global::System.Char.IsLowSurrogate(C);
	}
	public static bool IsLowSurrogate(string S, int Index)
	{
		if (S == null) throw new global::System.ArgumentNullException(nameof(S));
		return global::System.Char.IsLowSurrogate(S, Index);
	}
	public static bool IsSurrogate(char C)
	{
		return global::System.Char.IsSurrogate(C);
	}
	public static bool IsSurrogate(string S, int Index)
	{
		if (S == null) throw new global::System.ArgumentNullException(nameof(S));
		return global::System.Char.IsSurrogate(S, Index);
	}
	public static bool IsSurrogatePair(char HighSurrogate, char LowSurrogate)
	{
		return global::System.Char.IsSurrogatePair(HighSurrogate, LowSurrogate);
	}
	public static bool IsSurrogatePair(string S, int Index)
	{
		if (S == null) throw new global::System.ArgumentNullException(nameof(S));
		return global::System.Char.IsSurrogatePair(S, Index);
	}
	public static char ToLower(char C)
	{
		return global::System.Char.ToLower(C, global::System.Globalization.CultureInfo.CurrentCulture);
	}
	public static uint ToLower(uint C)
	{
		return TransformScalar(C, false, false);
	}
	public static string ToLower(string S)
	{
		if (S == null) throw new global::System.ArgumentNullException(nameof(S));
		return S.ToLower(global::System.Globalization.CultureInfo.CurrentCulture);
	}
	public static char ToLowerInvariant(char C)
	{
		return global::System.Char.ToLowerInvariant(C);
	}
	public static uint ToLowerInvariant(uint C)
	{
		return TransformScalar(C, false, true);
	}
	public static char ToUpper(char C)
	{
		return global::System.Char.ToUpper(C, global::System.Globalization.CultureInfo.CurrentCulture);
	}
	public static uint ToUpper(uint C)
	{
		return TransformScalar(C, true, false);
	}
	public static string ToUpper(string S)
	{
		if (S == null) throw new global::System.ArgumentNullException(nameof(S));
		return S.ToUpper(global::System.Globalization.CultureInfo.CurrentCulture);
	}
	public static char ToUpperInvariant(char C)
	{
		return global::System.Char.ToUpperInvariant(C);
	}
	public static uint ToUpperInvariant(uint C)
	{
		return TransformScalar(C, true, true);
	}
	
} // class CharacterInterface


file class CharacterImplementation
{

} // class CharacterImplementation

}  // namespace Character

}  // namespace System

