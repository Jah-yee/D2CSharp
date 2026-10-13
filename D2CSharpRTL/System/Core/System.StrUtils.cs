using System;
using System.Collections.Generic;
using System.Text;
using static System.SystemInterface;

namespace System
{
namespace Strutils
{

/*
  D2CSharp Runtime Library (RTL)

  C# runtime implementation corresponding to the declaration-oriented
  System.StrUtils MockRTL unit.

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

public static partial class StrutilsInterface
{
    public enum TStringSeachOption
    {
        soDown,
        soMatchCase,
        soWholeWord
    }

    public delegate bool TCompareTextProc(string AText, string AOther);

    public static TCompareTextProc AnsiResemblesProc = SoundexProc;
    public static TCompareTextProc ResemblesProc = SoundexProc;

    // Kept for API compatibility. SearchBuf currently cannot consume the
    // managed TSet until the D2CSharp pointer/set runtime contract is fixed.
    public static TSet WordDelimiters = CreateDefaultWordDelimiters();

    private static TSet CreateDefaultWordDelimiters()
    {
        TSet result = new TSet();
        result.IncludeRange(0, 255);
        result.ExcludeRange('0', '9');
        result.ExcludeRange('A', 'Z');
        result.ExcludeRange('a', 'z');
        return result;
    }

    private static string Nz(string value) => value ?? string.Empty;

    public static string LeftStr(string AText, int ACount)
    {
        string s = Nz(AText);
        if (ACount <= 0) return string.Empty;
        return ACount >= s.Length ? s : s.Substring(0, ACount);
    }

    public static string RightStr(string AText, int ACount)
    {
        string s = Nz(AText);
        if (ACount <= 0) return string.Empty;
        return ACount >= s.Length ? s : s.Substring(s.Length - ACount);
    }

    public static string MidStr(string AText, int AStart, int ACount)
    {
        string s = Nz(AText);
        if (ACount <= 0 || AStart > s.Length) return string.Empty;
        int start = global::System.Math.Max(1, AStart) - 1;
        if (start >= s.Length) return string.Empty;
        int count = global::System.Math.Min(ACount, s.Length - start);
        return s.Substring(start, count);
    }

    // Unicode RTL: byte-oriented deprecated variants are equivalent to their
    // character-oriented counterparts for the managed Unicode representation.
    public static string AnsiLeftStr(string AText, int ACount) => LeftStr(AText, ACount);
    public static string AnsiRightStr(string AText, int ACount) => RightStr(AText, ACount);
    public static string AnsiMidStr(string AText, int AStart, int ACount) => MidStr(AText, AStart, ACount);
    public static string LeftBStr(string AText, int AByteCount) => LeftStr(AText, AByteCount);
    public static string RightBStr(string AText, int AByteCount) => RightStr(AText, AByteCount);
    public static string MidBStr(string AText, int AByteStart, int AByteCount) => MidStr(AText, AByteStart, AByteCount);

    public static bool StartsStr(string ASubText, string AText) =>
        Nz(AText).StartsWith(Nz(ASubText), StringComparison.Ordinal);

    public static bool StartsText(string ASubText, string AText) =>
        Nz(AText).StartsWith(Nz(ASubText), StringComparison.OrdinalIgnoreCase);

    public static bool EndsStr(string ASubText, string AText) =>
        Nz(AText).EndsWith(Nz(ASubText), StringComparison.Ordinal);

    public static bool EndsText(string ASubText, string AText) =>
        Nz(AText).EndsWith(Nz(ASubText), StringComparison.OrdinalIgnoreCase);

    public static bool ContainsStr(string AText, string ASubText) =>
        Nz(AText).IndexOf(Nz(ASubText), StringComparison.Ordinal) >= 0;

    public static bool ContainsText(string AText, string ASubText) =>
        Nz(AText).IndexOf(Nz(ASubText), StringComparison.OrdinalIgnoreCase) >= 0;

    public static bool AnsiStartsStr(string ASubText, string AText) => StartsStr(ASubText, AText);
    public static bool AnsiStartsText(string ASubText, string AText) => StartsText(ASubText, AText);
    public static bool AnsiEndsStr(string ASubText, string AText) => EndsStr(ASubText, AText);
    public static bool AnsiEndsText(string ASubText, string AText) => EndsText(ASubText, AText);
    public static bool AnsiContainsStr(string AText, string ASubText) => ContainsStr(AText, ASubText);
    public static bool AnsiContainsText(string AText, string ASubText) => ContainsText(AText, ASubText);

    private static int IndexOfValue(string AText, string[] AValues, StringComparison comparison)
    {
        if (AValues == null) return -1;
        string text = Nz(AText);
        for (int i = 0; i < AValues.Length; i++)
            if (string.Equals(text, Nz(AValues[i]), comparison))
                return i;
        return -1;
    }

    public static bool MatchStr(string AText, string[] AValues) => IndexStr(AText, AValues) >= 0;
    public static bool MatchText(string AText, string[] AValues) => IndexText(AText, AValues) >= 0;
    public static int IndexStr(string AText, string[] AValues) => IndexOfValue(AText, AValues, StringComparison.Ordinal);
    public static int IndexText(string AText, string[] AValues) => IndexOfValue(AText, AValues, StringComparison.OrdinalIgnoreCase);
    public static bool AnsiMatchStr(string AText, string[] AValues) => MatchStr(AText, AValues);
    public static bool AnsiMatchText(string AText, string[] AValues) => MatchText(AText, AValues);
    public static int AnsiIndexStr(string AText, string[] AValues) => IndexStr(AText, AValues);
    public static int AnsiIndexText(string AText, string[] AValues) => IndexText(AText, AValues);

    private static string ReplaceAll(string text, string from, string to, StringComparison comparison)
    {
        text = Nz(text);
        from = Nz(from);
        to = Nz(to);
        if (from.Length == 0) return text;

        StringBuilder result = new StringBuilder(text.Length);
        int start = 0;
        while (start < text.Length)
        {
            int p = text.IndexOf(from, start, comparison);
            if (p < 0)
            {
                result.Append(text, start, text.Length - start);
                break;
            }
            result.Append(text, start, p - start);
            result.Append(to);
            start = p + from.Length;
        }
        return result.ToString();
    }

    public static string ReplaceStr(string AText, string AFromText, string AToText) =>
        ReplaceAll(AText, AFromText, AToText, StringComparison.Ordinal);

    public static string ReplaceText(string AText, string AFromText, string AToText) =>
        ReplaceAll(AText, AFromText, AToText, StringComparison.OrdinalIgnoreCase);

    public static string ReverseString(string AText)
    {
        string s = Nz(AText);
        char[] chars = s.ToCharArray();
        Array.Reverse(chars);
        return new string(chars);
    }

    public static string AnsiReplaceStr(string AText, string AFromText, string AToText) => ReplaceStr(AText, AFromText, AToText);
    public static string AnsiReplaceText(string AText, string AFromText, string AToText) => ReplaceText(AText, AFromText, AToText);
    public static string AnsiReverseString(string AText) => ReverseString(AText);

    public static int PosEx(string SubStr, string S, int Offset = 1)
    {
        string text = Nz(S);
        string sub = Nz(SubStr);
        if (Offset < 1) Offset = 1;
        if (Offset > text.Length + 1) return 0;
        int p = text.IndexOf(sub, Offset - 1, StringComparison.Ordinal);
        return p < 0 ? 0 : p + 1;
    }

    public static PAnsiChar SearchBuf(PAnsiChar Buf, int BufLen, int SelStart, int SelLength, AnsiString SearchString, TSet Options)
    {
        if (Buf.IsNull() || SearchString is null || BufLen <= 0)
            return default;

        int searchLength = SearchString.Length;
        if (searchLength == 0 || searchLength > BufLen)
            return default;

        bool down = Options != null && Options.Contains((int)TStringSeachOption.soDown);
        bool matchCase = Options != null && Options.Contains((int)TStringSeachOption.soMatchCase);
        bool wholeWord = Options != null && Options.Contains((int)TStringSeachOption.soWholeWord);

        int start;
        int finish;

        if (down)
        {
            start = SelStart + SelLength;
            if (start < 0)
                start = 0;
            if (start > BufLen - searchLength)
                return default;
            finish = BufLen - searchLength;
        }
        else
        {
            start = SelStart;
            if (start > BufLen - searchLength)
                start = BufLen - searchLength;
            finish = 0;
            if (start < finish)
                return default;
        }

        bool IsDelimiter(int index)
        {
            if (index < 0 || index >= BufLen)
                return true;

            byte value = Buf[index];
            return WordDelimiters != null && WordDelimiters.Contains(value);
        }

        bool MatchesAt(int index)
        {
            for (int i = 0; i < searchLength; i++)
            {
                byte left = Buf[index + i];
                byte right = SearchString[i];

                if (matchCase)
                {
                    if (left != right)
                        return false;
                }
                else
                {
                    char leftChar = char.ToUpperInvariant((char)left);
                    char rightChar = char.ToUpperInvariant((char)right);
                    if (leftChar != rightChar)
                        return false;
                }
            }

            if (wholeWord)
            {
                if (!IsDelimiter(index - 1))
                    return false;
                if (!IsDelimiter(index + searchLength))
                    return false;
            }

            return true;
        }

        if (down)
        {
            for (int index = start; index <= finish; index++)
                if (MatchesAt(index))
                    return Buf + index;
        }
        else
        {
            for (int index = start; index >= finish; index--)
                if (MatchesAt(index))
                    return Buf + index;
        }

        return default;
    }

    public static PChar SearchBuf(PChar Buf, int BufLen, int SelStart, int SelLength, string SearchString, TSet Options)
    {
        if (Buf.IsNull() || SearchString == null || BufLen <= 0)
            return default;

        int searchLength = SearchString.Length;
        if (searchLength == 0 || searchLength > BufLen)
            return default;

        bool down = Options != null && Options.Contains((int)TStringSeachOption.soDown);
        bool matchCase = Options != null && Options.Contains((int)TStringSeachOption.soMatchCase);
        bool wholeWord = Options != null && Options.Contains((int)TStringSeachOption.soWholeWord);

        int start;
        int finish;

        if (down)
        {
            start = SelStart + SelLength;
            if (start < 0)
                start = 0;
            if (start > BufLen - searchLength)
                return default;
            finish = BufLen - searchLength;
        }
        else
        {
            start = SelStart;
            if (start > BufLen - searchLength)
                start = BufLen - searchLength;
            finish = 0;
            if (start < finish)
                return default;
        }

        bool IsDelimiter(int index)
        {
            if (index < 0 || index >= BufLen)
                return true;

            char value = Buf[index];
            if (value > byte.MaxValue)
                return false;

            return WordDelimiters != null && WordDelimiters.Contains(value);
        }

        bool MatchesAt(int index)
        {
            for (int i = 0; i < searchLength; i++)
            {
                char left = Buf[index + i];
                char right = SearchString[i];

                if (matchCase)
                {
                    if (left != right)
                        return false;
                }
                else if (char.ToUpperInvariant(left) != char.ToUpperInvariant(right))
                    return false;
            }

            if (wholeWord)
            {
                if (!IsDelimiter(index - 1))
                    return false;
                if (!IsDelimiter(index + searchLength))
                    return false;
            }

            return true;
        }

        if (down)
        {
            for (int index = start; index <= finish; index++)
                if (MatchesAt(index))
                    return Buf + index;
        }
        else
        {
            for (int index = start; index >= finish; index--)
                if (MatchesAt(index))
                    return Buf + index;
        }

        return default;
    }

    public static string DupeString(string AText, int ACount)
    {
        if (ACount <= 0) return string.Empty;
        string s = Nz(AText);
        if (s.Length == 0) return string.Empty;
        StringBuilder result = new StringBuilder(checked(s.Length * ACount));
        for (int i = 0; i < ACount; i++) result.Append(s);
        return result.ToString();
    }

    public static string StuffString(string AText, uint AStart, uint ALength, string ASubText)
    {
        string text = Nz(AText);
        string sub = Nz(ASubText);
        int start = AStart == 0 ? 0 : checked((int)AStart - 1);
        if (start < 0) start = 0;
        if (start > text.Length) start = text.Length;
        int length = global::System.Math.Min(checked((int)ALength), text.Length - start);
        return text.Substring(0, start) + sub + text.Substring(start + length);
    }

    public static string IfThen(bool AValue, string ATrue, string AFalse = "") =>
        AValue ? Nz(ATrue) : Nz(AFalse);

    public static string[] SplitString(string S, string Delimiters)
    {
        string text = Nz(S);
        string delimiters = Nz(Delimiters);
        if (delimiters.Length == 0) return new[] { text };
        return text.Split(delimiters.ToCharArray(), StringSplitOptions.None);
    }

    private static char SoundexDigit(char c)
    {
        switch (char.ToUpperInvariant(c))
        {
            case 'B': case 'F': case 'P': case 'V': return '1';
            case 'C': case 'G': case 'J': case 'K': case 'Q': case 'S': case 'X': case 'Z': return '2';
            case 'D': case 'T': return '3';
            case 'L': return '4';
            case 'M': case 'N': return '5';
            case 'R': return '6';
            default: return '0';
        }
    }

    public static string Soundex(string AText, int /*1..MaxInt*/ ALength = 4)
    {
        if (ALength <= 0) return string.Empty;
        string text = Nz(AText);
        StringBuilder result = new StringBuilder(ALength);
        char previous = '0';

        foreach (char c in text)
        {
            char digit = SoundexDigit(c);
            if (digit == '0')
            {
                previous = '0';
                continue;
            }

            if (digit != previous)
            {
                result.Append(digit);
                if (result.Length >= ALength) break;
            }
            previous = digit;
        }

        return result.ToString();
    }

    private static int EncodeSoundex(string code, int maxLength)
    {
        int length = global::System.Math.Min(global::System.Math.Min(code.Length, maxLength), 8);
        uint value = 0;
        for (int i = 0; i < length; i++)
        {
            uint digit = code[i] >= '0' && code[i] <= '6' ? (uint)(code[i] - '0') : 0U;
            value |= digit << (i * 4);
        }
        return unchecked((int)value);
    }

    private static string DecodeSoundex(uint value, int maxLength)
    {
        StringBuilder result = new StringBuilder(maxLength);
        for (int i = 0; i < maxLength; i++)
        {
            int digit = (int)((value >> (i * 4)) & 0xFU);
            if (digit == 0) break;
            if (digit > 6) break;
            result.Append((char)('0' + digit));
        }
        return result.ToString();
    }

    public static int SoundexInt(string AText, int /*1..8*/ ALength = 4)
    {
        int length = global::System.Math.Max(1, global::System.Math.Min(8, ALength));
        return EncodeSoundex(Soundex(AText, length), length);
    }

    public static ushort SoundexWord(string AText) =>
        unchecked((ushort)EncodeSoundex(Soundex(AText, 4), 4));

    public static string DecodeSoundexInt(int AValue) =>
        DecodeSoundex(unchecked((uint)AValue), 8);

    public static string DecodeSoundexWord(ushort AValue) =>
        DecodeSoundex(AValue, 4);

    public static bool SoundexSimilar(string AText, string AOther, int /*1..MaxInt*/ ALength = 4) =>
        string.Equals(Soundex(AText, ALength), Soundex(AOther, ALength), StringComparison.Ordinal);

    public static int SoundexCompare(string AText, string AOther, int /*1..MaxInt*/ ALength = 4) =>
        string.Compare(Soundex(AText, ALength), Soundex(AOther, ALength), StringComparison.Ordinal);

    public static bool SoundexProc(string AText, string AOther) =>
        SoundexSimilar(AText, AOther, 4);

    public static bool ResemblesText(string AText, string AOther)
    {
        TCompareTextProc proc = ResemblesProc ?? SoundexProc;
        return proc(AText, AOther);
    }

    public static bool AnsiResemblesText(string AText, string AOther)
    {
        TCompareTextProc proc = AnsiResemblesProc ?? ResemblesProc ?? SoundexProc;
        return proc(AText, AOther);
    }
}

file static class StrutilsImplementation
{
}

} // namespace Strutils
} // namespace System
