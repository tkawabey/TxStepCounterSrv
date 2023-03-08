using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Runtime.InteropServices;


namespace TxStepCounterSrv
{
    public class SendInptData
    {
        // マウスイベント(mouse_eventの引数と同様のデータ)
        [StructLayout(LayoutKind.Sequential)]
        public struct MOUSEINPUT
        {
            public int dx;
            public int dy;
            public int mouseData;
            public int dwFlags;
            public int time;
            public int dwExtraInfo;
        };

        // キーボードイベント(keybd_eventの引数と同様のデータ)
        [StructLayout(LayoutKind.Sequential)]
        public struct KEYBDINPUT
        {
            public short wVk;
            public short wScan;
            public int dwFlags;
            public int time;
            public int dwExtraInfo;
        };

        // ハードウェアイベント
        [StructLayout(LayoutKind.Sequential)]
        public struct HARDWAREINPUT
        {
            public int uMsg;
            public short wParamL;
            public short wParamH;
        };

        // 各種イベント(SendInputの引数データ)
        [StructLayout(LayoutKind.Explicit)]
        public struct INPUT
        {
            [FieldOffset(0)]
            public int type;
            [FieldOffset(4)]
            public MOUSEINPUT mi;
            [FieldOffset(4)]
            public KEYBDINPUT ki;
            [FieldOffset(4)]
            public HARDWAREINPUT hi;
        };

        public const int INPUT_MOUSE = 0;                  // マウスイベント
        public const int INPUT_KEYBOARD = 1;               // キーボードイベント
        public const int INPUT_HARDWARE = 2;               // ハードウェアイベント

        public const int MOUSEEVENTF_MOVE = 0x1;           // マウスを移動する
        public const int MOUSEEVENTF_ABSOLUTE = 0x8000;    // 絶対座標指定
        public const int MOUSEEVENTF_LEFTDOWN = 0x2;       // 左　ボタンを押す
        public const int MOUSEEVENTF_LEFTUP = 0x4;         // 左　ボタンを離す
        public const int MOUSEEVENTF_RIGHTDOWN = 0x8;      // 右　ボタンを押す
        public const int MOUSEEVENTF_RIGHTUP = 0x10;       // 右　ボタンを離す
        public const int MOUSEEVENTF_MIDDLEDOWN = 0x20;    // 中央ボタンを押す
        public const int MOUSEEVENTF_MIDDLEUP = 0x40;      // 中央ボタンを離す
        public const int MOUSEEVENTF_WHEEL = 0x800;        // ホイールを回転する
        public const int WHEEL_DELTA = 120;                // ホイール回転値

        public const int KEYEVENTF_KEYDOWN = 0x0;          // キーを押す
        public const int KEYEVENTF_KEYUP = 0x2;            // キーを離す
        public const int KEYEVENTF_EXTENDEDKEY = 0x1;      // 拡張コード
        public const int VK_SHIFT = 0x10;                  // SHIFTキー


        // キー操作、マウス操作をシミュレート(擬似的に操作する)
        [DllImport("user32.dll")]
        public extern static void SendInput(
            int nInputs, ref INPUT pInputs, int cbsize);

        // 仮想キーコードをスキャンコードに変換
        [DllImport("user32.dll", EntryPoint = "MapVirtualKeyA")]
        public extern static int MapVirtualKey(
            int wCode, int wMapType);
    }
}
