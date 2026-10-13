/*
  D2CSharp Runtime Library (RTL)

  This file is implemented directly in C# and provides supporting
  functionality for the runtime library. It is not generated from
  a Delphi source file.

  Copyright (c) 2026 Dr. Detlef Meyer-Eltz, t2t-soft
  SPDX-License-Identifier: MPL-2.0

  This Source Code Form is subject to the terms of the Mozilla Public
  License, v. 2.0. If a copy of the MPL was not distributed with this
  file, You can obtain one at https://mozilla.org/MPL/2.0/.

  Part of the D2CSharp project by t2t-soft.
*/

using System.Formats.Asn1;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using static Winapi.Windows.WindowsInterface;
using static System.SystemInterface;

namespace System
{

    public sealed class DelphiInterfaceEntry
    {
        private readonly Guid FIID;
        private readonly Type FInterfaceType;
        private readonly Func<TObject, object> FGetter;

        public DelphiInterfaceEntry(Guid iid, Type interfaceType, Func<TObject, object> getter)
        {
            if (interfaceType == null)
                throw new ArgumentNullException("interfaceType");

            if (!interfaceType.IsInterface)
                throw new ArgumentException("The type must be an interface type.", "interfaceType");

            if (getter == null)
                throw new ArgumentNullException("getter");

            FIID = iid;
            FInterfaceType = interfaceType;
            FGetter = getter;
        }

        public Guid IID
        {
            get
            {
                return FIID;
            }
        }

        public Type InterfaceType
        {
            get
            {
                return FInterfaceType;
            }
        }

        public object GetObject(TObject self)
        {
            if (self == null)
                return null;

            return FGetter(self);
        }
    }
    public class TObject
    {
        protected bool FDisposed;

        protected void CheckDisposed()
        {
            if (FDisposed)
                throw new ObjectDisposedException(ClassName());
        }

        protected bool GetDisposed()
        {
            return FDisposed;
        }

        public bool Disposed
        {
            get { return GetDisposed(); }
        }

        public virtual void Destroy()
        {
            if (FDisposed)
                return;

            BeforeDestruction();
            FDisposed = true;
            GC.SuppressFinalize(this);
        }

        public void Free()
        {
            Destroy();
        }

        public static void Free(TObject obj)
        {
            if (obj != null)
                obj.Destroy();
        }

        [Obsolete("DisposeOf is retained for Delphi compatibility.")]
        public void DisposeOf()
        {
            Destroy();
        }

        public static TObject NewInstance()
        {
            return new TObject();
        }

        public virtual void FreeInstance()
        {
            if (!FDisposed)
            {
                FDisposed = true;
                GC.SuppressFinalize(this);
            }
        }

        public static TObject InitInstance(Pointer instance)
        {
            return null;
        }

        public void CleanupInstance()
        {
        }

        public virtual void AfterConstruction()
        {
        }

        public virtual void BeforeDestruction()
        {
        }

        public TClass ClassType()
        {
            return TClass.Of(GetType());
        }

        public string ClassName()
        {
            return ClassType().ClassName();
        }

        public string QualifiedClassName()
        {
            return GetType().FullName ?? GetType().Name;
        }

        public string UnitName()
        {
            return GetType().Namespace ?? string.Empty;
        }

        public string UnitScope()
        {
            string ns = GetType().Namespace ?? string.Empty;
            int separator = ns.IndexOf('.');
            return separator < 0 ? ns : ns.Substring(0, separator);
        }

        public TClass ClassParent()
        {
            return ClassType().ClassParent();
        }

        public Pointer ClassInfo()
        {
            return default;
        }

        public int InstanceSize()
        {
            return 0;
        }

        public bool InheritsFrom(TClass aClass)
        {
            return ClassType().InheritsFrom(aClass);
        }

        public bool ClassNameIs(string name)
        {
            return ClassType().ClassNameIs(name);
        }

        public Pointer FieldAddress(string name)
        {
            return default;
        }

        public Pointer MethodAddress(string name)
        {
            return default;
        }

        public string MethodName(Pointer address)
        {
            return string.Empty;
        }

        public virtual void Dispatch(UntypedPointer message)
        {
            DefaultHandler(message);
        }

        public virtual void DefaultHandler(UntypedPointer message)
        {
        }

        public static Pointer<SystemInterface.TInterfaceEntry> GetInterfaceEntry(Guid iid)
        {
            return default;
        }

        public static Pointer<SystemInterface.TInterfaceTable> GetInterfaceTable()
        {
            return default;
        }

        public string InterfaceName()
        {
            return ToString();
        }

        protected virtual IEnumerable<DelphiInterfaceEntry> GetDelphiInterfaces()
        {
            yield break;
        }

        public virtual bool GetInterface(Guid iid, out object obj)
        {
            foreach (DelphiInterfaceEntry entry in GetDelphiInterfaces())
            {
                if (entry.IID.Equals(iid))
                {
                    obj = entry.GetObject(this);
                    return obj != null;
                }
            }

            obj = null;
            return false;
        }

        public virtual bool GetInterface(Guid iid, UntypedPointer obj)
        {
            if (obj.IsNull())
                return false;

            object interfaceObject;
            if (!GetInterface(iid, out interfaceObject))
                return false;

            obj.Write<object>(interfaceObject);
            return true;
        }

        public virtual int QueryInterface(Guid iid, out object obj)
        {
            if (GetInterface(iid, out obj))
                return S_OK;

            return E_NOINTERFACE;
        }

        public virtual int SafeCallException(TObject exceptObject, Pointer exceptAddr)
        {
            return E_UNEXPECTED;
        }

        public virtual bool Equals(TObject obj)
        {
            return ReferenceEquals(this, obj);
        }

        public override bool Equals(object obj)
        {
            return obj is TObject other && Equals(other);
        }

        public override int GetHashCode()
        {
            return RuntimeHelpers.GetHashCode(this);
        }

        public override string ToString()
        {
            return ClassName();
        }
    }

    //public static class TInterfaceRegistry
    //{
    //    private static readonly global::System.Collections.Generic.Dictionary<global::System.Guid, global::System.Type> FTypes =
    //        new global::System.Collections.Generic.Dictionary<global::System.Guid, global::System.Type>();

    //    public static void Register(global::System.Guid iid, global::System.Type interfaceType)
    //    {
    //        FTypes[iid] = interfaceType;
    //    }

    //    public static global::System.Type GetInterfaceType(global::System.Guid iid)
    //    {
    //        global::System.Type result;

    //        if (FTypes.TryGetValue(iid, out result))
    //        {
    //            return result;
    //        }

    //        return null;
    //    }
    //}
}
