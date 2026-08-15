using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace C3AP_Client
{
    public partial class ItemPopup : Form
    {
        private Image cyrstal = C3AP_Client.Properties.Resources.crystal;
        private Image cleargem = C3AP_Client.Properties.Resources.cleargem;
        private Image bluegem = C3AP_Client.Properties.Resources.bluegem;
        private Image redgem = C3AP_Client.Properties.Resources.redgem;
        private Image yellowgem = C3AP_Client.Properties.Resources.yelllowgem;
        private Image greengem = C3AP_Client.Properties.Resources.greengem;
        private Image purplegem = C3AP_Client.Properties.Resources.purplegem;
        private Image relics = C3AP_Client.Properties.Resources.relicsapphire;
        private Image relicg = C3AP_Client.Properties.Resources.relicgold;
        private Image relicp = C3AP_Client.Properties.Resources.relicplatinum;

        private Queue<Tuple<long, string, string>> itemQueue = new Queue<Tuple<long, string, string>>();
        private System.Windows.Forms.Timer queueTimer = new System.Windows.Forms.Timer();
        private System.Windows.Forms.Timer displayTimer = new System.Windows.Forms.Timer();

        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        private static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);

        [DllImport("user32.dll")]
        private static extern bool AnimateWindow(IntPtr hWnd, int time, int flags);

        private const int AW_SLIDE = 0x00040000;
        private const int AW_VER_POSITIVE = 0x0004;
        private const int AW_VER_NEGATIVE = 0x0008;
        private const int AW_HIDE = 0x00010000;

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }
        public ItemPopup()
        {
            InitializeComponent();

            
            queueTimer.Interval = 500;
            queueTimer.Tick += ProcessQueue;

            displayTimer.Interval = 5000;
            displayTimer.Tick += (s, e) => {
                displayTimer.Stop();
                HideItemPopup();
                System.Threading.Tasks.Task.Delay(400).ContinueWith(_ => {
                    if (this.IsHandleCreated)
                    {
                        this.Invoke(new Action(() => ProcessQueue(null, null)));
                    }
                });
            };
        }

        private IntPtr targetHwnd = IntPtr.Zero;

        public void StartTracking(IntPtr hwnd)
        {
            targetHwnd = hwnd;
        }

        private void UpdatePosition(object sender, EventArgs e)
        {
            if (targetHwnd != IntPtr.Zero && GetWindowRect(targetHwnd, out RECT rect))
            {
                int popupX = rect.Right - 358;
                int popupY = rect.Bottom - 150;

                this.Location = new Point(popupX, popupY);
            }
            else
            {
                
            }
        }

        private void setItemIcon(long APItemId)
        {
            int labelWidth = itemName.Width / 2;
            int labelWidth2 = itemLocation.Width / 2;
            int panelWidth = panel1.Width / 2;
            itemName.Location = new Point(panelWidth - labelWidth + 40, 29);
            itemLocation.Location = new Point(panelWidth - labelWidth2 + 40, 63);

            bool isCrystalItem = APItemId >= 142000 && APItemId <= 142024;
            bool isClearGemItem = APItemId >= 142025 && APItemId <= 142063;
            bool isColoredGemItem = APItemId >= 142064 && APItemId <= 142068;
            bool isRelicSapphireGemItem = APItemId >= 142069 && APItemId <= 142098;
            bool isRelicGoldGemItem = APItemId >= 142099 && APItemId <= 142128;
            bool isRelicPlatinumGemItem = APItemId >= 142129 && APItemId <= 142158;

            if (isCrystalItem) { itemIcon.Image = cyrstal; }
            else if (isClearGemItem) { itemIcon.Image = cleargem; }
            else if (isColoredGemItem)
            {
                if (APItemId == 142064) { itemIcon.Image = bluegem; }
                else if (APItemId == 142065) { itemIcon.Image = redgem; }
                else if (APItemId == 142066) { itemIcon.Image = greengem; }
                else if (APItemId == 142067) { itemIcon.Image = yellowgem; }
                else if (APItemId == 142068) { itemIcon.Image = purplegem; }
            }
            else if (isRelicSapphireGemItem) { itemIcon.Image = relics; }
            else if (isRelicGoldGemItem) { itemIcon.Image = relicg; }
            else if (isRelicPlatinumGemItem) { itemIcon.Image = relicp; }
        }

       
        public void EnqueueItem(long APItemId, string itemName, string levelName)
        {
            itemQueue.Enqueue(new Tuple<long, string, string>(APItemId, itemName, levelName));

            
            TryProcessNextItem();
        }

        private void TryProcessNextItem()
        {
            if (this.Visible || displayTimer.Enabled) return;

            if (itemQueue.Count == 0) return;

            var processes = System.Diagnostics.Process.GetProcessesByName("duckstation-qt-x64-ReleaseLTCG");
            if (processes.Length > 0)
            {
                IntPtr duckHwnd = processes[0].MainWindowHandle;
                IntPtr activeHwnd = GetForegroundWindow();

                if (duckHwnd != IntPtr.Zero && activeHwnd == duckHwnd)
                {
                    // Ja! Nächstes Item holen und anzeigen
                    var nextItem = itemQueue.Dequeue();
                    ShowItemPopup(nextItem.Item1, nextItem.Item2, nextItem.Item3);
                    displayTimer.Start();
                    return;
                }
            }

            if (!queueTimer.Enabled && itemQueue.Count > 0)
            {
                queueTimer.Interval = 1000;
                queueTimer.Start();
            }
        }

        private void ProcessQueue(object sender, EventArgs e)
        {
            
            if (this.Visible) return;

            if (itemQueue.Count == 0)
            {
                return;
            }

            var nextItem = itemQueue.Dequeue();

            ShowItemPopup(nextItem.Item1, nextItem.Item2, nextItem.Item3);

            displayTimer.Start();
        }

        public void ShowItemPopup(long APItemId, string itemName, string levelName)
        {
            this.itemName.Text = itemName;
            itemLocation.Text = levelName;
            setItemIcon(APItemId);
            this.itemName.Font = CustomFontHelper.GetInstance(26, FontStyle.Regular);

            var processes = System.Diagnostics.Process.GetProcessesByName("duckstation-qt-x64-ReleaseLTCG");
            if (processes.Length > 0)
            {
                IntPtr duckHwnd = processes[0].MainWindowHandle;
                if (duckHwnd != IntPtr.Zero && GetWindowRect(duckHwnd, out RECT rect))
                {
                    this.Width = 350;
                    this.Height = 120;

                    int popupX = rect.Right - 358;
                    int popupY = rect.Bottom - 150;

                    this.StartPosition = FormStartPosition.Manual;
                    this.Location = new Point(popupX, popupY);

                    if (this.IsHandleCreated)
                    {
                        this.Invoke(new Action(() =>
                        {
                            this.Show();
                            this.BringToFront();
                            StartTracking(duckHwnd);
                        }));
                    }
                    else
                    {
                        this.Show();
                        this.BringToFront();
                        StartTracking(duckHwnd);
                    }
                }
            }
        }

        public void HideItemPopup()
        {
            if (this.Visible)
            {
                AnimateWindow(this.Handle, 300, AW_SLIDE | AW_VER_POSITIVE | AW_HIDE);
                this.Hide();
            }
        }
    }
}
public class CustomFontHelper
{
    private static PrivateFontCollection fontCollection = new PrivateFontCollection();

    public static Font GetInstance(float size, FontStyle style = FontStyle.Regular)
    {
        if (fontCollection.Families.Length == 0)
        {

            string resourceName = "C3AP_Client.crash-a-like.ttf";

            using (var stream = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName))
            {
                if (stream != null)
                {
                    IntPtr data = Marshal.AllocCoTaskMem((int)stream.Length);
                    byte[] fontData = new byte[stream.Length];
                    stream.Read(fontData, 0, (int)stream.Length);
                    Marshal.Copy(fontData, 0, data, (int)stream.Length);

                    fontCollection.AddMemoryFont(data, (int)stream.Length);
                    Marshal.FreeCoTaskMem(data);
                }
            }
        }

        if (fontCollection.Families.Length > 0)
        {
            return new Font(fontCollection.Families[0], size, style);
        }

        return new Font("Segoe UI", size, style);
    }
}