/*

MIT Creator Revision License v1.0 (MITCRL1.0)

Copyright (c) 2023 Tomasz Szynkar (tsx4k [TSX], tszynkar@tlen.pl, https://github.com/tsx4k)

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute copies of the Software
and to permit persons to whom the Software is furnished to do so, 
subject to the following conditions:

1. The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.
2. There are no permissions, and/or no rights to fork, make similar Software,
sublicense, and/or sell copies of the Software, and/or any part of it.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.

*/
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;

namespace WIMMapp.Utils
{

    public class VirtualDesktopManager
    {
        public bool IsWindowPinned(IntPtr hWnd)
        {
            try
            {
                return DesktopManager1803.IsWindowPinned(hWnd);
            } catch { }
            try
            {
                return DesktopManager10240.IsWindowPinned(hWnd);
            }
            catch { }
            try
            {
                return DesktopManager1121H2.IsWindowPinned(hWnd);
            }
            catch { }
            try
            {
                return DesktopManager11.IsWindowPinned(hWnd);
            }
            catch { }
            try
            {
                return DesktopManager1607.IsWindowPinned(hWnd);
            }
            catch { }
            try
            {
                return DesktopManager11Insider.IsWindowPinned(hWnd);
            }
            catch { }
            try
            {
                return DesktopManagerS2022.IsWindowPinned(hWnd);
            }
            catch { }
            try
            {
                return DesktopManager22H2.IsWindowPinned(hWnd);
            }
            catch { }
            return false;
        }

        // pin window to all desktops
        public void PinWindow(IntPtr hWnd)
        {
            try
            {
                DesktopManager1803.PinWindow(hWnd);
            } catch { }
            try
            {
                DesktopManager10240.PinWindow(hWnd);
            }
            catch { }
            try
            {
                DesktopManager1121H2.PinWindow(hWnd);
            }
            catch { }
            try
            {
                DesktopManager11.PinWindow(hWnd);
            }
            catch { }
            try
            {
                DesktopManager1607.PinWindow(hWnd);
            }
            catch { }
            try
            {
                DesktopManager11Insider.PinWindow(hWnd);
            }
            catch { }
            try
            {
                DesktopManagerS2022.PinWindow(hWnd);
            }
            catch { }
            try
            {
                DesktopManager22H2.PinWindow(hWnd);
            }
            catch { }
        }

        // unpin window from all desktops
        public void UnpinWindow(IntPtr hWnd)
        {
            try
            {
                DesktopManager1803.UnpinWindow(hWnd);
            } catch { }
            try
            {
                DesktopManager10240.UnpinWindow(hWnd);
            }
            catch { }
            try
            {
                DesktopManager1121H2.UnpinWindow(hWnd);
            }
            catch { }
            try
            {
                DesktopManager11.UnpinWindow(hWnd);
            }
            catch { }
            try
            {
                DesktopManager1607.UnpinWindow(hWnd);
            }
            catch { }
            try
            {
                DesktopManager11Insider.UnpinWindow(hWnd);
            }
            catch { }
            try
            {
                DesktopManagerS2022.UnpinWindow(hWnd);
            }
            catch { }
            try
            {
                DesktopManager22H2.UnpinWindow(hWnd);
            }
            catch { }
        }
    }

    #region COM API
    internal static class Guids
    {
        public static readonly Guid CLSID_ImmersiveShell1803 = new Guid("C2F03A33-21F5-47FA-B4BB-156362A2F239");
        public static readonly Guid CLSID_VirtualDesktopPinnedApps1803 = new Guid("B5A399E7-1C87-46B8-88E9-FC5747B171BD");

        public static readonly Guid CLSID_ImmersiveShell10240 = new Guid("C2F03A33-21F5-47FA-B4BB-156362A2F239");
        public static readonly Guid CLSID_VirtualDesktopPinnedApps10240 = new Guid("B5A399E7-1C87-46B8-88E9-FC5747B171BD");

        public static readonly Guid CLSID_ImmersiveShell1121H2 = new Guid("C2F03A33-21F5-47FA-B4BB-156362A2F239");
        public static readonly Guid CLSID_VirtualDesktopPinnedApps1121H2 = new Guid("B5A399E7-1C87-46B8-88E9-FC5747B171BD");

        public static readonly Guid CLSID_ImmersiveShell11 = new Guid("C2F03A33-21F5-47FA-B4BB-156362A2F239");
        public static readonly Guid CLSID_VirtualDesktopPinnedApps11 = new Guid("B5A399E7-1C87-46B8-88E9-FC5747B171BD");

        public static readonly Guid CLSID_ImmersiveShell1607 = new Guid("C2F03A33-21F5-47FA-B4BB-156362A2F239");
        public static readonly Guid CLSID_VirtualDesktopPinnedApps1607 = new Guid("B5A399E7-1C87-46B8-88E9-FC5747B171BD");

        public static readonly Guid CLSID_ImmersiveShell11Insider = new Guid("C2F03A33-21F5-47FA-B4BB-156362A2F239");
        public static readonly Guid CLSID_VirtualDesktopPinnedApps11Insider = new Guid("B5A399E7-1C87-46B8-88E9-FC5747B171BD");

        public static readonly Guid CLSID_ImmersiveShellS2022 = new Guid("C2F03A33-21F5-47FA-B4BB-156362A2F239");
        public static readonly Guid CLSID_VirtualDesktopPinnedAppsS2022 = new Guid("B5A399E7-1C87-46B8-88E9-FC5747B171BD");

        public static readonly Guid CLSID_ImmersiveShell22H2 = new Guid("C2F03A33-21F5-47FA-B4BB-156362A2F239");
        public static readonly Guid CLSID_VirtualDesktopPinnedApps22H2 = new Guid("B5A399E7-1C87-46B8-88E9-FC5747B171BD");
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct Size
    {
        public int X;
        public int Y;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct Rect
    {
        public int Left;
        public int Top;
        public int Right;
        public int Bottom;
    }

    public enum APPLICATION_VIEW_CLOAK_TYPE : int
    {
        AVCT_NONE = 0,
        AVCT_DEFAULT = 1,
        AVCT_VIRTUAL_DESKTOP = 2
    }



    public enum APPLICATION_VIEW_COMPATIBILITY_POLICY : int
    {
        AVCP_NONE = 0,
        AVCP_SMALL_SCREEN = 1,
        AVCP_TABLET_SMALL_SCREEN = 2,
        AVCP_VERY_SMALL_SCREEN = 3,
        AVCP_HIGH_SCALE_FACTOR = 4
    }




    [ComImport]
    [InterfaceType(ComInterfaceType.InterfaceIsIInspectable)]
    [Guid("372E1D3B-38D3-42E4-A15B-8AB2B178F513")]
    internal interface IApplicationView11Insider
    {
        int SetFocus();
        int SwitchTo();
        int TryInvokeBack(IntPtr /* IAsyncCallback* */ callback);
        int GetThumbnailWindow(out IntPtr hwnd);
        int GetMonitor(out IntPtr /* IImmersiveMonitor */ immersiveMonitor);
        int GetVisibility(out int visibility);
        int SetCloak(APPLICATION_VIEW_CLOAK_TYPE cloakType, int unknown);
        int GetPosition(ref Guid guid /* GUID for IApplicationViewPosition */, out IntPtr /* IApplicationViewPosition** */ position);
        int SetPosition(ref IntPtr /* IApplicationViewPosition* */ position);
        int InsertAfterWindow(IntPtr hwnd);
        int GetExtendedFramePosition(out Rect rect);
        int GetAppUserModelId([MarshalAs(UnmanagedType.LPWStr)] out string id);
        int SetAppUserModelId(string id);
        int IsEqualByAppUserModelId(string id, out int result);
        int GetViewState(out uint state);
        int SetViewState(uint state);
        int GetNeediness(out int neediness);
        int GetLastActivationTimestamp(out ulong timestamp);
        int SetLastActivationTimestamp(ulong timestamp);
        int GetVirtualDesktopId(out Guid guid);
        int SetVirtualDesktopId(ref Guid guid);
        int GetShowInSwitchers(out int flag);
        int SetShowInSwitchers(int flag);
        int GetScaleFactor(out int factor);
        int CanReceiveInput(out bool canReceiveInput);
        int GetCompatibilityPolicyType(out APPLICATION_VIEW_COMPATIBILITY_POLICY flags);
        int SetCompatibilityPolicyType(APPLICATION_VIEW_COMPATIBILITY_POLICY flags);
        int GetSizeConstraints(IntPtr /* IImmersiveMonitor* */ monitor, out Size size1, out Size size2);
        int GetSizeConstraintsForDpi(uint uint1, out Size size1, out Size size2);
        int SetSizeConstraintsForDpi(ref uint uint1, ref Size size1, ref Size size2);
        int OnMinSizePreferencesUpdated(IntPtr hwnd);
        int ApplyOperation(IntPtr /* IApplicationViewOperation* */ operation);
        int IsTray(out bool isTray);
        int IsInHighZOrderBand(out bool isInHighZOrderBand);
        int IsSplashScreenPresented(out bool isSplashScreenPresented);
        int Flash();
        int GetRootSwitchableOwner(out IApplicationView11Insider rootSwitchableOwner);
        int EnumerateOwnershipTree(out IObjectArray ownershipTree);
        int GetEnterpriseId([MarshalAs(UnmanagedType.LPWStr)] out string enterpriseId);
        int IsMirrored(out bool isMirrored);
        int Unknown1(out int unknown);
        int Unknown2(out int unknown);
        int Unknown3(out int unknown);
        int Unknown4(out int unknown);
        int Unknown5(out int unknown);
        int Unknown6(int unknown);
        int Unknown7();
        int Unknown8(out int unknown);
        int Unknown9(int unknown);
        int Unknown10(int unknownX, int unknownY);
        int Unknown11(int unknown);
        int Unknown12(out Size size1);
    }

    [ComImport]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [Guid("1841C6D7-4F9D-42C0-AF41-8747538F10E5")]
    internal interface IApplicationViewCollection11Insider
    {
        int GetViews(out IObjectArray array);
        int GetViewsByZOrder(out IObjectArray array);
        int GetViewsByAppUserModelId(string id, out IObjectArray array);
        int GetViewForHwnd(IntPtr hwnd, out IApplicationView11Insider view);
        int GetViewForApplication(object application, out IApplicationView11Insider view);
        int GetViewForAppUserModelId(string id, out IApplicationView11Insider view);
        int GetViewInFocus(out IntPtr view);
        int Unknown1(out IntPtr view);
        void RefreshCollection();
        int RegisterForApplicationViewChanges(object listener, out int cookie);
        int UnregisterForApplicationViewChanges(int cookie);
    }

    [ComImport]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [Guid("4CE81583-1E4C-4632-A621-07A53543148F")]
    internal interface IVirtualDesktopPinnedApps11Insider
    {
        bool IsAppIdPinned(string appId);
        void PinAppID(string appId);
        void UnpinAppID(string appId);
        bool IsViewPinned(IApplicationView11Insider applicationView);
        void PinView(IApplicationView11Insider applicationView);
        void UnpinView(IApplicationView11Insider applicationView);
    }





    [ComImport]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [Guid("9AC0B5C8-1484-4C5B-9533-4134A0F97CEA")]
    internal interface IApplicationView1607
    {
        int SetFocus();
        int SwitchTo();
        int TryInvokeBack(IntPtr /* IAsyncCallback* */ callback);
        int GetThumbnailWindow(out IntPtr hwnd);
        int GetMonitor(out IntPtr /* IImmersiveMonitor */ immersiveMonitor);
        int GetVisibility(out int visibility);
        int SetCloak(APPLICATION_VIEW_CLOAK_TYPE cloakType, int unknown);
        int GetPosition(ref Guid guid /* GUID for IApplicationViewPosition */, out IntPtr /* IApplicationViewPosition** */ position);
        int SetPosition(ref IntPtr /* IApplicationViewPosition* */ position);
        int InsertAfterWindow(IntPtr hwnd);
        int GetExtendedFramePosition(out Rect rect);
        int GetAppUserModelId([MarshalAs(UnmanagedType.LPWStr)] out string id);
        int SetAppUserModelId(string id);
        int IsEqualByAppUserModelId(string id, out int result);
        int GetViewState(out uint state);
        int SetViewState(uint state);
        int GetNeediness(out int neediness);
        int GetLastActivationTimestamp(out ulong timestamp);
        int SetLastActivationTimestamp(ulong timestamp);
        int GetVirtualDesktopId(out Guid guid);
        int SetVirtualDesktopId(ref Guid guid);
        int GetShowInSwitchers(out int flag);
        int SetShowInSwitchers(int flag);
        int GetScaleFactor(out int factor);
        int CanReceiveInput(out bool canReceiveInput);
        int GetCompatibilityPolicyType(out APPLICATION_VIEW_COMPATIBILITY_POLICY flags);
        int SetCompatibilityPolicyType(APPLICATION_VIEW_COMPATIBILITY_POLICY flags);
        int GetPositionPriority(out IntPtr /* IShellPositionerPriority** */ priority);
        int SetPositionPriority(IntPtr /* IShellPositionerPriority* */ priority);
        int GetSizeConstraints(IntPtr /* IImmersiveMonitor* */ monitor, out Size size1, out Size size2);
        int GetSizeConstraintsForDpi(uint uint1, out Size size1, out Size size2);
        int SetSizeConstraintsForDpi(ref uint uint1, ref Size size1, ref Size size2);
        int QuerySizeConstraintsFromApp();
        int OnMinSizePreferencesUpdated(IntPtr hwnd);
        int ApplyOperation(IntPtr /* IApplicationViewOperation* */ operation);
        int IsTray(out bool isTray);
        int IsInHighZOrderBand(out bool isInHighZOrderBand);
        int IsSplashScreenPresented(out bool isSplashScreenPresented);
        int Flash();
        int GetRootSwitchableOwner(out IApplicationView1607 rootSwitchableOwner);
        int EnumerateOwnershipTree(out IObjectArray ownershipTree);
        int GetEnterpriseId([MarshalAs(UnmanagedType.LPWStr)] out string enterpriseId);
        int IsMirrored(out bool isMirrored);
    }

    [ComImport]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [Guid("2C08ADF0-A386-4B35-9250-0FE183476FCC")]
    internal interface IApplicationViewCollection1607
    {
        int GetViews(out IObjectArray array);
        int GetViewsByZOrder(out IObjectArray array);
        int GetViewsByAppUserModelId(string id, out IObjectArray array);
        int GetViewForHwnd(IntPtr hwnd, out IApplicationView1607 view);
        int GetViewForApplication(object application, out IApplicationView1607 view);
        int GetViewForAppUserModelId(string id, out IApplicationView1607 view);
        int GetViewInFocus(out IntPtr view);
        void RefreshCollection();
        int RegisterForApplicationViewChanges(object listener, out int cookie);
        int RegisterForApplicationViewPositionChanges(object listener, out int cookie);
        int UnregisterForApplicationViewChanges(int cookie);
    }

    [ComImport]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [Guid("4CE81583-1E4C-4632-A621-07A53543148F")]
    internal interface IVirtualDesktopPinnedApps1607
    {
        bool IsAppIdPinned(string appId);
        void PinAppID(string appId);
        void UnpinAppID(string appId);
        bool IsViewPinned(IApplicationView1607 applicationView);
        void PinView(IApplicationView1607 applicationView);
        void UnpinView(IApplicationView1607 applicationView);
    }


    [ComImport]
    [InterfaceType(ComInterfaceType.InterfaceIsIInspectable)]
    [Guid("871F602A-2B58-42B4-8C4B-6C43D642C06F")]
    internal interface IApplicationView1803
    {
        int SetFocus();
        int SwitchTo();
        int TryInvokeBack(IntPtr /* IAsyncCallback* */ callback);
        int GetThumbnailWindow(out IntPtr hwnd);
        int GetMonitor(out IntPtr /* IImmersiveMonitor */ immersiveMonitor);
        int GetVisibility(out int visibility);
        int SetCloak(APPLICATION_VIEW_CLOAK_TYPE cloakType, int unknown);
        int GetPosition(ref Guid guid /* GUID for IApplicationViewPosition */, out IntPtr /* IApplicationViewPosition** */ position);
        int SetPosition(ref IntPtr /* IApplicationViewPosition* */ position);
        int InsertAfterWindow(IntPtr hwnd);
        int GetExtendedFramePosition(out Rect rect);
        int GetAppUserModelId([MarshalAs(UnmanagedType.LPWStr)] out string id);
        int SetAppUserModelId(string id);
        int IsEqualByAppUserModelId(string id, out int result);
        int GetViewState(out uint state);
        int SetViewState(uint state);
        int GetNeediness(out int neediness);
        int GetLastActivationTimestamp(out ulong timestamp);
        int SetLastActivationTimestamp(ulong timestamp);
        int GetVirtualDesktopId(out Guid guid);
        int SetVirtualDesktopId(ref Guid guid);
        int GetShowInSwitchers(out int flag);
        int SetShowInSwitchers(int flag);
        int GetScaleFactor(out int factor);
        int CanReceiveInput(out bool canReceiveInput);
        int GetCompatibilityPolicyType(out APPLICATION_VIEW_COMPATIBILITY_POLICY flags);
        int SetCompatibilityPolicyType(APPLICATION_VIEW_COMPATIBILITY_POLICY flags);
        int GetSizeConstraints(IntPtr /* IImmersiveMonitor* */ monitor, out Size size1, out Size size2);
        int GetSizeConstraintsForDpi(uint uint1, out Size size1, out Size size2);
        int SetSizeConstraintsForDpi(ref uint uint1, ref Size size1, ref Size size2);
        int OnMinSizePreferencesUpdated(IntPtr hwnd);
        int ApplyOperation(IntPtr /* IApplicationViewOperation* */ operation);
        int IsTray(out bool isTray);
        int IsInHighZOrderBand(out bool isInHighZOrderBand);
        int IsSplashScreenPresented(out bool isSplashScreenPresented);
        int Flash();
        int GetRootSwitchableOwner(out IApplicationView1803 rootSwitchableOwner);
        int EnumerateOwnershipTree(out IObjectArray ownershipTree);
        int GetEnterpriseId([MarshalAs(UnmanagedType.LPWStr)] out string enterpriseId);
        int IsMirrored(out bool isMirrored);
        int Unknown1(out int unknown);
        int Unknown2(out int unknown);
        int Unknown3(out int unknown);
        int Unknown4(out int unknown);
    }

    [ComImport]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [Guid("2C08ADF0-A386-4B35-9250-0FE183476FCC")]
    internal interface IApplicationViewCollection1803
    {
        int GetViews(out IObjectArray array);
        int GetViewsByZOrder(out IObjectArray array);
        int GetViewsByAppUserModelId(string id, out IObjectArray array);
        int GetViewForHwnd(IntPtr hwnd, out IApplicationView1803 view);
        int GetViewForApplication(object application, out IApplicationView1803 view);
        int GetViewForAppUserModelId(string id, out IApplicationView1803 view);
        int GetViewInFocus(out IntPtr view);
        int Unknown1(out IntPtr view);
        void RefreshCollection();
        int RegisterForApplicationViewChanges(object listener, out int cookie);
        int RegisterForApplicationViewPositionChanges(object listener, out int cookie);
        int UnregisterForApplicationViewChanges(int cookie);
    }

    [ComImport]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [Guid("1841c6d7-4f9d-42c0-af41-8747538f10e5")]
    internal interface IApplicationViewCollection10240
    {
        IObjectArray GetViews();

        IObjectArray GetViewsByZOrder();

        IObjectArray GetViewsByAppUserModelId(string id);

        IApplicationView10240 GetViewForHwnd(IntPtr hwnd);

        IApplicationView10240 GetViewForApplication(object application);

        IApplicationView10240 GetViewForAppUserModelId(string id);

        IntPtr GetViewInFocus();

        void RefreshCollection();

        int RegisterForApplicationViewChanges(object listener);

        int RegisterForApplicationViewPositionChanges(object listener);

        void UnregisterForApplicationViewChanges(int cookie);
    }

    [ComImport]
    [Guid("372e1d3b-38d3-42e4-a15b-8ab2b178f513")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IApplicationView10240
    {
        void Proc3();

        void Proc4();

        void Proc5();

        void SetFocus();

        void SwitchTo();

        void TryInvokeBack(IntPtr callback);

        IntPtr GetThumbnailWindow();

        IntPtr GetMonitor();

        int GetVisibility();

        void SetCloak(APPLICATION_VIEW_CLOAK_TYPE cloakType, int unknown);

        IntPtr GetPosition(in Guid guid, out IntPtr position);

        void SetPosition(in IntPtr position);

        void InsertAfterWindow(IntPtr hwnd);

        Rect GetExtendedFramePosition();

        [return: MarshalAs(UnmanagedType.LPWStr)]
        string GetAppUserModelId();

        void SetAppUserModelId([MarshalAs(UnmanagedType.LPWStr)] string id);

        bool IsEqualByAppUserModelId(string id);

        uint GetViewState();

        void SetViewState(uint state);

        int GetNeediness();

        ulong GetLastActivationTimestamp();

        void SetLastActivationTimestamp(ulong timestamp);

        Guid GetVirtualDesktopId();

        void SetVirtualDesktopId(in Guid guid);

        int GetShowInSwitchers();

        void SetShowInSwitchers(int flag);

        int GetScaleFactor();

        bool CanReceiveInput();

        APPLICATION_VIEW_COMPATIBILITY_POLICY GetCompatibilityPolicyType();

        void SetCompatibilityPolicyType(APPLICATION_VIEW_COMPATIBILITY_POLICY flags);

        IntPtr GetPositionPriority();

        void SetPositionPriority(IntPtr priority);

        void GetSizeConstraints(IntPtr monitor, out Size size1, out Size size2);

        void GetSizeConstraintsForDpi(uint uint1, out Size size1, out Size size2);

        void SetSizeConstraintsForDpi(ref uint uint1, in Size size1, in Size size2);

        int QuerySizeConstraintsFromApp();

        void OnMinSizePreferencesUpdated(IntPtr hwnd);

        void ApplyOperation(IntPtr operation);

        bool IsTray();

        bool IsInHighZOrderBand();

        bool IsSplashScreenPresented();

        void Flash();

        IApplicationView10240 GetRootSwitchableOwner();

        IObjectArray EnumerateOwnershipTree();

        [return: MarshalAs(UnmanagedType.LPWStr)]
        string GetEnterpriseId();

        bool IsMirrored();
    }


    [ComImport]
    [InterfaceType(ComInterfaceType.InterfaceIsIInspectable)]
    [Guid("372E1D3B-38D3-42E4-A15B-8AB2B178F513")]
    internal interface IApplicationView1121H2
    {
        int SetFocus();
        int SwitchTo();
        int TryInvokeBack(IntPtr /* IAsyncCallback* */ callback);
        int GetThumbnailWindow(out IntPtr hwnd);
        int GetMonitor(out IntPtr /* IImmersiveMonitor */ immersiveMonitor);
        int GetVisibility(out int visibility);
        int SetCloak(APPLICATION_VIEW_CLOAK_TYPE cloakType, int unknown);
        int GetPosition(ref Guid guid /* GUID for IApplicationViewPosition */, out IntPtr /* IApplicationViewPosition** */ position);
        int SetPosition(ref IntPtr /* IApplicationViewPosition* */ position);
        int InsertAfterWindow(IntPtr hwnd);
        int GetExtendedFramePosition(out Rect rect);
        int GetAppUserModelId([MarshalAs(UnmanagedType.LPWStr)] out string id);
        int SetAppUserModelId(string id);
        int IsEqualByAppUserModelId(string id, out int result);
        int GetViewState(out uint state);
        int SetViewState(uint state);
        int GetNeediness(out int neediness);
        int GetLastActivationTimestamp(out ulong timestamp);
        int SetLastActivationTimestamp(ulong timestamp);
        int GetVirtualDesktopId(out Guid guid);
        int SetVirtualDesktopId(ref Guid guid);
        int GetShowInSwitchers(out int flag);
        int SetShowInSwitchers(int flag);
        int GetScaleFactor(out int factor);
        int CanReceiveInput(out bool canReceiveInput);
        int GetCompatibilityPolicyType(out APPLICATION_VIEW_COMPATIBILITY_POLICY flags);
        int SetCompatibilityPolicyType(APPLICATION_VIEW_COMPATIBILITY_POLICY flags);
        int GetSizeConstraints(IntPtr /* IImmersiveMonitor* */ monitor, out Size size1, out Size size2);
        int GetSizeConstraintsForDpi(uint uint1, out Size size1, out Size size2);
        int SetSizeConstraintsForDpi(ref uint uint1, ref Size size1, ref Size size2);
        int OnMinSizePreferencesUpdated(IntPtr hwnd);
        int ApplyOperation(IntPtr /* IApplicationViewOperation* */ operation);
        int IsTray(out bool isTray);
        int IsInHighZOrderBand(out bool isInHighZOrderBand);
        int IsSplashScreenPresented(out bool isSplashScreenPresented);
        int Flash();
        int GetRootSwitchableOwner(out IApplicationView1121H2 rootSwitchableOwner);
        int EnumerateOwnershipTree(out IObjectArray ownershipTree);
        int GetEnterpriseId([MarshalAs(UnmanagedType.LPWStr)] out string enterpriseId);
        int IsMirrored(out bool isMirrored);
        int Unknown1(out int unknown);
        int Unknown2(out int unknown);
        int Unknown3(out int unknown);
        int Unknown4(out int unknown);
        int Unknown5(out int unknown);
        int Unknown6(int unknown);
        int Unknown7();
        int Unknown8(out int unknown);
        int Unknown9(int unknown);
        int Unknown10(int unknownX, int unknownY);
        int Unknown11(int unknown);
        int Unknown12(out Size size1);
    }

    [ComImport]
    [InterfaceType(ComInterfaceType.InterfaceIsIInspectable)]
    [Guid("372E1D3B-38D3-42E4-A15B-8AB2B178F513")]
    internal interface IApplicationView11
    {
        int SetFocus();
        int SwitchTo();
        int TryInvokeBack(IntPtr /* IAsyncCallback* */ callback);
        int GetThumbnailWindow(out IntPtr hwnd);
        int GetMonitor(out IntPtr /* IImmersiveMonitor */ immersiveMonitor);
        int GetVisibility(out int visibility);
        int SetCloak(APPLICATION_VIEW_CLOAK_TYPE cloakType, int unknown);
        int GetPosition(ref Guid guid /* GUID for IApplicationViewPosition */, out IntPtr /* IApplicationViewPosition** */ position);
        int SetPosition(ref IntPtr /* IApplicationViewPosition* */ position);
        int InsertAfterWindow(IntPtr hwnd);
        int GetExtendedFramePosition(out Rect rect);
        int GetAppUserModelId([MarshalAs(UnmanagedType.LPWStr)] out string id);
        int SetAppUserModelId(string id);
        int IsEqualByAppUserModelId(string id, out int result);
        int GetViewState(out uint state);
        int SetViewState(uint state);
        int GetNeediness(out int neediness);
        int GetLastActivationTimestamp(out ulong timestamp);
        int SetLastActivationTimestamp(ulong timestamp);
        int GetVirtualDesktopId(out Guid guid);
        int SetVirtualDesktopId(ref Guid guid);
        int GetShowInSwitchers(out int flag);
        int SetShowInSwitchers(int flag);
        int GetScaleFactor(out int factor);
        int CanReceiveInput(out bool canReceiveInput);
        int GetCompatibilityPolicyType(out APPLICATION_VIEW_COMPATIBILITY_POLICY flags);
        int SetCompatibilityPolicyType(APPLICATION_VIEW_COMPATIBILITY_POLICY flags);
        int GetSizeConstraints(IntPtr /* IImmersiveMonitor* */ monitor, out Size size1, out Size size2);
        int GetSizeConstraintsForDpi(uint uint1, out Size size1, out Size size2);
        int SetSizeConstraintsForDpi(ref uint uint1, ref Size size1, ref Size size2);
        int OnMinSizePreferencesUpdated(IntPtr hwnd);
        int ApplyOperation(IntPtr /* IApplicationViewOperation* */ operation);
        int IsTray(out bool isTray);
        int IsInHighZOrderBand(out bool isInHighZOrderBand);
        int IsSplashScreenPresented(out bool isSplashScreenPresented);
        int Flash();
        int GetRootSwitchableOwner(out IApplicationView11 rootSwitchableOwner);
        int EnumerateOwnershipTree(out IObjectArray ownershipTree);
        int GetEnterpriseId([MarshalAs(UnmanagedType.LPWStr)] out string enterpriseId);
        int IsMirrored(out bool isMirrored);
        int Unknown1(out int unknown);
        int Unknown2(out int unknown);
        int Unknown3(out int unknown);
        int Unknown4(out int unknown);
        int Unknown5(out int unknown);
        int Unknown6(int unknown);
        int Unknown7();
        int Unknown8(out int unknown);
        int Unknown9(int unknown);
        int Unknown10(int unknownX, int unknownY);
        int Unknown11(int unknown);
        int Unknown12(out Size size1);
    }

    [ComImport]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [Guid("1841C6D7-4F9D-42C0-AF41-8747538F10E5")]
    internal interface IApplicationViewCollection11
    {
        int GetViews(out IObjectArray array);
        int GetViewsByZOrder(out IObjectArray array);
        int GetViewsByAppUserModelId(string id, out IObjectArray array);
        int GetViewForHwnd(IntPtr hwnd, out IApplicationView11 view);
        int GetViewForApplication(object application, out IApplicationView11 view);
        int GetViewForAppUserModelId(string id, out IApplicationView11 view);
        int GetViewInFocus(out IntPtr view);
        int Unknown1(out IntPtr view);
        void RefreshCollection();
        int RegisterForApplicationViewChanges(object listener, out int cookie);
        int UnregisterForApplicationViewChanges(int cookie);
    }

    [ComImport]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [Guid("4CE81583-1E4C-4632-A621-07A53543148F")]
    internal interface IVirtualDesktopPinnedApps11
    {
        bool IsAppIdPinned(string appId);
        void PinAppID(string appId);
        void UnpinAppID(string appId);
        bool IsViewPinned(IApplicationView11 applicationView);
        void PinView(IApplicationView11 applicationView);
        void UnpinView(IApplicationView11 applicationView);
    }



    [ComImport]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [Guid("1841C6D7-4F9D-42C0-AF41-8747538F10E5")]
    internal interface IApplicationViewCollection1121H2
    {
        int GetViews(out IObjectArray array);
        int GetViewsByZOrder(out IObjectArray array);
        int GetViewsByAppUserModelId(string id, out IObjectArray array);
        int GetViewForHwnd(IntPtr hwnd, out IApplicationView1121H2 view);
        int GetViewForApplication(object application, out IApplicationView1121H2 view);
        int GetViewForAppUserModelId(string id, out IApplicationView1121H2 view);
        int GetViewInFocus(out IntPtr view);
        int Unknown1(out IntPtr view);
        void RefreshCollection();
        int RegisterForApplicationViewChanges(object listener, out int cookie);
        int UnregisterForApplicationViewChanges(int cookie);
    }



    [ComImport]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [Guid("4CE81583-1E4C-4632-A621-07A53543148F")]
    internal interface IVirtualDesktopPinnedApps1803
    {
        bool IsAppIdPinned(string appId);
        void PinAppID(string appId);
        void UnpinAppID(string appId);
        bool IsViewPinned(IApplicationView1803 applicationView);
        void PinView(IApplicationView1803 applicationView);
        void UnpinView(IApplicationView1803 applicationView);
    }

    [ComImport]
    [Guid("4ce81583-1e4c-4632-a621-07a53543148f")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IVirtualDesktopPinnedApps10240
    {
        bool IsAppIdPinned(string appId);

        void PinAppID(string appId);

        void UnpinAppID(string appId);

        bool IsViewPinned(IApplicationView10240 applicationView);

        void PinView(IApplicationView10240 applicationView);

        void UnpinView(IApplicationView10240 applicationView);
    }


    [ComImport]
    [InterfaceType(ComInterfaceType.InterfaceIsIInspectable)]
    [Guid("372E1D3B-38D3-42E4-A15B-8AB2B178F513")]
    internal interface IApplicationView22H2
    {
        int SetFocus();
        int SwitchTo();
        int TryInvokeBack(IntPtr /* IAsyncCallback* */ callback);
        int GetThumbnailWindow(out IntPtr hwnd);
        int GetMonitor(out IntPtr /* IImmersiveMonitor */ immersiveMonitor);
        int GetVisibility(out int visibility);
        int SetCloak(APPLICATION_VIEW_CLOAK_TYPE cloakType, int unknown);
        int GetPosition(ref Guid guid /* GUID for IApplicationViewPosition */, out IntPtr /* IApplicationViewPosition** */ position);
        int SetPosition(ref IntPtr /* IApplicationViewPosition* */ position);
        int InsertAfterWindow(IntPtr hwnd);
        int GetExtendedFramePosition(out Rect rect);
        int GetAppUserModelId([MarshalAs(UnmanagedType.LPWStr)] out string id);
        int SetAppUserModelId(string id);
        int IsEqualByAppUserModelId(string id, out int result);
        int GetViewState(out uint state);
        int SetViewState(uint state);
        int GetNeediness(out int neediness);
        int GetLastActivationTimestamp(out ulong timestamp);
        int SetLastActivationTimestamp(ulong timestamp);
        int GetVirtualDesktopId(out Guid guid);
        int SetVirtualDesktopId(ref Guid guid);
        int GetShowInSwitchers(out int flag);
        int SetShowInSwitchers(int flag);
        int GetScaleFactor(out int factor);
        int CanReceiveInput(out bool canReceiveInput);
        int GetCompatibilityPolicyType(out APPLICATION_VIEW_COMPATIBILITY_POLICY flags);
        int SetCompatibilityPolicyType(APPLICATION_VIEW_COMPATIBILITY_POLICY flags);
        int GetSizeConstraints(IntPtr /* IImmersiveMonitor* */ monitor, out Size size1, out Size size2);
        int GetSizeConstraintsForDpi(uint uint1, out Size size1, out Size size2);
        int SetSizeConstraintsForDpi(ref uint uint1, ref Size size1, ref Size size2);
        int OnMinSizePreferencesUpdated(IntPtr hwnd);
        int ApplyOperation(IntPtr /* IApplicationViewOperation* */ operation);
        int IsTray(out bool isTray);
        int IsInHighZOrderBand(out bool isInHighZOrderBand);
        int IsSplashScreenPresented(out bool isSplashScreenPresented);
        int Flash();
        int GetRootSwitchableOwner(out IApplicationView22H2 rootSwitchableOwner);
        int EnumerateOwnershipTree(out IObjectArray ownershipTree);
        int GetEnterpriseId([MarshalAs(UnmanagedType.LPWStr)] out string enterpriseId);
        int IsMirrored(out bool isMirrored);
        int Unknown1(out int unknown);
        int Unknown2(out int unknown);
        int Unknown3(out int unknown);
        int Unknown4(out int unknown);
        int Unknown5(out int unknown);
        int Unknown6(int unknown);
        int Unknown7();
        int Unknown8(out int unknown);
        int Unknown9(int unknown);
        int Unknown10(int unknownX, int unknownY);
        int Unknown11(int unknown);
        int Unknown12(out Size size1);
    }

    [ComImport]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [Guid("1841C6D7-4F9D-42C0-AF41-8747538F10E5")]
    internal interface IApplicationViewCollection22H2
    {
        int GetViews(out IObjectArray array);
        int GetViewsByZOrder(out IObjectArray array);
        int GetViewsByAppUserModelId(string id, out IObjectArray array);
        int GetViewForHwnd(IntPtr hwnd, out IApplicationView22H2 view);
        int GetViewForApplication(object application, out IApplicationView22H2 view);
        int GetViewForAppUserModelId(string id, out IApplicationView22H2 view);
        int GetViewInFocus(out IntPtr view);
        int Unknown1(out IntPtr view);
        void RefreshCollection();
        int RegisterForApplicationViewChanges(object listener, out int cookie);
        int UnregisterForApplicationViewChanges(int cookie);
    }

    [ComImport]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [Guid("4CE81583-1E4C-4632-A621-07A53543148F")]
    internal interface IVirtualDesktopPinnedApps22H2
    {
        bool IsAppIdPinned(string appId);
        void PinAppID(string appId);
        void UnpinAppID(string appId);
        bool IsViewPinned(IApplicationView22H2 applicationView);
        void PinView(IApplicationView22H2 applicationView);
        void UnpinView(IApplicationView22H2 applicationView);
    }


    [ComImport]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [Guid("92CA9DCD-5622-4BBA-A805-5E9F541BD8C9")]
    public interface IObjectArray
    {
        void GetCount(out int count);
        void GetAt(int index, ref Guid iid, [MarshalAs(UnmanagedType.Interface)] out object obj);
    }

    [ComImport]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [Guid("6D5140C1-7436-11CE-8034-00AA006009FA")]
    internal interface IServiceProvider10
    {
        [return: MarshalAs(UnmanagedType.IUnknown)]
        object QueryService(ref Guid service, ref Guid riid);
    }
    #endregion

    #region COM wrapper
    internal static class DesktopManager1803
    {
        static DesktopManager1803()
        {
            var shell = (IServiceProvider10)Activator.CreateInstance(Type.GetTypeFromCLSID(Guids.CLSID_ImmersiveShell1803));
            ApplicationViewCollection1803 = (IApplicationViewCollection1803)shell.QueryService(typeof(IApplicationViewCollection1803).GUID, typeof(IApplicationViewCollection1803).GUID);
            VirtualDesktopPinnedApps1803 = (IVirtualDesktopPinnedApps1803)shell.QueryService(Guids.CLSID_VirtualDesktopPinnedApps1803, typeof(IVirtualDesktopPinnedApps1803).GUID);
        }

        static IApplicationView1803 GetApplicationView(IntPtr hWnd)
        {
            DesktopManager1803.ApplicationViewCollection1803.GetViewForHwnd(hWnd, out IApplicationView1803 view);
            return view;
        }

        internal static IApplicationViewCollection1803 ApplicationViewCollection1803;
        internal static IVirtualDesktopPinnedApps1803 VirtualDesktopPinnedApps1803;

        // return true if window is pinned to all desktops
        public static bool IsWindowPinned(IntPtr hWnd)
        {
            if (hWnd == IntPtr.Zero) throw new ArgumentNullException();
            return DesktopManager1803.VirtualDesktopPinnedApps1803.IsViewPinned(GetApplicationView(hWnd));
        }

        // pin window to all desktops
        public static void PinWindow(IntPtr hWnd)
        {
            if (hWnd == IntPtr.Zero) throw new ArgumentNullException();
            var view = GetApplicationView(hWnd);
            if (!DesktopManager1803.VirtualDesktopPinnedApps1803.IsViewPinned(view))
            {
                DesktopManager1803.VirtualDesktopPinnedApps1803.PinView(view);
            }
        }

        // unpin window from all desktops
        public static void UnpinWindow(IntPtr hWnd)
        {
            if (hWnd == IntPtr.Zero) throw new ArgumentNullException();
            var view = GetApplicationView(hWnd);
            if (DesktopManager1803.VirtualDesktopPinnedApps1803.IsViewPinned(view))
            {
                DesktopManager1803.VirtualDesktopPinnedApps1803.UnpinView(view);
            }
        }
    }



    internal static class DesktopManager10240
    {
        static DesktopManager10240()
        {
            var shell = (IServiceProvider10)Activator.CreateInstance(Type.GetTypeFromCLSID(Guids.CLSID_ImmersiveShell10240));
            ApplicationViewCollection10240 = (IApplicationViewCollection10240)shell.QueryService(typeof(IApplicationViewCollection10240).GUID, typeof(IApplicationViewCollection10240).GUID);
            VirtualDesktopPinnedApps10240 = (IVirtualDesktopPinnedApps10240)shell.QueryService(Guids.CLSID_VirtualDesktopPinnedApps10240, typeof(IVirtualDesktopPinnedApps10240).GUID);
        }

        static IApplicationView10240 GetApplicationView(IntPtr hWnd)
        {
            return DesktopManager10240.ApplicationViewCollection10240.GetViewForHwnd(hWnd);
        }

        internal static IApplicationViewCollection10240 ApplicationViewCollection10240;
        internal static IVirtualDesktopPinnedApps10240 VirtualDesktopPinnedApps10240;

        // return true if window is pinned to all desktops
        public static bool IsWindowPinned(IntPtr hWnd)
        {
            if (hWnd == IntPtr.Zero) throw new ArgumentNullException();
            return DesktopManager10240.VirtualDesktopPinnedApps10240.IsViewPinned(GetApplicationView(hWnd));
        }

        // pin window to all desktops
        public static void PinWindow(IntPtr hWnd)
        {
            if (hWnd == IntPtr.Zero) throw new ArgumentNullException();
            var view = GetApplicationView(hWnd);
            if (!DesktopManager10240.VirtualDesktopPinnedApps10240.IsViewPinned(view))
            {
                DesktopManager10240.VirtualDesktopPinnedApps10240.PinView(view);
            }
        }

        // unpin window from all desktops
        public static void UnpinWindow(IntPtr hWnd)
        {
            if (hWnd == IntPtr.Zero) throw new ArgumentNullException();
            var view = GetApplicationView(hWnd);
            if (DesktopManager10240.VirtualDesktopPinnedApps10240.IsViewPinned(view))
            {
                DesktopManager10240.VirtualDesktopPinnedApps10240.UnpinView(view);
            }
        }

    }

    [ComImport]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [Guid("4CE81583-1E4C-4632-A621-07A53543148F")]
    internal interface IVirtualDesktopPinnedApps1121H2
    {
        bool IsAppIdPinned(string appId);
        void PinAppID(string appId);
        void UnpinAppID(string appId);
        bool IsViewPinned(IApplicationView1121H2 applicationView);
        void PinView(IApplicationView1121H2 applicationView);
        void UnpinView(IApplicationView1121H2 applicationView);
    }




    [ComImport]
    [InterfaceType(ComInterfaceType.InterfaceIsIInspectable)]
    [Guid("372E1D3B-38D3-42E4-A15B-8AB2B178F513")]
    internal interface IApplicationViewS2022
    {
        int SetFocus();
        int SwitchTo();
        int TryInvokeBack(IntPtr /* IAsyncCallback* */ callback);
        int GetThumbnailWindow(out IntPtr hwnd);
        int GetMonitor(out IntPtr /* IImmersiveMonitor */ immersiveMonitor);
        int GetVisibility(out int visibility);
        int SetCloak(APPLICATION_VIEW_CLOAK_TYPE cloakType, int unknown);
        int GetPosition(ref Guid guid /* GUID for IApplicationViewPosition */, out IntPtr /* IApplicationViewPosition** */ position);
        int SetPosition(ref IntPtr /* IApplicationViewPosition* */ position);
        int InsertAfterWindow(IntPtr hwnd);
        int GetExtendedFramePosition(out Rect rect);
        int GetAppUserModelId([MarshalAs(UnmanagedType.LPWStr)] out string id);
        int SetAppUserModelId(string id);
        int IsEqualByAppUserModelId(string id, out int result);
        int GetViewState(out uint state);
        int SetViewState(uint state);
        int GetNeediness(out int neediness);
        int GetLastActivationTimestamp(out ulong timestamp);
        int SetLastActivationTimestamp(ulong timestamp);
        int GetVirtualDesktopId(out Guid guid);
        int SetVirtualDesktopId(ref Guid guid);
        int GetShowInSwitchers(out int flag);
        int SetShowInSwitchers(int flag);
        int GetScaleFactor(out int factor);
        int CanReceiveInput(out bool canReceiveInput);
        int GetCompatibilityPolicyType(out APPLICATION_VIEW_COMPATIBILITY_POLICY flags);
        int SetCompatibilityPolicyType(APPLICATION_VIEW_COMPATIBILITY_POLICY flags);
        int GetSizeConstraints(IntPtr /* IImmersiveMonitor* */ monitor, out Size size1, out Size size2);
        int GetSizeConstraintsForDpi(uint uint1, out Size size1, out Size size2);
        int SetSizeConstraintsForDpi(ref uint uint1, ref Size size1, ref Size size2);
        int OnMinSizePreferencesUpdated(IntPtr hwnd);
        int ApplyOperation(IntPtr /* IApplicationViewOperation* */ operation);
        int IsTray(out bool isTray);
        int IsInHighZOrderBand(out bool isInHighZOrderBand);
        int IsSplashScreenPresented(out bool isSplashScreenPresented);
        int Flash();
        int GetRootSwitchableOwner(out IApplicationViewS2022 rootSwitchableOwner);
        int EnumerateOwnershipTree(out IObjectArray ownershipTree);
        int GetEnterpriseId([MarshalAs(UnmanagedType.LPWStr)] out string enterpriseId);
        int IsMirrored(out bool isMirrored);
        int Unknown1(out int unknown);
        int Unknown2(out int unknown);
        int Unknown3(out int unknown);
        int Unknown4(out int unknown);
        int Unknown5(out int unknown);
        int Unknown6(int unknown);
        int Unknown7();
        int Unknown8(out int unknown);
        int Unknown9(int unknown);
        int Unknown10(int unknownX, int unknownY);
        int Unknown11(int unknown);
        int Unknown12(out Size size1);
    }

    [ComImport]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [Guid("1841C6D7-4F9D-42C0-AF41-8747538F10E5")]
    internal interface IApplicationViewCollectionS2022
    {
        int GetViews(out IObjectArray array);
        int GetViewsByZOrder(out IObjectArray array);
        int GetViewsByAppUserModelId(string id, out IObjectArray array);
        int GetViewForHwnd(IntPtr hwnd, out IApplicationViewS2022 view);
        int GetViewForApplication(object application, out IApplicationViewS2022 view);
        int GetViewForAppUserModelId(string id, out IApplicationViewS2022 view);
        int GetViewInFocus(out IntPtr view);
        int Unknown1(out IntPtr view);
        void RefreshCollection();
        int RegisterForApplicationViewChanges(object listener, out int cookie);
        int UnregisterForApplicationViewChanges(int cookie);
    }


    [ComImport]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [Guid("4CE81583-1E4C-4632-A621-07A53543148F")]
    internal interface IVirtualDesktopPinnedAppsS2022
    {
        bool IsAppIdPinned(string appId);
        void PinAppID(string appId);
        void UnpinAppID(string appId);
        bool IsViewPinned(IApplicationViewS2022 applicationView);
        void PinView(IApplicationViewS2022 applicationView);
        void UnpinView(IApplicationViewS2022 applicationView);
    }



    internal static class DesktopManager1121H2
    {
        static DesktopManager1121H2()
        {
            var shell = (IServiceProvider10)Activator.CreateInstance(Type.GetTypeFromCLSID(Guids.CLSID_ImmersiveShell1121H2));
            ApplicationViewCollection1121H2 = (IApplicationViewCollection1121H2)shell.QueryService(typeof(IApplicationViewCollection1121H2).GUID, typeof(IApplicationViewCollection1121H2).GUID);
            VirtualDesktopPinnedApps1121H2 = (IVirtualDesktopPinnedApps1121H2)shell.QueryService(Guids.CLSID_VirtualDesktopPinnedApps1121H2, typeof(IVirtualDesktopPinnedApps1121H2).GUID);
        }

        static IApplicationView1121H2 GetApplicationView(IntPtr hWnd)
        {
            DesktopManager1121H2.ApplicationViewCollection1121H2.GetViewForHwnd(hWnd, out IApplicationView1121H2 view);
            return view;
        }

        internal static IApplicationViewCollection1121H2 ApplicationViewCollection1121H2;
        internal static IVirtualDesktopPinnedApps1121H2 VirtualDesktopPinnedApps1121H2;

        // return true if window is pinned to all desktops
        public static bool IsWindowPinned(IntPtr hWnd)
        {
            if (hWnd == IntPtr.Zero) throw new ArgumentNullException();
            return DesktopManager1121H2.VirtualDesktopPinnedApps1121H2.IsViewPinned(GetApplicationView(hWnd));
        }

        // pin window to all desktops
        public static void PinWindow(IntPtr hWnd)
        {
            if (hWnd == IntPtr.Zero) throw new ArgumentNullException();
            var view = GetApplicationView(hWnd);
            if (!DesktopManager1121H2.VirtualDesktopPinnedApps1121H2.IsViewPinned(view))
            {
                DesktopManager1121H2.VirtualDesktopPinnedApps1121H2.PinView(view);
            }
        }

        // unpin window from all desktops
        public static void UnpinWindow(IntPtr hWnd)
        {
            if (hWnd == IntPtr.Zero) throw new ArgumentNullException();
            var view = GetApplicationView(hWnd);
            if (DesktopManager1121H2.VirtualDesktopPinnedApps1121H2.IsViewPinned(view))
            {
                DesktopManager1121H2.VirtualDesktopPinnedApps1121H2.UnpinView(view);
            }
        }

    }



    internal static class DesktopManager11
    {
        static DesktopManager11()
        {
            var shell = (IServiceProvider10)Activator.CreateInstance(Type.GetTypeFromCLSID(Guids.CLSID_ImmersiveShell11));
            ApplicationViewCollection11 = (IApplicationViewCollection11)shell.QueryService(typeof(IApplicationViewCollection11).GUID, typeof(IApplicationViewCollection11).GUID);
            VirtualDesktopPinnedApps11 = (IVirtualDesktopPinnedApps11)shell.QueryService(Guids.CLSID_VirtualDesktopPinnedApps11, typeof(IVirtualDesktopPinnedApps11).GUID);
        }

        static IApplicationView11 GetApplicationView(IntPtr hWnd)
        {
            DesktopManager11.ApplicationViewCollection11.GetViewForHwnd(hWnd, out IApplicationView11 view);
            return view;
        }

        internal static IApplicationViewCollection11 ApplicationViewCollection11;
        internal static IVirtualDesktopPinnedApps11 VirtualDesktopPinnedApps11;

        // return true if window is pinned to all desktops
        public static bool IsWindowPinned(IntPtr hWnd)
        {
            if (hWnd == IntPtr.Zero) throw new ArgumentNullException();
            return DesktopManager11.VirtualDesktopPinnedApps11.IsViewPinned(GetApplicationView(hWnd));
        }

        // pin window to all desktops
        public static void PinWindow(IntPtr hWnd)
        {
            if (hWnd == IntPtr.Zero) throw new ArgumentNullException();
            var view = GetApplicationView(hWnd);
            if (!DesktopManager11.VirtualDesktopPinnedApps11.IsViewPinned(view))
            {
                DesktopManager11.VirtualDesktopPinnedApps11.PinView(view);
            }
        }

        // unpin window from all desktops
        public static void UnpinWindow(IntPtr hWnd)
        {
            if (hWnd == IntPtr.Zero) throw new ArgumentNullException();
            var view = GetApplicationView(hWnd);
            if (DesktopManager11.VirtualDesktopPinnedApps11.IsViewPinned(view))
            {
                DesktopManager11.VirtualDesktopPinnedApps11.UnpinView(view);
            }
        }

    }



    internal static class DesktopManager1607
    {
        static DesktopManager1607()
        {
            var shell = (IServiceProvider10)Activator.CreateInstance(Type.GetTypeFromCLSID(Guids.CLSID_ImmersiveShell1607));
            ApplicationViewCollection1607 = (IApplicationViewCollection1607)shell.QueryService(typeof(IApplicationViewCollection1607).GUID, typeof(IApplicationViewCollection1607).GUID);
            VirtualDesktopPinnedApps1607 = (IVirtualDesktopPinnedApps1607)shell.QueryService(Guids.CLSID_VirtualDesktopPinnedApps1607, typeof(IVirtualDesktopPinnedApps1607).GUID);
        }

        static IApplicationView1607 GetApplicationView(IntPtr hWnd)
        {
            DesktopManager1607.ApplicationViewCollection1607.GetViewForHwnd(hWnd, out IApplicationView1607 view);
            return view;
        }

        internal static IApplicationViewCollection1607 ApplicationViewCollection1607;
        internal static IVirtualDesktopPinnedApps1607 VirtualDesktopPinnedApps1607;

        // return true if window is pinned to all desktops
        public static bool IsWindowPinned(IntPtr hWnd)
        {
            if (hWnd == IntPtr.Zero) throw new ArgumentNullException();
            return DesktopManager1607.VirtualDesktopPinnedApps1607.IsViewPinned(GetApplicationView(hWnd));
        }

        // pin window to all desktops
        public static void PinWindow(IntPtr hWnd)
        {
            if (hWnd == IntPtr.Zero) throw new ArgumentNullException();
            var view = GetApplicationView(hWnd);
            if (!DesktopManager1607.VirtualDesktopPinnedApps1607.IsViewPinned(view))
            {
                DesktopManager1607.VirtualDesktopPinnedApps1607.PinView(view);
            }
        }

        // unpin window from all desktops
        public static void UnpinWindow(IntPtr hWnd)
        {
            if (hWnd == IntPtr.Zero) throw new ArgumentNullException();
            var view = GetApplicationView(hWnd);
            if (DesktopManager1607.VirtualDesktopPinnedApps1607.IsViewPinned(view))
            {
                DesktopManager1607.VirtualDesktopPinnedApps1607.UnpinView(view);
            }
        }

    }


    internal static class DesktopManager11Insider
    {
        static DesktopManager11Insider()
        {
            var shell = (IServiceProvider10)Activator.CreateInstance(Type.GetTypeFromCLSID(Guids.CLSID_ImmersiveShell11Insider));
            ApplicationViewCollection11Insider = (IApplicationViewCollection11Insider)shell.QueryService(typeof(IApplicationViewCollection11Insider).GUID, typeof(IApplicationViewCollection11Insider).GUID);
            VirtualDesktopPinnedApps11Insider = (IVirtualDesktopPinnedApps11Insider)shell.QueryService(Guids.CLSID_VirtualDesktopPinnedApps11Insider, typeof(IVirtualDesktopPinnedApps11Insider).GUID);
        }

        static IApplicationView11Insider GetApplicationView(IntPtr hWnd)
        {
            DesktopManager11Insider.ApplicationViewCollection11Insider.GetViewForHwnd(hWnd, out IApplicationView11Insider view);
            return view;
        }

        internal static IApplicationViewCollection11Insider ApplicationViewCollection11Insider;
        internal static IVirtualDesktopPinnedApps11Insider VirtualDesktopPinnedApps11Insider;

        // return true if window is pinned to all desktops
        public static bool IsWindowPinned(IntPtr hWnd)
        {
            if (hWnd == IntPtr.Zero) throw new ArgumentNullException();
            return DesktopManager11Insider.VirtualDesktopPinnedApps11Insider.IsViewPinned(GetApplicationView(hWnd));
        }

        // pin window to all desktops
        public static void PinWindow(IntPtr hWnd)
        {
            if (hWnd == IntPtr.Zero) throw new ArgumentNullException();
            var view = GetApplicationView(hWnd);
            if (!DesktopManager11Insider.VirtualDesktopPinnedApps11Insider.IsViewPinned(view))
            {
                DesktopManager11Insider.VirtualDesktopPinnedApps11Insider.PinView(view);
            }
        }

        // unpin window from all desktops
        public static void UnpinWindow(IntPtr hWnd)
        {
            if (hWnd == IntPtr.Zero) throw new ArgumentNullException();
            var view = GetApplicationView(hWnd);
            if (DesktopManager11Insider.VirtualDesktopPinnedApps11Insider.IsViewPinned(view))
            {
                DesktopManager11Insider.VirtualDesktopPinnedApps11Insider.UnpinView(view);
            }
        }

    }




    internal static class DesktopManagerS2022
    {
        static DesktopManagerS2022()
        {
            var shell = (IServiceProvider10)Activator.CreateInstance(Type.GetTypeFromCLSID(Guids.CLSID_ImmersiveShellS2022));
            ApplicationViewCollectionS2022 = (IApplicationViewCollectionS2022)shell.QueryService(typeof(IApplicationViewCollectionS2022).GUID, typeof(IApplicationViewCollectionS2022).GUID);
            VirtualDesktopPinnedAppsS2022 = (IVirtualDesktopPinnedAppsS2022)shell.QueryService(Guids.CLSID_VirtualDesktopPinnedAppsS2022, typeof(IVirtualDesktopPinnedAppsS2022).GUID);
        }

        static IApplicationViewS2022 GetApplicationView(IntPtr hWnd)
        {
            DesktopManagerS2022.ApplicationViewCollectionS2022.GetViewForHwnd(hWnd, out IApplicationViewS2022 view);
            return view;
        }

        internal static IApplicationViewCollectionS2022 ApplicationViewCollectionS2022;
        internal static IVirtualDesktopPinnedAppsS2022 VirtualDesktopPinnedAppsS2022;

        // return true if window is pinned to all desktops
        public static bool IsWindowPinned(IntPtr hWnd)
        {
            if (hWnd == IntPtr.Zero) throw new ArgumentNullException();
            return DesktopManagerS2022.VirtualDesktopPinnedAppsS2022.IsViewPinned(GetApplicationView(hWnd));
        }

        // pin window to all desktops
        public static void PinWindow(IntPtr hWnd)
        {
            if (hWnd == IntPtr.Zero) throw new ArgumentNullException();
            var view = GetApplicationView(hWnd);
            if (!DesktopManagerS2022.VirtualDesktopPinnedAppsS2022.IsViewPinned(view))
            {
                DesktopManagerS2022.VirtualDesktopPinnedAppsS2022.PinView(view);
            }
        }

        // unpin window from all desktops
        public static void UnpinWindow(IntPtr hWnd)
        {
            if (hWnd == IntPtr.Zero) throw new ArgumentNullException();
            var view = GetApplicationView(hWnd);
            if (DesktopManagerS2022.VirtualDesktopPinnedAppsS2022.IsViewPinned(view))
            {
                DesktopManagerS2022.VirtualDesktopPinnedAppsS2022.UnpinView(view);
            }
        }

    }



    internal static class DesktopManager22H2
    {
        static DesktopManager22H2()
        {
            var shell = (IServiceProvider10)Activator.CreateInstance(Type.GetTypeFromCLSID(Guids.CLSID_ImmersiveShell22H2));
            ApplicationViewCollection22H2 = (IApplicationViewCollection22H2)shell.QueryService(typeof(IApplicationViewCollection22H2).GUID, typeof(IApplicationViewCollection22H2).GUID);
            VirtualDesktopPinnedApps22H2 = (IVirtualDesktopPinnedApps22H2)shell.QueryService(Guids.CLSID_VirtualDesktopPinnedApps22H2, typeof(IVirtualDesktopPinnedApps22H2).GUID);
        }

        static IApplicationView22H2 GetApplicationView(IntPtr hWnd)
        {
            DesktopManager22H2.ApplicationViewCollection22H2.GetViewForHwnd(hWnd, out IApplicationView22H2 view);
            return view;
        }

        internal static IApplicationViewCollection22H2 ApplicationViewCollection22H2;
        internal static IVirtualDesktopPinnedApps22H2 VirtualDesktopPinnedApps22H2;

        // return true if window is pinned to all desktops
        public static bool IsWindowPinned(IntPtr hWnd)
        {
            if (hWnd == IntPtr.Zero) throw new ArgumentNullException();
            return DesktopManager22H2.VirtualDesktopPinnedApps22H2.IsViewPinned(GetApplicationView(hWnd));
        }

        // pin window to all desktops
        public static void PinWindow(IntPtr hWnd)
        {
            if (hWnd == IntPtr.Zero) throw new ArgumentNullException();
            var view = GetApplicationView(hWnd);
            if (!DesktopManager22H2.VirtualDesktopPinnedApps22H2.IsViewPinned(view))
            {
                DesktopManager22H2.VirtualDesktopPinnedApps22H2.PinView(view);
            }
        }

        // unpin window from all desktops
        public static void UnpinWindow(IntPtr hWnd)
        {
            if (hWnd == IntPtr.Zero) throw new ArgumentNullException();
            var view = GetApplicationView(hWnd);
            if (DesktopManager22H2.VirtualDesktopPinnedApps22H2.IsViewPinned(view))
            {
                DesktopManager22H2.VirtualDesktopPinnedApps22H2.UnpinView(view);
            }
        }
    }



    #endregion


}
