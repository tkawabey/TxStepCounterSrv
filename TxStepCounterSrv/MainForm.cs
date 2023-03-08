using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;

using System.Reflection;
using System.Runtime.InteropServices;


namespace TxStepCounterSrv
{

 

    public partial class MainForm : Form
    {
        private bool mRunning = false;
        /// <summary>
        /// TCPサーバー。
        /// </summary>
        private TcpListenerEx Server { get; set; }
        /// <summary>
        /// 接続中クライアントリスト。
        /// </summary>
        private List<TcpClientEx> ClientList { get; set; }
        /// <summary>
        /// ステップ数
        /// </summary>
        private int mStep = 0;

        public MainForm()
        {
            InitializeComponent();

            ClientList = new List<TcpClientEx>();


            String hostName = Dns.GetHostName();

            IPHostEntry ip = Dns.GetHostEntry(hostName);
            foreach (IPAddress address in ip.AddressList)
            {
                if (address.AddressFamily == AddressFamily.InterNetwork)
                {
                    //Console.WriteLine(address);
                    txtMyServerIP.Text = address.ToString();
                    break;
                }
            }
            
        }

        // Start or Stop ボタンクリック
        private void btnStart_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                if (mRunning == false)
                {
                    // サーバーを開始する
                    if (startServer(textPortNumber.Text) == true)
                    {
                        btnStart.Text = "Stop";
                    }
                }
                else
                {
                    // サーバーを停止する
                    if (endServer() == true)
                    {
                        btnStart.Text = "Start";
                    }
                }
                
            }
            catch(SocketException ex)
            {
                if (ex.SocketErrorCode == SocketError.AddressAlreadyInUse)
                {
                    MessageBox.Show("指定のポートは他のシステムに使用されています。");

                }
                else
                {
                    MessageBox.Show(ex.ToString(),
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(),
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        // ログリストに追加
        private void AddLog(string text)
        {
            Invoke(new Action(() => 
            {
                // ログを追加
                listBxLog.Items.Add(text);
 
                // リスト末尾を選択中とする
                listBxLog.SelectedIndex = listBxLog.Items.Count != -1 ? listBxLog.Items.Count - 1 : -1;
            }));
        }
        private void setStepText(int step)
        {
            // textStep
            Invoke(new Action(() =>
            {
                // ログを追加
                textStep.Text = step.ToString();
            }));
        }
        // サーバーを開始
        private bool startServer(string port)
        {
            // 接続情報有効チェック
            if (!CheckConnectionSettings(port)) return false;

            // サーバーを作成して監視開始
            //var localEndPoint = new IPEndPoint(IPAddress.Parse(txtMyServerIP.Text), int.Parse(port));
            var localEndPoint = new IPEndPoint(IPAddress.Parse("0.0.0.0"), int.Parse(port));
            Server = new TcpListenerEx(localEndPoint);
            Server.Start();

            // 接続受付ループ開始
            AsyncAcceptWaitLoop();

            mRunning = true;
            AddLog("サーバーを開始しました。");
            return true;
        }
        private void setClientState(bool isConnect)
        {
            // textStatus
            Invoke(new Action(() =>
            {
                if (isConnect)
                {
                    textStatus.Text = "接続中...";
                    textStatus.BackColor = Color.SeaGreen;
                    textStatus.ForeColor = Color.White;
                }
                else
                {
                    textStep.Text = "";
                    textStatus.Text = "";
                    textStatus.BackColor = Color.White;
                    textStatus.ForeColor = Color.Black;

                }
            }));
        }



        // サーバーを停止
        private bool endServer()
        {
            // サーバー停止
            if( Server != null )
            {
                Server.Stop();
            }
            Server = null;

            foreach (var client in ClientList)
            {
                client.Socket.Close();
            }
            ClientList.Clear();


            //this.timer.Enabled = false;
            //this.timer = null;

            mRunning = false;
            AddLog("サーバーを停止しました。");
            return true;
        }
        // ポート番号の文字列をチェック
        private bool CheckConnectionSettings(string port)
        {
            // ポート番号空チェック
            if (string.IsNullOrEmpty(port))
            {
                MessageBox.Show("ポート番号が空です。");
                return false;
            }

            // ポート番号数値チェック
            if (!Regex.IsMatch(port, "^[0-9]+$"))
            {
                MessageBox.Show("ポート番号は数値を指定してください。");
                return false;
            }

            var portNum = int.Parse(port);

            // ポート番号有効値チェック
            if (portNum < IPEndPoint.MinPort || IPEndPoint.MaxPort < portNum)
            {
                MessageBox.Show("無効なポート番号が指定されています。");
                return false;
            }

            return true;
        }
        // Acceptの処理
        private async Task AsyncAcceptWaitLoop()
        {
            await Task.Run(() =>
            {
                // サーバーが監視中の間は接続を受け入れ続ける
                while (Server != null && Server.IsActive)
                {
                    try
                    {
                        // 非同期で接続を待ち受ける
                        Server.BeginAcceptTcpClient(AcceptCallback, null).AsyncWaitHandle.WaitOne(-1);
                    }
                    catch (Exception)
                    {
                        AddLog("接続受け入れでエラーが発生しました。");
                        break;
                    }
                }
            });
        }
        // Acceptコールバック
        private void AcceptCallback(IAsyncResult result)
        {
            try
            {
                // 接続を受け入れる
                var client = Server.EndAcceptTcpClient(result);

                //// 接続ログを出力
                AddLog("クライアント切断: " + client.Client.RemoteEndPoint.ToString());
                setClientState(true);

                //// 接続中クライアントを追加
                var clientInfo = new TcpClientEx(client);
                //SetConnectiongClient(client, true);
                ClientList.Add(clientInfo);

                // クライアントからのデータ受信を待機
                var data = new CommunicationData(clientInfo);
                client.Client.BeginReceive(data.Data, 0, data.Data.Length, SocketFlags.None, ReceiveCallback, data);
            }
            catch (Exception) { }
        }


        byte[] Data = new byte[4];


        // キーアップを送信
        private void sendKeyup()
        {
            const int num = 2;
            SendInptData.INPUT[] inp = new SendInptData.INPUT[num];
            // (2)キーボード(Key Up)を押す
            inp[0].type = SendInptData.INPUT_KEYBOARD;
            inp[0].ki.wVk = (short)Keys.Up;
            inp[0].ki.wScan = (short)SendInptData.MapVirtualKey(inp[0].ki.wVk, 0);
            inp[0].ki.dwFlags = SendInptData.KEYEVENTF_EXTENDEDKEY | SendInptData.KEYEVENTF_KEYDOWN;
            inp[0].ki.dwExtraInfo = 0;
            inp[0].ki.time = 0;

            // (3)キーボード(A)を離す
            inp[1].type = SendInptData.INPUT_KEYBOARD;
            inp[1].ki.wVk = (short)Keys.A;
            inp[1].ki.wScan = (short)SendInptData.MapVirtualKey(inp[1].ki.wVk, 0);
            inp[1].ki.dwFlags = SendInptData.KEYEVENTF_EXTENDEDKEY | SendInptData.KEYEVENTF_KEYUP;
            inp[1].ki.dwExtraInfo = 0;
            inp[1].ki.time = 0;

            // キーボード操作実行
            SendInptData.SendInput(num, ref inp[0], Marshal.SizeOf(inp[0]));
        }
        private void ReceiveCallback(IAsyncResult result)
        {
            try
            {
                // クライアントからのデータを受信
                var data = result.AsyncState as CommunicationData;
                var length = data.Client.Socket.EndReceive(result);

                // 受信データが0byteの場合切断と判定
                if (length == 0)
                {
                    // 切断ログを出力
                    AddLog("クライアント接続: " + data.Client.RemoteEndPointEx.ToString() );
                    setClientState(false);
                    

                    // 接続中クライアントを削除
                    //SetConnectiongClient(data.Client.Client, false);
                    ClientList.Remove(data.Client);

                    // データ受信を終了
                    return;
                }

                // 受信データを出力
                if (length >= 4)
                {
                    // ネットワークバイトオーダーの変換
                    Data[0] = data.Data[3];
                    Data[1] = data.Data[2];
                    Data[2] = data.Data[1];
                    Data[3] = data.Data[0];
                    int Dstep = BitConverter.ToInt32(Data, 0);

                    if (mStep == 0)
                    {
                        mStep = Dstep;
                    }
                    int diffStep = Dstep - mStep;
                    
                    
                    setStepText(Dstep);

                    if (diffStep > 10)
                    {
                        // 前回からの差分が10Step以上なら、Keyupを実行
                        sendKeyup();
                        mStep = Dstep;
                    }
                }

                // サーバーが監視中の場合
                if (Server != null && Server.IsActive)
                {
                    // 再度クライアントからのデータ受信を待機
                    data.Client.Socket.BeginReceive(data.Data, 0, data.Data.Length, SocketFlags.None, ReceiveCallback, data);
                }
            }
            catch (Exception e) {
                MessageBox.Show(e.ToString());
            }
        }
    }


}
