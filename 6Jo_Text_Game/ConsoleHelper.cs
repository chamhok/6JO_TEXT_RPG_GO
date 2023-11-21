﻿using System.Runtime.InteropServices;

public static class ConsoleHelper
{
        private const int FixedWidthTrueType = 54;
        private const int StandardOutputHandle = -11;

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern IntPtr GetStdHandle(int nStdHandle);

        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        static extern bool SetCurrentConsoleFontEx(IntPtr hConsoleOutput, bool MaximumWindow, ref FontInfo ConsoleCurrentFontEx);

        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        static extern bool GetCurrentConsoleFontEx(IntPtr hConsoleOutput, bool MaximumWindow, ref FontInfo ConsoleCurrentFontEx);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool SetConsoleScreenBufferSize(IntPtr hConsoleOutput, COORD size);

        [StructLayout(LayoutKind.Sequential)]
        public struct COORD
        {
                public short X;
                public short Y;
        }

        private static readonly IntPtr ConsoleOutputHandle = GetStdHandle(StandardOutputHandle);

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public struct FontInfo
        {
                internal int cbSize;
                internal int FontIndex;
                internal short FontWidth;
                public short FontSize;
                public int FontFamily;
                public int FontWeight;
                [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
                public string FontName;
        }

        public static FontInfo GetCurrentFont()
        {
                FontInfo before = new FontInfo
                {
                        cbSize = Marshal.SizeOf<FontInfo>()
                };

                if (!GetCurrentConsoleFontEx(ConsoleOutputHandle, false, ref before))
                {
                        var er = Marshal.GetLastWin32Error();
                        throw new System.ComponentModel.Win32Exception(er);
                }
                return before;
        }

        public static FontInfo[] SetCurrentFont(string font, short fontSize = 0)
        {
                FontInfo before = new FontInfo
                {
                        cbSize = Marshal.SizeOf<FontInfo>()
                };

                if (GetCurrentConsoleFontEx(ConsoleOutputHandle, false, ref before))
                {
                        FontInfo set = new FontInfo
                        {
                                cbSize = Marshal.SizeOf<FontInfo>(),
                                FontIndex = 0,
                                FontFamily = FixedWidthTrueType,
                                FontName = font,
                                FontWeight = 400,
                                FontSize = fontSize > 0 ? fontSize : before.FontSize
                        };

                        if (!SetCurrentConsoleFontEx(ConsoleOutputHandle, false, ref set))
                        {
                                var ex = Marshal.GetLastWin32Error();
                                throw new System.ComponentModel.Win32Exception(ex);
                        }

                        // Reset console buffer size and window size
                        SetConsoleScreenBufferSize(ConsoleOutputHandle, new COORD { X = 80, Y = 300 });

                        FontInfo after = new FontInfo
                        {
                                cbSize = Marshal.SizeOf<FontInfo>()
                        };
                        GetCurrentConsoleFontEx(ConsoleOutputHandle, false, ref after);

                        return new[] { before, set, after };
                }
                else
                {
                        var er = Marshal.GetLastWin32Error();
                        throw new System.ComponentModel.Win32Exception(er);
                }
        }
}