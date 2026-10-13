using System.Sysutils;
using System.Types;
using System;
using static System.Dmath.DmathImplementation;
using static System.Dmath.DmathInterface;
using static System.SystemInterface;
using static System.Sysutils.SysutilsInterface;
using static System.Types.TypesInterface;

namespace System
{

namespace Dmath
{

/*
  D2CSharp Runtime Library (RTL)

  This Delphi source file provides a declaration-oriented mock of selected
  System.Math functionality required by D2CSharp.

  The file serves two purposes:
  - it provides symbols and routine signatures used while translating Delphi
    source code;
  - it provides the basis for generating the corresponding C# mock file.

  The routines below intentionally contain no RTL implementation. Functions
  return simple default values and procedures have empty bodies.

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


public class DmathInterface
{

	public class EInvalidArgument : DException
	{


		public EInvalidArgument(string Msg) : base(Msg) {}
		public EInvalidArgument(string Msg, params TVarRec[] Args) : base(Msg, Args) {}
		public EInvalidArgument(string Msg, TVarRec[] Args, int AHelpContext) : base(Msg, Args, AHelpContext) {}
		public EInvalidArgument(string Msg, int AHelpContext) : base(Msg, AHelpContext) {}
		public EInvalidArgument(uint Ident) : base(Ident) {}
		public EInvalidArgument(Pointer<TResStringRec> ResStringRec) : base(ResStringRec) {}
		public EInvalidArgument(uint Ident, params TVarRec[] Args) : base(Ident, Args) {}
		public EInvalidArgument(Pointer<TResStringRec> ResStringRec, params TVarRec[] Args) : base(ResStringRec, Args) {}
		public EInvalidArgument(Pointer<TResStringRec> ResStringRec, TVarRec[] Args, int AHelpContext) : base(ResStringRec, Args, AHelpContext) {}
		public EInvalidArgument(uint Ident, TVarRec[] Args, int AHelpContext) : base(Ident, Args, AHelpContext) {}
		public EInvalidArgument(uint Ident, int AHelpContext) : base(Ident, AHelpContext) {}
		public EInvalidArgument(Pointer<TResStringRec> ResStringRec, int AHelpContext) : base(ResStringRec, AHelpContext) {}
	}	
	public const int ZeroValue = 0;
	public const int PositiveValue = 1;
	public const int NegativeValue = -1;
	public const double TwoPi = 6.2831853071795864769D;
	public const double PiOver2 = 1.5707963267948966192D;
	public const double PiOver4 = 0.7853981633974483096D;
	public const double MaxSingle = 3.403e38D;
	public const double MinSingle = 1.175e-38D;
	public const double MinSingleDenormal = 1.401e-45D;
    public const double MaxDouble = double.MaxValue;
	public const double MinDouble = 2.225e-308D;
    public const double MinDoubleDenormal = double.Epsilon;
	public const int seSSE = 1;
	public const int seSSE2 = 2;
	public const int seSSE3 = 4;
	public const int seSSSE3 = 8;
	public const int seSSE41 = 0x10;
	public const int seSSE42 = 0x20;
	public const int sePOPCNT = 0x40;
	public const int seAESNI = 0x80;
	public const int sePCLMULQDQ = 0x100;
	public enum TRoundingMode {rmNearest,
                    rmDown,
                    rmUp,
                    rmTruncate };

	public enum TFPUPrecisionMode {pmSingle,
                        pmReserved,
                        pmDouble,
                        pmExtended };
	public enum TArithmeticException {exInvalidOp,
                           exDenormalized,
                           exZeroDivide,
                           exOverflow,
                           exUnderflow,
                           exPrecision };

	public enum TPaymentTime {ptEndOfPeriod,
                   ptStartOfPeriod };
	public static readonly double Infinity = double.PositiveInfinity;
	public static readonly double NegInfinity = double.NegativeInfinity;
	public static readonly double NaN = double.NaN;
	public static long MaxComp = long.MaxValue;
	public static long MinComp = long.MinValue;

/* Min/max and range */
	public static sbyte Min(sbyte A, sbyte B)
	{
		return A < B ? A : B;
	}
	public static byte Min(byte A, byte B)
	{
		return A < B ? A : B;
	}
	public static short Min(short A, short B)
	{
		return A < B ? A : B;
	}
	public static ushort Min(ushort A, ushort B)
	{
		return A < B ? A : B;
	}
	public static int Min(int A, int B)
	{
		return A < B ? A : B;
	}
	public static uint Min(uint A, uint B)
	{
		return A < B ? A : B;
	}
	public static long Min(long A, long B)
	{
		return A < B ? A : B;
	}
	public static ulong Min(ulong A, ulong B)
	{
		return A < B ? A : B;
	}
	public static float Min(float A, float B)
	{
		return global::System.Math.Min(A, B);
	}
	public static double Min(double A, double B)
	{
		return global::System.Math.Min(A, B);
	}
	public static Currency Min(Currency A, Currency B)
	{
		return A < B ? A : B;
	}
	public static sbyte Max(sbyte A, sbyte B)
	{
		return A > B ? A : B;
	}
	public static byte Max(byte A, byte B)
	{
		return A > B ? A : B;
	}
	public static short Max(short A, short B)
	{
		return A > B ? A : B;
	}
	public static ushort Max(ushort A, ushort B)
	{
		return A > B ? A : B;
	}
	public static int Max(int A, int B)
	{
		return A > B ? A : B;
	}
	public static uint Max(uint A, uint B)
	{
		return A > B ? A : B;
	}
	public static long Max(long A, long B)
	{
		return A > B ? A : B;
	}
	public static ulong Max(ulong A, ulong B)
	{
		return A > B ? A : B;
	}
	public static float Max(float A, float B)
	{
		return global::System.Math.Max(A, B);
	}
	public static double Max(double A, double B)
	{
		return global::System.Math.Max(A, B);
	}
	public static Currency Max(Currency A, Currency B)
	{
		return A > B ? A : B;
	}
	public static int EnsureRange(int AValue, int AMin, int AMax)
	{
		if (AMin > AMax) throw new EInvalidArgument("AMin must not be greater than AMax");
				return AValue < AMin ? AMin : (AValue > AMax ? AMax : AValue);
	}
	public static long EnsureRange(long AValue, long AMin, long AMax)
	{
		if (AMin > AMax) throw new EInvalidArgument("AMin must not be greater than AMax");
				return AValue < AMin ? AMin : (AValue > AMax ? AMax : AValue);
	}
	public static ulong EnsureRange(ulong AValue, ulong AMin, ulong AMax)
	{
		if (AMin > AMax) throw new EInvalidArgument("AMin must not be greater than AMax");
				return AValue < AMin ? AMin : (AValue > AMax ? AMax : AValue);
	}
	public static float EnsureRange(float AValue, float AMin, float AMax)
	{
		if (AMin > AMax) throw new EInvalidArgument("AMin must not be greater than AMax");
				return AValue < AMin ? AMin : (AValue > AMax ? AMax : AValue);
	}
	public static double EnsureRange(double AValue, double AMin, double AMax)
	{
		if (AMin > AMax) throw new EInvalidArgument("AMin must not be greater than AMax");
				return AValue < AMin ? AMin : (AValue > AMax ? AMax : AValue);
	}
	public static bool InRange(int AValue, int AMin, int AMax)
	{
		return AValue >= AMin && AValue <= AMax;
	}
	public static bool InRange(long AValue, long AMin, long AMax)
	{
		return AValue >= AMin && AValue <= AMax;
	}
	public static bool InRange(ulong AValue, ulong AMin, ulong AMax)
	{
		return AValue >= AMin && AValue <= AMax;
	}
	public static bool InRange(float AValue, float AMin, float AMax)
	{
		return AValue >= AMin && AValue <= AMax;
	}
	public static bool InRange(double AValue, double AMin, double AMax)
	{
		return AValue >= AMin && AValue <= AMax;
	}

/* Comparison */
	public static bool SameValue(float A, float B, float Epsilon = 0.0F)
	{
		if (float.IsNaN(A) || float.IsNaN(B)) return false;
				if (A == B) return true;
				if (Epsilon == 0.0F) Epsilon = DefaultEpsilon(A, B);
				return global::System.Math.Abs(A - B) <= global::System.Math.Abs(Epsilon);
	}
	public static bool SameValue(double A, double B, double Epsilon = 0.0F)
	{
		if (double.IsNaN(A) || double.IsNaN(B)) return false;
				if (A == B) return true;
				if (Epsilon == 0.0D) Epsilon = DefaultEpsilon(A, B);
				return global::System.Math.Abs(A - B) <= global::System.Math.Abs(Epsilon);
	}
	public static bool IsZero(float A, float Epsilon = 0.0F)
	{
		if (float.IsNaN(A)) return false;
				if (Epsilon == 0.0F) Epsilon = 1.0e-5F;
				return global::System.Math.Abs(A) <= global::System.Math.Abs(Epsilon);
	}
	public static bool IsZero(double A, double Epsilon = 0.0F)
	{
		if (double.IsNaN(A)) return false;
				if (Epsilon == 0.0D) Epsilon = 1.0e-12D;
				return global::System.Math.Abs(A) <= global::System.Math.Abs(Epsilon);
	}
	public static int /*-1..1*/ CompareValue(float A, float B, float Epsilon = 0.0F)
	{
		if (SameValue(A, B, Epsilon)) return EqualsValue;
				return A < B ? LessThanValue : GreaterThanValue;
	}
	public static int /*-1..1*/ CompareValue(double A, double B, double Epsilon = 0.0F)
	{
		if (SameValue(A, B, Epsilon)) return EqualsValue;
				return A < B ? LessThanValue : GreaterThanValue;
	}
	public static int /*-1..1*/ CompareValue(int A, int B)
	{
		return A < B ? LessThanValue : (A > B ? GreaterThanValue : EqualsValue);
	}
	public static int /*-1..1*/ CompareValue(long A, long B)
	{
		return A < B ? LessThanValue : (A > B ? GreaterThanValue : EqualsValue);
	}
	public static int /*-1..1*/ CompareValue(ulong A, ulong B)
	{
		return A < B ? LessThanValue : (A > B ? GreaterThanValue : EqualsValue);
	}
	public static int /*-1..1*/ Sign(int AValue)
	{
		return AValue < 0 ? NegativeValue : (AValue > 0 ? PositiveValue : ZeroValue);
	}
	public static int /*-1..1*/ Sign(long AValue)
	{
		return AValue < 0 ? NegativeValue : (AValue > 0 ? PositiveValue : ZeroValue);
	}
	public static int /*-1..1*/ Sign(float AValue)
	{
		return AValue < 0.0F ? NegativeValue : (AValue > 0.0F ? PositiveValue : ZeroValue);
	}
	public static int /*-1..1*/ Sign(double AValue)
	{
		return AValue < 0.0D ? NegativeValue : (AValue > 0.0D ? PositiveValue : ZeroValue);
	}

/* Floating-point control */
	public static TRoundingMode GetRoundMode()
	{
		return _roundingMode;
	}
	public static TRoundingMode SetRoundMode(TRoundingMode RoundMode)
	{
		TRoundingMode previous = _roundingMode;
				_roundingMode = RoundMode;
				return previous;
	}
	public static TRoundingMode GetFPURoundMode()
	{
		return _roundingMode;
	}
	public static TRoundingMode SetFPURoundMode(TRoundingMode RoundMode)
	{
		TRoundingMode old = _roundingMode;
		_roundingMode = RoundMode;
		return old;
	}
	public static TRoundingMode GetSSERoundMode()
	{
		return _roundingMode;
	}
	public static TRoundingMode SetSSERoundMode(TRoundingMode RoundMode)
	{
		TRoundingMode old = _roundingMode;
		_roundingMode = RoundMode;
		return old;
	}
	public static TFPUPrecisionMode GetPrecisionMode()
	{
		return _precisionMode;
	}
	public static TFPUPrecisionMode SetPrecisionMode(TFPUPrecisionMode Precision)
	{
		TFPUPrecisionMode old = _precisionMode;
		_precisionMode = Precision;
		return old;
	}
	public static TSet GetExceptionMask()
	{
		return _exceptionMask;
	}
	public static TSet SetExceptionMask(TSet Mask)
	{
		TSet old = _exceptionMask;
		_exceptionMask = Mask;
		return old;
	}
	public static TSet GetExceptions()
	{
		return _exceptions;
	}
	public static TSet SetExceptions(TSet Exceptions)
	{
		TSet old = _exceptions;
		_exceptions = Exceptions;
		return old;
	}
	public static TSet GetFPUExceptionMask()
	{
		return _fpuExceptionMask;
	}
	public static TSet SetFPUExceptionMask(TSet Mask)
	{
		TSet old = _fpuExceptionMask;
		_fpuExceptionMask = Mask;
		return old;
	}
	public static TSet GetSSEExceptionMask()
	{
		return _sseExceptionMask;
	}
	public static TSet SetSSEExceptionMask(TSet Mask)
	{
		TSet old = _sseExceptionMask;
		_sseExceptionMask = Mask;
		return old;
	}
	public static void ClearExceptions(bool RaisePending = true)
	{
		_exceptions = new TSet();
	}
	public static void ClearFPUExceptions(bool RaisePending = true)
	{
		_exceptions = new TSet();
	}
	public static void ClearSSEExceptions(bool RaisePending = true)
	{
		_exceptions = new TSet();
	}
	public static void RaiseExceptions(TSet ExceptionFlags)
	{
		_exceptions = ExceptionFlags;
	}

/* Rounding and decomposition */
	public static double RoundTo(double AValue, int /*-20..20*/ ADigit)
	{
		return RoundScaled(AValue, ADigit, global::System.MidpointRounding.ToEven);
	}
	public static float SimpleRoundTo(float AValue, int /*-37..37*/ ADigit = -2)
	{
		return (float)RoundScaled(AValue, ADigit, global::System.MidpointRounding.AwayFromZero);
	}
	public static double SimpleRoundTo(double AValue, int /*-37..37*/ ADigit = -2)
	{
		return RoundScaled(AValue, ADigit, global::System.MidpointRounding.AwayFromZero);
	}
	public static int Floor(float X)
	{
		return checked((int)global::System.Math.Floor(X));
	}
	public static int Floor(double X)
	{
		return checked((int)global::System.Math.Floor(X));
	}
	public static int Ceil(float X)
	{
		return checked((int)global::System.Math.Ceiling(X));
	}
	public static int Ceil(double X)
	{
		return checked((int)global::System.Math.Ceiling(X));
	}
	public static void Frexp(float X, ref float Mantissa, ref int Exponent)
	{
		if (X == 0.0F || float.IsNaN(X) || float.IsInfinity(X))
		{
		    Mantissa = X;
		    Exponent = 0;
		    return;
		}
		Exponent = (int)global::System.Math.Floor(global::System.Math.Log(global::System.Math.Abs(X), 2.0D)) + 1;
		Mantissa = (float)(X / global::System.Math.Pow(2.0D, Exponent));
	}
	public static void Frexp(double X, ref double Mantissa, ref int Exponent)
	{
		if (X == 0.0D || double.IsNaN(X) || double.IsInfinity(X))
		{
		    Mantissa = X;
		    Exponent = 0;
		    return;
		}
		Exponent = (int)global::System.Math.Floor(global::System.Math.Log(global::System.Math.Abs(X), 2.0D)) + 1;
		Mantissa = X / global::System.Math.Pow(2.0D, Exponent);
	}
	public static float FMod(float ANumerator, float ADenominator)
	{
		return ANumerator % ADenominator;
	}
	public static double FMod(double ANumerator, double ADenominator)
	{
		return ANumerator % ADenominator;
	}
	public static void DivMod(uint Dividend, ushort Divisor, ref ushort Result, ref ushort Remainder)
	{
		if (Divisor == 0) throw new EInvalidArgument("Divisor must not be zero");
		Result = (ushort)(Dividend / Divisor);
		Remainder = (ushort)(Dividend % Divisor);
	}
	public static void DivMod(ulong Dividend, ulong Divisor, ref ulong Result, ref ulong Remainder)
	{
		if (Divisor == 0) throw new EInvalidArgument("Divisor must not be zero");
		Result = Dividend / Divisor;
		Remainder = Dividend % Divisor;
	}

/* Powers and logarithms */
	public static float IntPower(float Base, int Exponent)
	{
		return (float)IntPower((double)Base, Exponent);
	}
	public static double IntPower(double Base, int Exponent)
	{
		if (Exponent == 0) return 1.0D;
				long e = Exponent;
				bool negative = e < 0;
				if (negative) e = -e;
				double result = 1.0D;
				double factor = Base;
				while (e != 0)
				{
					if ((e & 1L) != 0) result *= factor;
					factor *= factor;
					e >>= 1;
				}
				return negative ? 1.0D / result : result;
	}
	public static double Power(double Base, double Exponent)
	{
		return global::System.Math.Pow(Base, Exponent);
	}
	public static double LogN(double Base, double X)
	{
		return global::System.Math.Log(X, Base);
	}
	public static double Log2(double X)
	{
		return global::System.Math.Log(X, 2.0D);
	}
	public static double Log10(double X)
	{
		return global::System.Math.Log10(X);
	}
	public static double LnXP1(double X)
	{
		return global::System.Math.Log(1.0D + X);
	}
	public static double Ldexp(double X, int P)
	{
		return X * global::System.Math.Pow(2.0D, P);
	}

/* Trigonometric and hyperbolic functions */
	public static double ArcCos(double X)
	{
		return global::System.Math.Acos(X);
	}
	public static double ArcSin(double X)
	{
		return global::System.Math.Asin(X);
	}
	public static double ArcTan2(double Y, double X)
	{
		return global::System.Math.Atan2(Y, X);
	}
	public static double ArcCot(double X)
	{
		return global::System.Math.Atan2(1.0D, X);
	}
	public static double ArcCotH(double X)
	{
		return 0.5D * global::System.Math.Log((X + 1.0D) / (X - 1.0D));
	}
	public static double ArcCsc(double X)
	{
		return global::System.Math.Asin(1.0D / X);
	}
	public static double ArcCscH(double X)
	{
		return global::System.Math.Log(1.0D / X + global::System.Math.Sqrt(1.0D / (X * X) + 1.0D));
	}
	public static double ArcSec(double X)
	{
		return global::System.Math.Acos(1.0D / X);
	}
	public static double ArcSecH(double X)
	{
		return global::System.Math.Log((1.0D + global::System.Math.Sqrt(1.0D - X * X)) / X);
	}
	public static double SinH(double X)
	{
		return global::System.Math.Sinh(X);
	}
	public static double CosH(double X)
	{
		return global::System.Math.Cosh(X);
	}
	public static double TanH(double X)
	{
		return global::System.Math.Tanh(X);
	}
	public static double SecH(double X)
	{
		return 1.0D / global::System.Math.Cosh(X);
	}
	public static double CscH(double X)
	{
		return 1.0D / global::System.Math.Sinh(X);
	}
	public static double CotH(double X)
	{
		return global::System.Math.Cosh(X) / global::System.Math.Sinh(X);
	}
	public static double ArcSinh(double X)
	{
		return global::System.Math.Log(X + global::System.Math.Sqrt(X * X + 1.0D));
	}
	public static double ArcCosh(double X)
	{
		return global::System.Math.Log(X + global::System.Math.Sqrt(X * X - 1.0D));
	}
	public static double ArcTanh(double X)
	{
		return 0.5D * global::System.Math.Log((1.0D + X) / (1.0D - X));
	}
	public static double Tan(double X)
	{
		return global::System.Math.Tan(X);
	}
	public static double Cot(double X)
	{
		return 1.0D / global::System.Math.Tan(X);
	}
	public static double Cotan(double X)
	{
		return Cot(X);
	}
	public static double Sec(double X)
	{
		return 1.0D / global::System.Math.Cos(X);
	}
	public static double Secant(double X)
	{
		return Sec(X);
	}
	public static double Csc(double X)
	{
		return 1.0D / global::System.Math.Sin(X);
	}
	public static double Cosecant(double X)
	{
		return Csc(X);
	}
	public static void SinCos(double Theta, ref double Sin, ref double Cos)
	{
		Sin = global::System.Math.Sin(Theta);
				Cos = global::System.Math.Cos(Theta);
	}
	public static float Hypot(float X, float Y)
	{
		return (float)Hypot((double)X, (double)Y);
	}
	public static double Hypot(double X, double Y)
	{
		double ax = global::System.Math.Abs(X);
				double ay = global::System.Math.Abs(Y);
				if (ax < ay) { double t = ax; ax = ay; ay = t; }
				if (ax == 0.0D) return 0.0D;
				double r = ay / ax;
				return ax * global::System.Math.Sqrt(1.0D + r * r);
	}
	public static float Hypot(float X, float Y, float Z)
	{
		return (float)Hypot((double)X, (double)Y, (double)Z);
	}
	public static double Hypot(double X, double Y, double Z)
	{
		double ax = global::System.Math.Abs(X), ay = global::System.Math.Abs(Y), az = global::System.Math.Abs(Z);
		double m = global::System.Math.Max(ax, global::System.Math.Max(ay, az));
		if (m == 0.0D) return 0.0D;
		ax /= m; ay /= m; az /= m;
		return m * global::System.Math.Sqrt(ax * ax + ay * ay + az * az);
	}

/* Angle conversion */
	public static double DegToRad(double Degrees)
	{
		return Degrees * global::System.Math.PI / 180.0D;
	}
	public static double RadToDeg(double Radians)
	{
		return Radians * 180.0D / global::System.Math.PI;
	}
	public static double GradToRad(double Grads)
	{
		return Grads * global::System.Math.PI / 200.0D;
	}
	public static double RadToGrad(double Radians)
	{
		return Radians * 200.0D / global::System.Math.PI;
	}
	public static double DegToGrad(double Degrees)
	{
		return Degrees * 10.0D / 9.0D;
	}
	public static double GradToDeg(double Grads)
	{
		return Grads * 9.0D / 10.0D;
	}
	public static double CycleToRad(double Cycles)
	{
		return Cycles * TwoPi;
	}
	public static double RadToCycle(double Radians)
	{
		return Radians / TwoPi;
	}
	public static double CycleToDeg(double Cycles)
	{
		return Cycles * 360.0D;
	}
	public static double DegToCycle(double Degrees)
	{
		return Degrees / 360.0D;
	}
	public static double CycleToGrad(double Cycles)
	{
		return Cycles * 400.0D;
	}
	public static double GradToCycle(double Grads)
	{
		return Grads / 400.0D;
	}

/* Conditional helpers */
	public static int IfThen(bool AValue, int ATrue, int AFalse = 0)
	{
		return AValue ? ATrue : AFalse;
	}
	public static long IfThen(bool AValue, long ATrue, long AFalse = 0)
	{
		return AValue ? ATrue : AFalse;
	}
	public static float IfThen(bool AValue, float ATrue, float AFalse = 0.0F)
	{
		return AValue ? ATrue : AFalse;
	}
	public static double IfThen(bool AValue, double ATrue, double AFalse = 0.0F)
	{
		return AValue ? ATrue : AFalse;
	}

/* Random helpers */
	public static int RandomRange(int AFrom, int ATo)
	{
		if (AFrom == ATo) return AFrom;
				int min = AFrom < ATo ? AFrom : ATo;
				int max = AFrom < ATo ? ATo : AFrom;
				lock (_randomLock) return _random.Next(min, max);
	}
	public static long RandomRange(long AFrom, long ATo)
	{
		if (AFrom == ATo) return AFrom;
				long min = AFrom < ATo ? AFrom : ATo;
				long max = AFrom < ATo ? ATo : AFrom;
				return NextInt64(min, max);
	}
	public static sbyte RandomFrom(sbyte[] AValues)
	{
		return RandomElement(AValues);
	}
	public static short RandomFrom(short[] AValues)
	{
		return RandomElement(AValues);
	}
	public static int RandomFrom(int[] AValues)
	{
		return RandomElement(AValues);
	}
	public static long RandomFrom(long[] AValues)
	{
		return RandomElement(AValues);
	}
	public static byte RandomFrom(byte[] AValues)
	{
		return RandomElement(AValues);
	}
	public static ushort RandomFrom(ushort[] AValues)
	{
		return RandomElement(AValues);
	}
	public static uint RandomFrom(uint[] AValues)
	{
		return RandomElement(AValues);
	}
	public static ulong RandomFrom(ulong[] AValues)
	{
		return RandomElement(AValues);
	}
	public static float RandomFrom(float[] AValues)
	{
		return RandomElement(AValues);
	}
	public static double RandomFrom(double[] AValues)
	{
		return RandomElement(AValues);
	}
	public static bool RandomFrom(bool[] AValues)
	{
		return RandomElement(AValues);
	}
	public static float RandG(float Mean, float StdDev)
	{
		return (float)RandG((double)Mean, (double)StdDev);
	}
	public static double RandG(double Mean, double StdDev)
	{
		if (StdDev < 0.0D) throw new EInvalidArgument("StdDev must not be negative");
				double u1;
				double u2;
				lock (_randomLock)
				{
					u1 = 1.0D - _random.NextDouble();
					u2 = 1.0D - _random.NextDouble();
				}
				double z = global::System.Math.Sqrt(-2.0D * global::System.Math.Log(u1)) * global::System.Math.Cos(TwoPi * u2);
				return Mean + StdDev * z;
	}

/* Classification */
	public static bool IsNan(float AValue)
	{
		return float.IsNaN(AValue);
	}
	public static bool IsNan(double AValue)
	{
		return double.IsNaN(AValue);
	}
	public static bool IsInfinite(float AValue)
	{
		return float.IsInfinity(AValue);
	}
	public static bool IsInfinite(double AValue)
	{
		return double.IsInfinity(AValue);
	}

/* Statistics */
	public static float Sum(float[] Data)
	{
		RequireData(Data);
				double sum = 0.0D;
				for (int i = 0; i < Data.Length; i++) sum += Data[i];
				return (float)sum;
	}
	public static double Sum(double[] Data)
	{
		RequireData(Data);
				double sum = 0.0D;
				for (int i = 0; i < Data.Length; i++) sum += Data[i];
				return sum;
	}
	public static long SumInt(int[] Data)
	{
		RequireData(Data);
				long sum = 0L;
				for (int i = 0; i < Data.Length; i++) sum += Data[i];
				return sum;
	}
	public static float SumOfSquares(float[] Data)
	{
		RequireData(Data);
				double sum = 0.0D;
				for (int i = 0; i < Data.Length; i++) sum += (double)Data[i] * Data[i];
				return (float)sum;
	}
	public static double SumOfSquares(double[] Data)
	{
		RequireData(Data);
				double sum = 0.0D;
				for (int i = 0; i < Data.Length; i++) sum += Data[i] * Data[i];
				return sum;
	}
	public static void SumsAndSquares(float[] Data, ref float Sum, ref float SumOfSquares)
	{
		Sum = DmathInterface.Sum(Data);
				SumOfSquares = DmathInterface.SumOfSquares(Data);
	}
	public static void SumsAndSquares(double[] Data, ref double Sum, ref double SumOfSquares)
	{
		Sum = DmathInterface.Sum(Data);
				SumOfSquares = DmathInterface.SumOfSquares(Data);
	}
	public static float Mean(float[] Data)
	{
		RequireNonEmpty(Data);
				return Sum(Data) / Data.Length;
	}
	public static double Mean(double[] Data)
	{
		RequireNonEmpty(Data);
				return Sum(Data) / Data.Length;
	}
	public static float TotalVariance(float[] Data)
	{
		RequireNonEmpty(Data);
				double mean = Mean(Data);
				double total = 0.0D;
				for (int i = 0; i < Data.Length; i++) { double d = Data[i] - mean; total += d * d; }
				return (float)total;
	}
	public static double TotalVariance(double[] Data)
	{
		RequireNonEmpty(Data);
				double mean = Mean(Data);
				double total = 0.0D;
				for (int i = 0; i < Data.Length; i++) { double d = Data[i] - mean; total += d * d; }
				return total;
	}
	public static float Variance(float[] Data)
	{
		RequireAtLeastTwo(Data);
				return TotalVariance(Data) / (Data.Length - 1);
	}
	public static double Variance(double[] Data)
	{
		RequireAtLeastTwo(Data);
				return TotalVariance(Data) / (Data.Length - 1);
	}
	public static float PopnVariance(float[] Data)
	{
		RequireNonEmpty(Data);
		float mean = Mean(Data);
		double total = 0.0D;
		for (int i = 0; i < Data.Length; i++)
		{
		    double d = Data[i] - mean;
		    total += d * d;
		}
		return (float)(total / Data.Length);
	}
	public static double PopnVariance(double[] Data)
	{
		RequireNonEmpty(Data);
		double mean = Mean(Data);
		double total = 0.0D;
		for (int i = 0; i < Data.Length; i++)
		{
		    double d = Data[i] - mean;
		    total += d * d;
		}
		return (total / Data.Length);
	}
	public static float StdDev(float[] Data)
	{
		return (float)global::System.Math.Sqrt(Variance(Data));
	}
	public static double StdDev(double[] Data)
	{
		return global::System.Math.Sqrt(Variance(Data));
	}
	public static float PopnStdDev(float[] Data)
	{
		return (float)global::System.Math.Sqrt(PopnVariance(Data));
	}
	public static double PopnStdDev(double[] Data)
	{
		return global::System.Math.Sqrt(PopnVariance(Data));
	}
	public static void MeanAndStdDev(float[] Data, ref float Mean, ref float StdDev)
	{
		Mean = DmathInterface.Mean(Data);
				StdDev = DmathInterface.StdDev(Data);
	}
	public static void MeanAndStdDev(double[] Data, ref double Mean, ref double StdDev)
	{
		Mean = DmathInterface.Mean(Data);
				StdDev = DmathInterface.StdDev(Data);
	}
	public static float MaxValue(float[] Data)
	{
		RequireNonEmpty(Data);
				float result = Data[0];
				for (int i = 1; i < Data.Length; i++) if (Data[i] > result) result = Data[i];
				return result;
	}
	public static double MaxValue(double[] Data)
	{
		RequireNonEmpty(Data);
				double result = Data[0];
				for (int i = 1; i < Data.Length; i++) if (Data[i] > result) result = Data[i];
				return result;
	}
	public static float MinValue(float[] Data)
	{
		RequireNonEmpty(Data);
				float result = Data[0];
				for (int i = 1; i < Data.Length; i++) if (Data[i] < result) result = Data[i];
				return result;
	}
	public static double MinValue(double[] Data)
	{
		RequireNonEmpty(Data);
				double result = Data[0];
				for (int i = 1; i < Data.Length; i++) if (Data[i] < result) result = Data[i];
				return result;
	}
	public static int MaxIntValue(int[] Data)
	{
		RequireNonEmpty(Data);
				int result = Data[0];
				for (int i = 1; i < Data.Length; i++) if (Data[i] > result) result = Data[i];
				return result;
	}
	public static int MinIntValue(int[] Data)
	{
		RequireNonEmpty(Data);
				int result = Data[0];
				for (int i = 1; i < Data.Length; i++) if (Data[i] < result) result = Data[i];
				return result;
	}
	public static float Norm(float[] Data)
	{
		RequireData(Data);
		double sum = 0.0D;
		for (int i = 0; i < Data.Length; i++) sum += (double)Data[i] * Data[i];
		return (float)global::System.Math.Sqrt(sum);
	}
	public static double Norm(double[] Data)
	{
		RequireData(Data);
		double sum = 0.0D;
		for (int i = 0; i < Data.Length; i++) sum += (double)Data[i] * Data[i];
		return global::System.Math.Sqrt(sum);
	}
	public static float Poly(float X, float[] Coefficients)
	{
		RequireData(Coefficients);
		double result = 0.0D;
		for (int i = Coefficients.Length - 1; i >= 0; i--) result = result * X + Coefficients[i];
		return (float)result;
	}
	public static double Poly(double X, double[] Coefficients)
	{
		RequireData(Coefficients);
		double result = 0.0D;
		for (int i = Coefficients.Length - 1; i >= 0; i--) result = result * X + Coefficients[i];
		return result;
	}

/* Finance */
	public static double DoubleDecliningBalance(double Cost, double Salvage, int Life, int Period)
	{
		if (Life <= 0) throw new EInvalidArgument("Life must be positive");
		if (Period < 1 || Period > Life) throw new EInvalidArgument("Period must be between 1 and Life");
		double book = Cost;
		double depreciation = 0.0D;
		for (int p = 1; p <= Period; p++)
		{
		    depreciation = global::System.Math.Min(book * (2.0D / Life), book - Salvage);
		    if (depreciation < 0.0D) depreciation = 0.0D;
		    book -= depreciation;
		}
		return depreciation;
	}
	public static double FutureValue(double Rate, int NPeriods, double Payment, double PresentValue, TPaymentTime PaymentTime)
	{
		if (NPeriods < 0) throw new EInvalidArgument("NPeriods must not be negative");
		double type = PaymentTime == TPaymentTime.ptStartOfPeriod ? 1.0D : 0.0D;
		if (Rate == 0.0D) return -(PresentValue + Payment * NPeriods);
		double factor = global::System.Math.Pow(1.0D + Rate, NPeriods);
		return -(PresentValue * factor + Payment * (1.0D + Rate * type) * (factor - 1.0D) / Rate);
	}
	public static double PresentValue(double Rate, int NPeriods, double Payment, double FutureValue, TPaymentTime PaymentTime)
	{
		if (NPeriods < 0) throw new EInvalidArgument("NPeriods must not be negative");
		double type = PaymentTime == TPaymentTime.ptStartOfPeriod ? 1.0D : 0.0D;
		if (Rate == 0.0D) return -(FutureValue + Payment * NPeriods);
		double factor = global::System.Math.Pow(1.0D + Rate, NPeriods);
		return -(FutureValue + Payment * (1.0D + Rate * type) * (factor - 1.0D) / Rate) / factor;
	}
	public static double InterestPayment(double Rate, int Period, int NPeriods, double PresentValue, double FutureValue, TPaymentTime PaymentTime)
	{
		if (NPeriods <= 0) throw new EInvalidArgument("NPeriods must be positive");
		if (Period < 1 || Period > NPeriods) throw new EInvalidArgument("Period must be between 1 and NPeriods");
		if (Rate == 0.0D) return 0.0D;
		double type = PaymentTime == TPaymentTime.ptStartOfPeriod ? 1.0D : 0.0D;
		double payment = PaymentAmount(Rate, NPeriods, PresentValue, FutureValue, type);
		if (type != 0.0D && Period == 1) return 0.0D;
		int elapsed = Period - 1;
		double factor = global::System.Math.Pow(1.0D + Rate, elapsed);
		double balance = PresentValue * factor + payment * (1.0D + Rate * type) * (factor - 1.0D) / Rate;
		return -balance * Rate;
	}
	public static double InterestRate(int NPeriods, double Payment, double PresentValue, double FutureValue, TPaymentTime PaymentTime, double Guess)
	{
		if (NPeriods <= 0) throw new EInvalidArgument("NPeriods must be positive");
		double type = PaymentTime == TPaymentTime.ptStartOfPeriod ? 1.0D : 0.0D;
		double rate = Guess;
		if (rate <= -0.999999999D) rate = -0.9D;
		for (int i = 0; i < 100; i++)
		{
		    double f = RateEquation(rate, NPeriods, Payment, PresentValue, FutureValue, type);
		    if (global::System.Math.Abs(f) < 1.0e-10D) return rate;
		    double h = global::System.Math.Max(1.0e-7D, global::System.Math.Abs(rate) * 1.0e-6D);
		    double low = global::System.Math.Max(rate - h, -0.999999999D);
		    double derivative = (RateEquation(rate + h, NPeriods, Payment, PresentValue, FutureValue, type) - RateEquation(low, NPeriods, Payment, PresentValue, FutureValue, type)) / ((rate + h) - low);
		    if (derivative == 0.0D || double.IsNaN(derivative) || double.IsInfinity(derivative)) break;
		    double next = rate - f / derivative;
		    if (next <= -0.999999999D) next = (rate - 0.999999999D) / 2.0D;
		    if (global::System.Math.Abs(next - rate) < 1.0e-12D) return next;
		    rate = next;
		}
		throw new EInvalidArgument("InterestRate did not converge");
	}
	public static double InternalRateOfReturn(double Guess, double[] CashFlows)
	{
		RequireNonEmpty(CashFlows);
		double rate = Guess;
		if (rate <= -0.999999999D) rate = -0.9D;
		for (int iter = 0; iter < 100; iter++)
		{
		    double value = 0.0D, deriv = 0.0D;
		    for (int i = 0; i < CashFlows.Length; i++)
		    {
		        double p = global::System.Math.Pow(1.0D + rate, i);
		        value += CashFlows[i] / p;
		        if (i > 0) deriv -= i * CashFlows[i] / (p * (1.0D + rate));
		    }
		    if (global::System.Math.Abs(value) < 1.0e-10D) return rate;
		    if (deriv == 0.0D || double.IsNaN(deriv) || double.IsInfinity(deriv)) break;
		    double next = rate - value / deriv;
		    if (next <= -0.999999999D) next = (rate - 0.999999999D) / 2.0D;
		    if (global::System.Math.Abs(next - rate) < 1.0e-12D) return next;
		    rate = next;
		}
		throw new EInvalidArgument("InternalRateOfReturn did not converge");
	}
	public static double NumberOfPeriods(double Rate, double Payment, double PresentValue, double FutureValue, TPaymentTime PaymentTime)
	{
		double type = PaymentTime == TPaymentTime.ptStartOfPeriod ? 1.0D : 0.0D;
		if (Rate == 0.0D)
		{
		    if (Payment == 0.0D) throw new EInvalidArgument("Payment must not be zero");
		    return -(PresentValue + FutureValue) / Payment;
		}
		double p = Payment * (1.0D + Rate * type) / Rate;
		double numerator = p - FutureValue;
		double denominator = PresentValue + p;
		if (numerator <= 0.0D || denominator <= 0.0D) throw new EInvalidArgument("Arguments do not yield a real number of periods");
		return global::System.Math.Log(numerator / denominator) / global::System.Math.Log(1.0D + Rate);
	}
	public static double Payment(double Rate, int NPeriods, double PresentValue, double FutureValue, TPaymentTime PaymentTime)
	{
		if (NPeriods <= 0) throw new EInvalidArgument("NPeriods must be positive");
		double type = PaymentTime == TPaymentTime.ptStartOfPeriod ? 1.0D : 0.0D;
		return PaymentAmount(Rate, NPeriods, PresentValue, FutureValue, type);
	}
	public static double PeriodPayment(double Rate, int Period, int NPeriods, double PresentValue, double FutureValue, TPaymentTime PaymentTime)
	{
		double totalPayment = Payment(Rate, NPeriods, PresentValue, FutureValue, PaymentTime);
		return totalPayment - InterestPayment(Rate, Period, NPeriods, PresentValue, FutureValue, PaymentTime);
	}
	public static double SLNDepreciation(double Cost, double Salvage, int Life)
	{
		if (Life <= 0) throw new EInvalidArgument("Life must be positive");
		return (Cost - Salvage) / Life;
	}
	public static double SYDDepreciation(double Cost, double Salvage, int Life, int Period)
	{
		if (Life <= 0) throw new EInvalidArgument("Life must be positive");
		if (Period < 1 || Period > Life) throw new EInvalidArgument("Period must be between 1 and Life");
		return (Cost - Salvage) * (Life - Period + 1.0D) * 2.0D / (Life * (Life + 1.0D));
	}

	private static readonly object _randomLock = new object();
	private static readonly global::System.Random _random = new global::System.Random();
	private static TRoundingMode _roundingMode = TRoundingMode.rmNearest;

	private static float DefaultEpsilon(float A, float B)
	{
		float scale = global::System.Math.Max(global::System.Math.Abs(A), global::System.Math.Abs(B));
		return global::System.Math.Max(1.0e-5F, scale * 1.0e-5F);
	}

	private static double DefaultEpsilon(double A, double B)
	{
		double scale = global::System.Math.Max(global::System.Math.Abs(A), global::System.Math.Abs(B));
		return global::System.Math.Max(1.0e-12D, scale * 1.0e-12D);
	}

	private static double RoundScaled(double value, int digit, global::System.MidpointRounding mode)
	{
		if (double.IsNaN(value) || double.IsInfinity(value) || value == 0.0D)
			return value;
		double scale = global::System.Math.Pow(10.0D, -digit);
		if (scale == 0.0D || double.IsInfinity(scale))
			return value;
		return global::System.Math.Round(value / scale, 0, mode) * scale;
	}

	private static void RequireData(global::System.Array data)
	{
		if (data == null) throw new EInvalidArgument("Data must not be null");
	}

	private static void RequireNonEmpty(global::System.Array data)
	{
		RequireData(data);
		if (data.Length == 0) throw new EInvalidArgument("Data must not be empty");
	}

	private static void RequireAtLeastTwo(global::System.Array data)
	{
		RequireData(data);
		if (data.Length < 2) throw new EInvalidArgument("Data must contain at least two values");
	}

	private static long NextInt64(long minInclusive, long maxExclusive)
	{
		if (minInclusive >= maxExclusive) return minInclusive;
		ulong range = unchecked((ulong)(maxExclusive - minInclusive));
		byte[] bytes = new byte[8];
		ulong limit = ulong.MaxValue - (ulong.MaxValue % range);
		ulong value;
		do
		{
			lock (_randomLock) _random.NextBytes(bytes);
			value = global::System.BitConverter.ToUInt64(bytes, 0);
		} while (value >= limit);
		return unchecked(minInclusive + (long)(value % range));
	}

	private static double PaymentAmount(double rate, int periods, double presentValue, double futureValue, double type)
	{
		if (rate == 0.0D)
			return -(presentValue + futureValue) / periods;
		double factor = global::System.Math.Pow(1.0D + rate, periods);
		return -(rate * (futureValue + presentValue * factor)) /
			((1.0D + rate * type) * (factor - 1.0D));
	}

	private static double RateEquation(double rate, int periods, double payment, double presentValue, double futureValue, double type)
	{
		if (global::System.Math.Abs(rate) < 1.0e-14D)
			return presentValue + payment * periods + futureValue;
		double factor = global::System.Math.Pow(1.0D + rate, periods);
		return presentValue * factor + payment * (1.0D + rate * type) * (factor - 1.0D) / rate + futureValue;
	}


	private static TFPUPrecisionMode _precisionMode = TFPUPrecisionMode.pmExtended;
	private static TSet _exceptionMask = new TSet();
	private static TSet _exceptions = new TSet();
	private static TSet _fpuExceptionMask = new TSet();
	private static TSet _sseExceptionMask = new TSet();

	private static T RandomElement<T>(T[] values)
	{
		if (values == null) throw new EInvalidArgument("AValues must not be null");
		if (values.Length == 0) throw new EInvalidArgument("AValues must not be empty");
		int index;
		lock (_randomLock) index = _random.Next(values.Length);
		return values[index];
	}

	
} // class DmathInterface


file class DmathImplementation
{

} // class DmathImplementation

}  // namespace Dmath

}  // namespace System

